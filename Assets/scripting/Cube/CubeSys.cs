using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CubeSys : MonoBehaviour, CanBePicked
{
    public bool Pickedup;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public CameraManager CameraManager;
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
        if (CameraManager.CubeSys == null)
        CameraManager.CubeSys = this;
        CubeParent = this.transform.parent.GetComponentInParent<Rigidbody>();
        CubeSysCollider = this.GetComponentInChildren<Collider>();
       /* ParentColliders = CubeParent.GetComponentsInChildren<Collider>(); //c'est une table référençant tous les colliders
        
        foreach (Collider ParentCollider in ParentColliders)
        {
            Physics.IgnoreCollision(CubeSysCollider, ParentCollider, true);
        } */
        CubeSysCollider.enabled = false;
    }
     private IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(0.2f);
        CubeSysCollider.enabled = true;
    }

    void Start()
    {
        CameraManager.CubeCamera = CubeCamera;
        foreach (Transform child in transform)
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
            CameraManager.OtherCamera = OtherCamera;
            CubeChild.GetComponent<Rigidbody>().isKinematic = true;
            Caught = true;
/*            if (transform.parent.GetComponentInChildren<CinemachineCamera>() != null && transform.parent.GetComponentInChildren<CinemachineCamera>().name != "PlayerCamera" && (OtherCamera == null || OtherCamera.Priority == 0))
            {
                OtherCamera = transform.parent.GetComponentInChildren<CinemachineCamera>();
                CubeCamera.Priority = 0;
                OtherCamera.Priority = 100;
            }
*/
            //   PlayerCamera.Target.TrackingTarget = Catapilt.transform;
            //    PlayerCamera.GetComponent<CinemachineFollow>().FollowOffset = new Vector3(-15f, 8f, 0f);
            CameraManager.UpdateCamera();
        }
        if (Released && !Detached)
            {
        //    CubeCamera.Priority = 100;
        /*    if (OtherCamera != null)
            {
                OtherCamera.Priority = 0;
                OtherCamera = null;
                CameraManager.OtherCamera = null;
         */
           // }

            /*foreach (Collider ParentCollider in ParentColliders)
            {
                Physics.IgnoreCollision(CubeSysCollider, ParentCollider, false);
            }*/
            StartCoroutine(ReleaseTimer());
            this.gameObject.transform.SetParent(null);
            Detached = true;
            GeneralMouseCommand.UpdateCubeState();
            CameraManager.UpdateCamera();
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
