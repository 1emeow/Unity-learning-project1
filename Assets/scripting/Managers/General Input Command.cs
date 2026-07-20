using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class General_Input_Command : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent PausedStatusChanged = new (); //on fait un évènement public auquel vont s'inscrire les autres scripts
    public Vector2 mouseDelta;
    public bool commandSystemEnabler;
    public InputActionReference lookAction;
    public InputActionReference menuAction;
    public CatapultController LaCatapult;
    public InputActionReference clickAction;
    public CubeSys CubeSys;
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
        menuAction.action.Enable();
        menuAction.action.performed += OnPause;
    }

    private void OnDisable()
    {
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;
        lookAction.action.Disable();
        clickAction.action.performed -= OnAttack;
        clickAction.action.canceled -= OnAttack;
        clickAction.action.Disable();
        menuAction.action.performed -= OnPause;
        menuAction.action.Disable();
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
        }
        else
        {
            Mover = cubesys.gameObject.GetComponentInChildren<CanMove>();
            Launcher = null;
        }
    }
    private void OnLook(InputAction.CallbackContext ctx)
    {
        mouseDelta = ctx.ReadValue<Vector2>();
    }
    private void OnPause(InputAction.CallbackContext ctx)
    {
        PausedStatusChanged.Invoke(); //évènement public indiquant aux scripts correspondants que la pause a lieu
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (StartGame)  //si le jeu n'est pas en pause
        {
            if (LaCatapult != null)
                LaCatapult.ReceiveLookInput(mouseDelta);
            if (Mover != null)
                Mover.UpdateInput();
            if (Launcher != null)
                Launcher.LaunchUpdate();
        }
    }
}

