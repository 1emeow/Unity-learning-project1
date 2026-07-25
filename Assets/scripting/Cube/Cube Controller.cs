using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour, CanMove //cet object possède les fonctions indiquées dans l'interface CanMove
{
    [SerializeField]
    private PlayerInput _cubeInput;
    private bool jump;
    public bool canjump;
    public bool hasreceivedjumpbuff;
    private Rigidbody bodycube;
    private Vector3 surfaceNormal = Vector3.up; //new Vector3 (0,1,0);

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
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            canjump = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != null)
        {
            canjump = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        surfaceNormal = collision.GetContact(0).normal;
    }
    public void UpdateInput()
    {
        if (_cubeInput.currentActionMap.FindAction("saut").WasPressedThisFrame() && hasreceivedjumpbuff && canjump)
        {
            float enfoncementValue = Vector3.Dot(bodycube.linearVelocity, -surfaceNormal); //au cas où on a envie d'avoir un rebond moins important
            if (enfoncementValue > 0)
            {
                bodycube.linearVelocity += surfaceNormal * enfoncementValue; //absorbe la vitesse du cube 
            }

            bodycube.linearVelocity += surfaceNormal * 10;
        }
    }
    void Start()
    {
        bodycube = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
