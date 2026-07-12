using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CubeSys : MonoBehaviour, CanBePicked
{
    public bool Pickedup;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public Camera CameraManager;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera OtherCamera;
    private Rigidbody CubeParent; 
    private Collider CubeParentCollider;
    private Collider[] ParentColliders; //la table en question
    private Collider CubeSysCollider;
    private Transform CubeChild;
    [SerializeField]
    private General_Mouse_Command GeneralMouseCommand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CubeParent = this.transform.parent.GetComponentInParent<Rigidbody>();
        CubeSysCollider = this.GetComponentInChildren<Collider>();
        ParentColliders = CubeParent.GetComponentsInChildren<Collider>(); //c'est une table référençant tous les colliders

        foreach (Collider ParentCollider in ParentColliders)
        {
            Physics.IgnoreCollision(CubeSysCollider, ParentCollider, true);
        }
  
        //Physics.IgnoreCollision(CubeSysCollider, CubeParentCollider, true);
    }
     private IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(0.2f);
        Physics.IgnoreCollision(CubeSysCollider, CubeParentCollider, false);
    }

    void Start()
    {
        foreach(Transform child in transform)
        {
           if (child.GetComponent<Rigidbody>() != null)
            {
                CubeChild = child;
                break;
            }
        }
    }

    void Update()
    {
        if (!Released && !Caught)
        {

            CubeChild.GetComponent<Rigidbody>().isKinematic = true;
            Caught = true;
            foreach (Transform child in transform.parent)
            {
                if (child == transform)
                    continue;

                if (child.GetComponent<CinemachineCamera>() != null)
                {
                    OtherCamera = child.GetComponent<CinemachineCamera>();
                    break;
                }
            }
            CubeCamera.Priority = 0;
            OtherCamera.Priority = 100;
            //   PlayerCamera.Target.TrackingTarget = Catapilt.transform;
            //    PlayerCamera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-15f, 8f, 0f);
        }
        if (Released && !Detached)
            {
            CubeCamera.Priority = 100;
            if (OtherCamera != null)
            {
                OtherCamera.Priority = 0;
            }
            /*foreach (Collider ParentCollider in ParentColliders)
            {
                Physics.IgnoreCollision(CubeSysCollider, ParentCollider, false);
            }*/
            ReleaseTimer();
            this.gameObject.transform.SetParent(null);
            Detached = true;
            GeneralMouseCommand.UpdateCubeState();
            //   Physics.IgnoreCollision(CubeSysCollider, CubeParentCollider, false);
            Caught = false;
            }
     //   if (Pickedup)
     //       pickup();
    }
    public void IsPickedUp()
    { 
   //             this.gameObject.transform.SetParent();
   //             break;
   //         }
    }
   public void IsReleased()
    {
        if (this.transform.parent != null)
        {
            Released = true;
        //    PlayerCamera.Target.TrackingTarget = this.transform.GetChild(0);
        //    PlayerCamera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-5f, 1f, 0f);
        }
    }
}
