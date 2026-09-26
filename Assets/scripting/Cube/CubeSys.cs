using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.Events;

public class CubeSys : MonoBehaviour, CanBePicked //cet object possède les fonctions indiquées dans l'interface CanBePicked
{
    [HideInInspector]
    public UnityEvent<CubeSys> UpdateCubeState = new();
    public bool Pickedup;
    private bool DormantState;
    public bool Released;
    private List<Collider> parentColliderslist = new List<Collider>();
    public bool Detached;
    public bool Caught;
    public bool Dormant;
    public bool pickupable { get; set; }
    private Collider CubeSysCollider;
    private Collider parentSearchRadius;
    private Transform CubeChild;
    public CatapultController _playerCatapult;
    private Rigidbody CubeBody;
    private bool HasEneteredDormance;
    public bool Iamdead;
    public bool DeadAndDormant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        pickupable = false;
        CubeSysCollider = this.GetComponentInChildren<Collider>();
    }

    private IEnumerator ReleaseTimer() //on ne veut pas que le cube touche la catapulte pendant la phase de lancement pour ne pas que ça parte dans tous les sens
    {
        yield return new WaitForSeconds(0.2f);
        foreach (Collider parentCollider in parentColliderslist)
        {
            Physics.IgnoreCollision(CubeSysCollider, parentCollider, false);
        }
    
    }
    void Start()
    {
        CubeBody = GetComponentInChildren<Rigidbody>();
        CubeChild = GetComponentInChildren<Rigidbody>().transform;
        IsPickedUp(); //normalement le cube naît attaché à la catapulte, il va donc agir en conséquence
        UpdateCubeState.Invoke(this);
        foreach (Collider parentCollider in transform.root.GetComponentsInChildren<Collider>()) //important parce que unity gère difficilement les rapports de masse, un objet non massique fera toujours bouger l'objet qu'il touche et ce quelque soit la masse
        {
            if (parentCollider == null || parentCollider == CubeSysCollider)
                continue;
            if (!parentCollider.isTrigger)
            {
                parentColliderslist.Add(parentCollider);
                Physics.IgnoreCollision(CubeSysCollider, parentCollider);
            }
            else
            {
                if (parentCollider.gameObject.name == "CatapiltSearchRadius")
                {
                    parentSearchRadius = parentCollider;
                    Physics.IgnoreCollision(CubeSysCollider, parentSearchRadius);
                }
            }
        }
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
        if (CubeBody.linearVelocity.magnitude <= 0.1f && Caught == false && Detached)
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
            Caught = false;
            UpdateCubeState.Invoke(this);
            this.gameObject.transform.SetParent(null);
            Physics.IgnoreCollision(CubeSysCollider, parentSearchRadius, false);
        }
    }
    public void GetKilled()
    {
        if (!Dormant)
        {
            Iamdead = true;
            Dormant = true;
            UpdateCubeState.Invoke(this);
        }
        else
        {
            Iamdead = true;
            DeadAndDormant = true;
            UpdateCubeState.Invoke(this);
        }

    }
}
