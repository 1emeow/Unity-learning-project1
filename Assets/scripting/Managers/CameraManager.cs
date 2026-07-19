using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineBrain BrainCam;
    private Camera BaseCam;
    public CinemachineCamera CubeCamera;
    public CubeSys CubeSys;
    public CinemachineCamera OtherCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BaseCam = GetComponent<Camera>();
        BrainCam = GetComponent<CinemachineBrain>();
    }
    public void CubeListening(CubeSys cubesys)
    {
        cubesys.UpdateCubeState.AddListener(UpdateCubeState);
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateCubeState(CubeSys cubesys)
    {
        if (!cubesys.Dormant) 
        {
            if (CubeCamera == null)
                CubeCamera = cubesys.transform.GetComponentInChildren<CinemachineCamera>();
            BrainCam.enabled = true;
            if (cubesys.transform.parent != null && cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>() != null && cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>().name != "PlayerCamera" && OtherCamera == null)
                {
                    OtherCamera = cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>();
                    CubeCamera.Priority = 0;
                    OtherCamera.Priority = 100;
                }
            else if (cubesys.transform.parent == null)
            {
                if (OtherCamera != null)
                {
                    OtherCamera.Priority = 0;
                    OtherCamera = null;
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
