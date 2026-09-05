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
        if (!other.isTrigger)
        {
            Rigidbody rigidDestroy = other.GetComponentInParent<Rigidbody>();

            if (rigidDestroy != null && !dyingtime)
            {
                rigidDestroy.transform.root.SetParent(transform.root, true);
                rigidDestroy.position = transform.position;
                rigidDestroy.linearVelocity = Vector3.zero;
                rigidDestroy.linearDamping = 19;
                rigidDestroy.useGravity = false;
                _mangeflow.rigidDestroy = rigidDestroy;
                dyingtime = true;
                _mangeflow.TimeToDie();
            }
        }
    }
}
