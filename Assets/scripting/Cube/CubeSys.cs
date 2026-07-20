using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Events;

public class CubeSys : MonoBehaviour, CanBePicked
{
    [HideInInspector]
    public UnityEvent<CubeSys> UpdateCubeState = new();
    public bool Pickedup;
    private bool DormantState;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public bool Dormant;
    private Collider CubeSysCollider;
    private Transform CubeChild;
    public CatapultController _playerCatapult;
    private Rigidbody CubeBody;
    private bool HasEneteredDormance;
    [SerializeField]
    private General_Input_Command GeneralInputCommand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CubeSysCollider = this.GetComponentInChildren<Collider>();
        CubeSysCollider.enabled = false;
    }
    private IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(0.2f);
        CubeSysCollider.enabled = true;
    }

    void Start()
    {
        CubeBody = GetComponentInChildren<Rigidbody>();
        CubeChild = GetComponentInChildren<Rigidbody>().transform;
        IsPickedUp();
    }
    void Update()
    {
        if (Dormant != DormantState)
        {
            UpdateCubeState.Invoke(this);
            DormantState = Dormant;
        }
        if (Mathf.Abs(CubeBody.linearVelocity.x) <= 0.1f && Mathf.Abs(CubeBody.linearVelocity.y) <= 0.1f && Mathf.Abs(CubeBody.linearVelocity.z) <= 0.1f && Caught == false && !HasEneteredDormance && !Dormant)
        {
            StartCoroutine(DormanceRoutine());
            HasEneteredDormance = true;
        }
    }
    private IEnumerator DormanceRoutine()
    {
    yield return new WaitForSeconds(2f);
    if (Mathf.Abs(CubeBody.linearVelocity.x) <= 0.01f && Mathf.Abs(CubeBody.linearVelocity.y) <= 0.1f && Mathf.Abs(CubeBody.linearVelocity.z) <= 0.01f && Caught == false)
        {
        Dormant = true;
        Debug.Log("The Cube has fallen asleep.");
        }
        HasEneteredDormance = false;
    }
    public void IsPickedUp()
    {
        CubeChild.GetComponent<Rigidbody>().isKinematic = true;
        Caught = true;
        UpdateCubeState.Invoke(this);
    }
    public void IsReleased()
    {
        if (this.transform.parent != null)
        {
            Released = true;
            StartCoroutine(ReleaseTimer());
            this.gameObject.transform.SetParent(null);
            Detached = true;
            UpdateCubeState.Invoke(this);
            Caught = false;
        }
    }
}
