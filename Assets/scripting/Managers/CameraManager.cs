using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineBrain BrainCam;
    private Camera BaseCam;
    public CubeSys CubeSys;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera OtherCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BaseCam = GetComponent<Camera>();
        BrainCam = GetComponent<CinemachineBrain>();
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateCameraCube()
    {
        if (!CubeSys.Dormant)
        {
            BrainCam.enabled = true;
            if (CubeSys.transform.parent != null && CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>() != null && CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>().name != "PlayerCamera" && OtherCamera == null)
                {
                    OtherCamera = CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>();
                    CubeCamera.Priority = 0;
                    OtherCamera.Priority = 100;
                }
            else if (CubeSys.transform.parent == null)
            {
                if (OtherCamera != null)
                {
                    OtherCamera.Priority = 0;
                    OtherCamera = null;
                }
                if (CubeCamera != null)
                {
                    CubeCamera.Priority = 100;
                }
            }
        }
        else
        {
            if (CubeCamera != null)
            {
                CubeCamera.Priority = 0;
                CubeCamera = null;
            }
            BrainCam.enabled = false;
        }
    }
}
