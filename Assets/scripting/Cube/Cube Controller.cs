using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour, CanMove
{
    [SerializeField]
    private PlayerInput _cubeInput;
    private bool jump;
    public bool canjump;
    public bool hasreceivedjumpbuff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void OnEnable()
    {
        if (_cubeInput != null || _cubeInput.currentActionMap != null)
            return;
        {
            _cubeInput.currentActionMap.FindAction("saut").performed -= OnJump;
            _cubeInput.currentActionMap.FindAction("saut").performed += OnJump;
            _cubeInput.currentActionMap.Enable();
        }
    }

    protected void OnDisable()
    {
        if (_cubeInput == null || _cubeInput.currentActionMap == null)
            return;
        {
            _cubeInput.currentActionMap.FindAction("saut").performed -= OnJump;
            _cubeInput.currentActionMap.Disable();
        }
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {

    }
    public void UpdateInput()
    {
        //jump = _cubeInput.currentActionMap.FindAction("saut").IsPressed();
        // if (jump)
        if (_cubeInput.currentActionMap.FindAction("saut").WasPressedThisFrame() && hasreceivedjumpbuff)
        {
            //is on the ground and received a jump 
            this.GetComponent<Rigidbody>().linearVelocity = this.GetComponent<Rigidbody>().linearVelocity += new Vector3(0, 10, 0);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
