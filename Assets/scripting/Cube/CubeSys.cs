using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Events;

public class CubeSys : MonoBehaviour, CanBePicked //cet object possède les fonctions indiquées dans l'interface CanBePicked
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
        CubeSysCollider.enabled = false; //important parce que unity gère difficilement les rapports de masse, un objet non massique fera toujours bouger l'objet qu'il touche et ce quelque soit la masse
    }
    private IEnumerator ReleaseTimer() //on ne veut pas que le cube touche la catapulte pendant la phase de lancement pour ne pas que ça parte dans tous les sens
    {
        yield return new WaitForSeconds(0.2f);
        CubeSysCollider.enabled = true;
    }

    void Start()
    {
        CubeBody = GetComponentInChildren<Rigidbody>();
        CubeChild = GetComponentInChildren<Rigidbody>().transform;
        IsPickedUp(); //normalement le cube naît attaché à la catapulte, il va donc agir en conséquence
    }
    void Update()
    {
        if (Dormant != DormantState) //la logique de dormance
        {
            UpdateCubeState.Invoke(this);
            DormantState = Dormant;
        }
        //
        if (CubeBody.linearVelocity.magnitude <= 0.1f && Caught == false && !HasEneteredDormance && !Dormant)
        {
            StartCoroutine(DormanceRoutine());
            HasEneteredDormance = true;
        }
    }
    private IEnumerator DormanceRoutine() //timer de la dormance
    {
    yield return new WaitForSeconds(2f);
    if (CubeBody.linearVelocity.magnitude <= 0.1f && Caught == false)
        {
        Dormant = true;
        Debug.Log("The Cube has fallen asleep.");
        }
        HasEneteredDormance = false;
    }
    public void IsPickedUp() //ce qu'il se passe lorsque le cube est ramassé par un objet
    {
        CubeChild.GetComponent<Rigidbody>().isKinematic = true; //on ne veut pas que le cube bouge avant lancement
        Caught = true;
        UpdateCubeState.Invoke(this);
    }
    public void IsReleased() //ce qu'il se passe lorsque le cube est relaché
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
