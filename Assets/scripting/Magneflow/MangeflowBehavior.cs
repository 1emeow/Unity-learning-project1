using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MangeflowBehavior : MonoBehaviour
{
    public MagneflowStateMachine _magneflowStateMachine;
    public GameObject _repulsionSphere;
    public GameObject _attractionSphere;
    [SerializeField]
    private SphereCollider _interactiveSphereCollider;
    [HideInInspector]
    public Rigidbody rigidDestroy; 
    public float _interactiveSphereRadius;
    public float _magneticAttractionForce = 1f;
    public float _magneticRepulsionForce = 1f;
    public float _interactiveDamping = 0f;
    public GameObject _interactiveObject;
  //  public List<GameObject> InteractiveObjectsList = new List<GameObject>();
    void Start()
    {
        _interactiveSphereCollider.radius = _interactiveSphereRadius;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), _attractionSphere.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), _repulsionSphere.GetComponent<Collider>());
    }
    /*void FixedUpdate()
     {
         if (InteractiveObjectsList.Count > 0)
         {
         }
     }*/
    void Update()
    {
        if (rigidDestroy != null)
            rigidDestroy.transform.localScale = Vector3.Lerp(rigidDestroy.transform.localScale, Vector3.zero, Time.deltaTime / 2f); //le vector Lerp permet d'interpoler 2 vector3, ici le local scale et le O, le 2f représente le temps nécessaire }
    }
    void OnTriggerEnter(Collider other)
    {
        Rigidbody _interactiveRigid = other.GetComponent<Rigidbody>();
        if (_interactiveRigid != null)
            _interactiveRigid.linearDamping += _interactiveDamping;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 8 && other.GetComponent<Rigidbody>() != null) //&& !InteractiveObjectsList.Contains(collision.gameObject))
        {
            _interactiveObject = other.gameObject;
            Vector3 localPosition = transform.InverseTransformPoint(_interactiveObject.transform.position); //inverse transform point convertit la position globale en position locale
            Rigidbody _interactiveRigid = _interactiveObject.GetComponent<Rigidbody>();
            if (localPosition.y - 2 > 0 && _attractionSphere.activeSelf) //on regarde si l'objet a �t� activ� par le SetActive dans la state machine
            {
                Vector3 Distanceofthetwo = _interactiveObject.transform.position - _attractionSphere.transform.position;
                _interactiveRigid.AddForce((Distanceofthetwo).normalized * _magneticAttractionForce * (-1f), ForceMode.Acceleration); //on applique la force d'attraction
            }
            else if (localPosition.y - 2 <= 0 && _repulsionSphere.activeSelf)
            {
                Vector3 Distanceofthetwo = _interactiveObject.transform.position - _attractionSphere.transform.position;
                _interactiveRigid.AddForce((Distanceofthetwo).normalized * _magneticRepulsionForce, ForceMode.Acceleration);
            }
            //  InteractiveObjectsList.Add(collision.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Rigidbody _interactiveRigid = other.GetComponent<Rigidbody>();
        if (_interactiveRigid != null) 
        _interactiveRigid.linearDamping -= _interactiveDamping;
    }
    public void TimeToDie()
    {
        StartCoroutine(DyingTime());
    }
    public IEnumerator DyingTime()
    {
        yield return null;
        _magneflowStateMachine.ChangeStatus(MagneflowStateMachine.Status.dying); //parce que 'Status': cannot reference a type through an expression; try 'MagneflowStateMachine.Status' instead, ce qu'a dit Unity
        _magneflowStateMachine.StatusChange();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
