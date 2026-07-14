using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera BaseCam;
    public CubeSys CubeSys;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera OtherCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BaseCam = this.GetComponent<Camera>(); 
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateCamera()
    {
        if(CubeSys.transform.parent!= null && CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>() != null && CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>().name != "PlayerCamera" && OtherCamera == null)
        {
            {
                OtherCamera = CubeSys.transform.parent.GetComponentInChildren<CinemachineCamera>();
                CubeCamera.Priority = 0;
                OtherCamera.Priority = 100;
            }
        }
        else
            if (CubeSys.transform.parent == null)
        {
            Debug.Log("y a pas de parent wesh");
            if (OtherCamera != null)
            {
                OtherCamera.Priority = 0;
                OtherCamera = null;
            }
            CubeCamera.Priority = 100;
        }
    }
}
