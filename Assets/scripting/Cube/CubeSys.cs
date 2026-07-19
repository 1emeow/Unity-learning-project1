using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Events;

public class CubeSys : MonoBehaviour, CanBePicked
{
    [HideInInspector]
    public UnityEvent<CubeSys> UpdateCubeState = new ();
    public bool Pickedup;
    private bool DormantState;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public bool Dormant;
    private Collider CubeSysCollider;
    private Transform CubeChild;
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
