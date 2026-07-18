using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class General_Input_Command : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent PausedStatusChanged = new ();
    private bool CatapultActive;
    private bool MoverActive;
    private bool LauncherActive;
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
    public void UpdateCubeState()
    {
        if (CubeSys.transform.parent != null)
        {
            Mover = CubeSys.transform.parent.GetComponentInParent<CanMove>();
            Launcher = CubeSys.transform.parent.GetComponentInParent<Launcher>();
        }
        else
        {
            Mover = CubeSys.gameObject.GetComponentInChildren<CanMove>();
            Launcher = null;
        }
        //Debug.Log(Mover);
    }
    private void OnLook(InputAction.CallbackContext ctx)
    {
        mouseDelta = ctx.ReadValue<Vector2>();
    }
    private void OnPause(InputAction.CallbackContext ctx)
    {
        PausedStatusChanged.Invoke();
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
        if (CubeSys != null)
        {
            Mover = CubeSys.transform.parent.GetComponentInParent<CanMove>();
            Launcher = CubeSys.transform.parent.GetComponentInParent<Launcher>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartGame)
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

