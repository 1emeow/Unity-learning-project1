using UnityEngine;
using UnityEngine.InputSystem;

public class CatapultController : MonoBehaviour, CanMove
{
    [Header("Parts")]
    [SerializeField] private Transform baseYaw;
    [SerializeField] private Transform barrelPitch;
    [SerializeField] private Animator _CatapiltAnimator;
    public Transform GetBarrel()
    {
        return barrelPitch;
    }

[Header("Rotation")]
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float minPitch = -45f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private float minYaw = -65f;
    [SerializeField] private float maxYaw = 65f;


    [Header("Movement")]
    [SerializeField] private PlayerInput _catapiltInput;
    public Vector3 move3;
    public Vector2 move;
    public float _speed = 1;

    private float yaw;
    private float pitch;

    protected void OnEnable()
    {
        if (_catapiltInput != null || _catapiltInput.currentActionMap != null)
            return;
        {
            _catapiltInput.currentActionMap.FindAction("Movement").performed -= OnMove;
            _catapiltInput.currentActionMap.FindAction("Movement").performed += OnMove;
            _catapiltInput.currentActionMap.Enable();
        }
        }

        protected void OnDisable()
    {
        if (_catapiltInput == null || _catapiltInput.currentActionMap == null)
            return;
        {
            _catapiltInput.currentActionMap.FindAction("Movement").performed -= OnMove;
            _catapiltInput.currentActionMap.Disable();
        }
    }
  private void OnMove(InputAction.CallbackContext context)
  {

  }
    public void UpdateInput()
    {
        move = _catapiltInput.currentActionMap.FindAction("Movement").ReadValue<Vector2>();
        move3 = new Vector3(move.x, 0, move.y);
        // Debug.Log(move3);

        if (move3.magnitude > 0)
        {
            transform.Translate(move3 * Time.deltaTime * _speed, Space.World);
        }
    }

public void ReceiveLookInput(Vector2 lookDelta)
    {
        yaw += lookDelta.x * sensitivity;
        pitch -= lookDelta.y * sensitivity;

        baseYaw.localRotation = Quaternion.Euler(0f, 0f, yaw);
        barrelPitch.localRotation = Quaternion.Euler(0f, pitch, 0f);


    }

    private void Start()
    {
        yaw = baseYaw.localRotation.z;
        pitch = barrelPitch.localRotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _catapiltInput.currentActionMap.FindAction("Movement").performed += OnMove;
    }
    private void Update()
    {
        if (yaw > maxYaw)
        {
            yaw = maxYaw;
        }
        else if (yaw < minYaw)
        {
            yaw = minYaw;
        }
        if (pitch > maxPitch)
        {
            pitch = maxPitch;
        }
        else if (pitch < minPitch)
        {
            pitch = minPitch;
        }


    }
}
