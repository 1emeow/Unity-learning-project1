using UnityEngine;
using System.Collections.Generic;

public class MangeflowBehavior : MonoBehaviour
{
    public GameObject _repulsionSphere;
    public GameObject _attractionSphere;
    [SerializeField]
    private SphereCollider _interactiveSphereCollider;
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
            Debug.Log(_interactiveObject.name);
            Vector3 localPosition = transform.InverseTransformPoint(_interactiveObject.transform.position); //inverse transform point convertit la position globale en position locale
            Debug.Log(localPosition);
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

}
