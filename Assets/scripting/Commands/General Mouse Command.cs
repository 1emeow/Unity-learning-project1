using UnityEngine;
using UnityEngine.InputSystem;

public class General_Mouse_Command : MonoBehaviour
{
    public Vector2 mouseDelta;
    public bool commandSystemEnabler;
    public InputActionReference lookAction;
    public CatapultController LaCatapult;
    public InputActionReference clickAction;
    public CubeSys CubeSys;
    private CanMove Mover;
    
    public void OnEnable ()
    {
        lookAction.action.Enable();
        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;
        clickAction.action.Enable();
        clickAction.action.performed += OnClick;
        clickAction.action.canceled += OnClick;
    }

    private void OnDisable()
    {
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;
        lookAction.action.Disable();
        clickAction.action.performed -= OnClick;
        clickAction.action.canceled -= OnClick;
        clickAction.action.Disable();
    }
    public void UpdateCubeState()
    {
        if (CubeSys.transform.parent != null)
        {
            Mover = CubeSys.transform.parent.GetComponentInParent<CanMove>();
        }
        else
            Mover = CubeSys;
    }
    private void OnLook(InputAction.CallbackContext ctx)
    {
        mouseDelta = ctx.ReadValue<Vector2>();
    }
    private void OnClick(InputAction.CallbackContext ctx)
    {
        if (CubeSys != null)
        {
            CubeSys.ReceiveClickInput();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mover = CubeSys.transform.parent.GetComponentInParent<CanMove>();
    }

    // Update is called once per frame
    void Update()
    {
        LaCatapult.ReceiveLookInput(mouseDelta);
        Mover.UpdateInput();
        Debug.Log(Mover);
    }
}

