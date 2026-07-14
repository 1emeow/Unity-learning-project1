using UnityEngine;
using UnityEngine.InputSystem;

public class General_Mouse_Command : MonoBehaviour
{
    private bool CatapultActive;
    private bool MoverActive;
    private bool LauncherActive;
    public Vector2 mouseDelta;
    public bool commandSystemEnabler;
    public InputActionReference lookAction;
    public CatapultController LaCatapult;
    public InputActionReference clickAction;
    public CubeSys CubeSys;
    private CanMove Mover;
    private Launcher Launcher;
    
    public void OnEnable()
    {
        lookAction.action.Enable();
        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;
        clickAction.action.Enable();
        clickAction.action.performed += OnAttack;
        clickAction.action.canceled += OnAttack;
    }

    private void OnDisable()
    {
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;
        lookAction.action.Disable();
        clickAction.action.performed -= OnAttack;
        clickAction.action.canceled -= OnAttack;
        clickAction.action.Disable();
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
    private void OnAttack(InputAction.CallbackContext ctx)
    {
       // Debug.Log(ctx.phase);
        if (ctx.performed && Launcher != null)
        {
            Launcher.Clicking(); 
        }

        if (ctx.canceled && Launcher!= null)
        {
            Launcher.Clicked();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CubeSys != null)
        {
            Mover = CubeSys.transform.parent.GetComponentInParent<CanMove>();
            Launcher = CubeSys.transform.parent.GetComponentInParent<Launcher>();
            Debug.Log(Launcher);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LaCatapult != null)
            LaCatapult.ReceiveLookInput(mouseDelta);
        if (Mover != null)
            Mover.UpdateInput();
        if (Launcher != null)
            Launcher.LaunchUpdate();
    }
}

