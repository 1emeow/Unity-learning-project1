using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
public class MenuCameraManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineBrain BrainCam; //on utilise le système brainmachine parce que c'est une fonction fournie par la compagnie qui permet d'avoir des caméras qui traquent sans codage complexe
    private Camera BaseCam;
    private float yaw;
    private float pitch;
    public CinemachineCamera FirstCam;
    public CinemachineCamera SecondCam;
    public CinemachineCamera CubeCamera;
    public CinemachineCamera LastCam;
    public CinemachineCamera PreviousCamera;
    public CinemachineCamera CurrentCamera;
    public CubeSys CubeSys;
    public bool Isloading;
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
        StartCoroutine(FirstCamCoroutine());
    }
    private IEnumerator FirstCamCoroutine()
    {
        CurrentCamera = SecondCam;
        yield return new WaitForSeconds(0.1f);
        CurrentCamera = FirstCam;
    }
    // Update is called once per frame
    void Update()
    {
        if (PreviousCamera != CurrentCamera)
        {
         if (PreviousCamera != null)
            PreviousCamera.Priority = 0;
            CurrentCamera.Priority = 100;
            PreviousCamera = CurrentCamera;
        }
        if (Isloading)
        {
            CurrentCamera = LastCam;
        }
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
    }
}
