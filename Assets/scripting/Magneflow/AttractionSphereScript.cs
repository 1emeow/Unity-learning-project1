using UnityEngine;

public class AttractionSphereScript : MonoBehaviour
{
    private bool dyingtime;
    public MangeflowBehavior _mangeflow;
    void Start()
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), _mangeflow.GetComponent<Collider>());
    }
    void OnTriggerEnter(Collider other)
    {

        Rigidbody rigidDestroy = other.GetComponent<Rigidbody>();
        if (rigidDestroy !=null && !dyingtime)
        {
            rigidDestroy.transform.root.SetParent(transform.root, true);
            rigidDestroy.linearVelocity = Vector3.zero;
            _mangeflow.rigidDestroy = rigidDestroy;
            dyingtime = true;
            _mangeflow.TimeToDie();
        }
    }
}
