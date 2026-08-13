using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineBrain BrainCam; //on utilise le système brainmachine parce que c'est une fonction fournie par la compagnie qui permet d'avoir des caméras qui traquent sans codage complexe
    private Camera BaseCam;
    private float yaw;
    private float pitch;
    public CinemachineCamera CubeCamera;
    public CubeSys CubeSys;
    public CinemachineCamera OtherCamera;
    [SerializeField] private float sensitivity = 1f; //sensibilité de la souris pour la rotation de la caméra
    private CinemachineOrbitalFollow orbitalFollow; //la cinemachine cam est mise en mode Orbital Follow pour orbiter autour de l'objet en question
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BaseCam = GetComponent<Camera>();
        BrainCam = GetComponent<CinemachineBrain>();
    }
    public void CubeListening(CubeSys cubesys) //la fonction nécessaire pour s'inscrire à l'évènement updatecubestate du cube, évènement qui va indiquer qui on doit observer
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
    public void ReceiveLookInput(Vector2 lookDelta)
    {
        if (orbitalFollow != null)
        {
            orbitalFollow.HorizontalAxis.Value += lookDelta.x * sensitivity;
        }
    }
    public void UpdateCubeState(CubeSys cubesys) //au changement d'état du cube, on change de caméra
    {
        if (!cubesys.Dormant) 
        {
            if (CubeCamera == null)
                CubeCamera = cubesys.transform.GetComponentInChildren<CinemachineCamera>();
            BrainCam.enabled = true;
            //si le cube est attaché à un lanceur
            if (cubesys.transform.parent != null && cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>() != null && cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>().name != "PlayerCamera" && OtherCamera == null)
                {
                    OtherCamera = cubesys.transform.parent.GetComponentInChildren<CinemachineCamera>();
                    CubeCamera.Priority = 0;
                    OtherCamera.Priority = 100;
                    orbitalFollow = OtherCamera.GetComponent<CinemachineOrbitalFollow>();
                }
            else if (cubesys.transform.parent == null)
            {
                if (OtherCamera != null)
                {
                    OtherCamera.Priority = 0;
                    OtherCamera = null;
                    CubeCamera.Priority = 100;
                    Debug.Log("iehafoefih");
                    orbitalFollow = CubeCamera.GetComponent<CinemachineOrbitalFollow>();
                    Debug.Log(orbitalFollow);
                }
            }
        }
        else //si le cube est libre
        {
            if (CubeCamera != null)
            {
                CubeCamera.Priority = 0;
                CubeCamera = null;
                orbitalFollow = null;
            }
            BrainCam.enabled = false;
        }
    }
}
