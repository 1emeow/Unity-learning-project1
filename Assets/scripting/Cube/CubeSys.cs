using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CubeSys : MonoBehaviour, CanBePicked
{
    public bool Pickedup;
    private bool DormantState;
    public bool Released;
    public bool Detached;
    public bool Caught;
    public bool Dormant;
    public CameraManager CameraManager;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera OtherCamera;
    private Rigidbody CubeParent; 
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
        CubeChild = GetComponentInChildren<Rigidbody>().transform;
     /*   foreach (Transform child in transform)
        {
           if (child.GetComponent<Rigidbody>() != null)
            {
                CubeChild = child;
                break;
            }
        }
     */
        IsPickedUp();
    }

    void Update()
    {
        if (Dormant != DormantState)
        {
            if (!Dormant)
            {
                CameraManager.CubeCamera = CubeCamera;
                CameraManager.UpdateCameraCube();
            }
            else
            {
                CameraManager.UpdateCameraCube();
            }
                DormantState = Dormant;
        }
     //   if (Pickedup)
     //       pickup();
    }
    public void IsPickedUp()
    {
        CameraManager.OtherCamera = OtherCamera;
        CubeChild.GetComponent<Rigidbody>().isKinematic = true;
        Caught = true;
        CameraManager.UpdateCameraCube();
    }
    public void IsReleased()
    {
        if (this.transform.parent != null)
        {
            Released = true;
            StartCoroutine(ReleaseTimer());
            this.gameObject.transform.SetParent(null);
            Detached = true;
            GeneralMouseCommand.UpdateCubeState();
            CameraManager.UpdateCameraCube();
            Caught = false;
        }
    }
}
