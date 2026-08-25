using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class General_Input_Command : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent PausedStatusChanged = new (); //on fait un évènement public auquel vont s'inscrire les autres scripts
    [HideInInspector]
    public UnityEvent RestartGame = new();
    [SerializeField]
    private CameraManager _cameraManager;
    public Vector2 mouseDelta;
    public bool commandSystemEnabler;
    public InputActionReference lookAction;
    public InputActionReference menuAction;
    public CatapultController LaCatapult;
    public InputActionReference clickAction;
    public InputActionReference newCubeAction;
    public InputActionReference restartGameAction;
    public InputActionReference rotateCameraAction;
    public InputActionReference recenterCameraAction;
    public CubeSys CubeSys;
    private bool cameraRotating;
    private CubeSys activeCube; 
    private CanMove Mover;
    private Launcher Launcher;
    public bool StartGame;

    public void OnEnable()
    {
        lookAction.action.Enable();
        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;
        clickAction.action.Enable();
        clickAction.action.performed += OnAttack;
        clickAction.action.canceled += OnAttack;
        rotateCameraAction.action.Enable();
        rotateCameraAction.action.performed += OnRotateCam;
        rotateCameraAction.action.canceled += OnRotateCam;
        recenterCameraAction.action.Enable();
        recenterCameraAction.action.performed += OnRecenterCam;
        recenterCameraAction.action.canceled += OnRecenterCam;
        menuAction.action.Enable();
        menuAction.action.performed += OnPause;
        newCubeAction.action.Enable();
        newCubeAction.action.performed += OnNewCube;
        restartGameAction.action.Enable();
        restartGameAction.action.performed += OnRestartGame;
    }

    private void OnDisable()
    {
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;
        lookAction.action.Disable();
        clickAction.action.performed -= OnAttack;
        clickAction.action.canceled -= OnAttack;
        clickAction.action.Disable();
        recenterCameraAction.action.performed -= OnRecenterCam;
        recenterCameraAction.action.canceled -= OnRecenterCam;
        recenterCameraAction.action.Disable();
        rotateCameraAction.action.performed -= OnRotateCam;
        rotateCameraAction.action.canceled -= OnRotateCam;
        rotateCameraAction.action.Disable();
        menuAction.action.performed -= OnPause;
        menuAction.action.Disable();
        newCubeAction.action.Disable();
        newCubeAction.action.performed -= OnNewCube;
        restartGameAction.action.Disable();
        restartGameAction.action.performed -= OnRestartGame;
    }
    public void CubeListening(CubeSys cubesys) //la fonction nécessaire pour s'inscrire à l'évènement updatecubestate du cube, évènement qui va indiquer quel élément bougera avec les actions effectuées par le joueur
    {
        cubesys.UpdateCubeState.AddListener(UpdateCubeState);
    }
    public void UpdateCubeState(CubeSys cubesys) //indique qui bouge
    {
        if (cubesys.transform.parent != null)
        {
            Mover = cubesys.transform.parent.GetComponentInParent<CanMove>(); //peut être la catapulte ou un lanceur temporaire sur lequel on jette le cube
            Launcher = cubesys.transform.parent.GetComponentInParent<Launcher>(); //l'élément du lanceur sujet à la souris
            Debug.Log(LaCatapult);
        }
        else
        {
            Mover = cubesys.gameObject.GetComponentInChildren<CanMove>();
            Launcher = null;
            if (!cubesys.Dormant)
            LaCatapult = null;
        }
        activeCube = cubesys;
    }
    private void OnLook(InputAction.CallbackContext ctx)
    {
        mouseDelta = ctx.ReadValue<Vector2>();
    }
    private void OnPause(InputAction.CallbackContext ctx)
    {
        PausedStatusChanged.Invoke(); //évènement public indiquant aux scripts correspondants que la pause a lieu
    }
    private void OnRestartGame(InputAction.CallbackContext ctx)
    {
        RestartGame.Invoke();
    }
    private void OnNewCube(InputAction.CallbackContext ctx)
    {
        if (activeCube != null)
        {
            activeCube.Dormant = true;
        }
    }
    private void OnAttack(InputAction.CallbackContext ctx)
    {
        // Debug.Log(ctx.phase);
        if (StartGame)
        {
            if (ctx.performed && Launcher != null)
            {
                Launcher.Clicking();
            }

            if (ctx.canceled && Launcher != null)
            {
                Launcher.Clicked();
            }
        }
    }
    private void OnRotateCam(InputAction.CallbackContext ctx)
    {
        // Debug.Log(ctx.phase);
        if (StartGame)
        {
            if (ctx.performed)
            {
                cameraRotating = true;
            }

            if (ctx.canceled)
            {
                cameraRotating = false;
                if (_cameraManager != null)
                _cameraManager.ReceiveLookInput(Vector2.zero);
            }
        }
    }
    private void OnRecenterCam(InputAction.CallbackContext ctx)
    {
     //   _cameraManager.ResetCamera();
    }
    // Update is called once per frame
    void Update()
    {
        if (StartGame)  //si le jeu n'est pas en pause
        {
            if (cameraRotating && _cameraManager != null)
                _cameraManager.ReceiveLookInput(mouseDelta);
            else
            {
             if (LaCatapult != null)
             LaCatapult.ReceiveLookInput(mouseDelta);
            }
            if (Mover != null)
                Mover.UpdateInput();
            if (Launcher != null)
                Launcher.LaunchUpdate();
        }
    }
}

