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
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 45f;
    [SerializeField] private float minYaw = -65f;
    [SerializeField] private float maxYaw = 65f;


    [Header("Movement")]
    [SerializeField] private PlayerInput _catapiltInput;
    public Vector3 move3;
    public Vector2 move;
    public float _speed = 1;
    public bool canmove;
    private float yaw;
    private float pitch;

    protected void OnEnable()
    {
        if (_catapiltInput != null || _catapiltInput.currentActionMap != null)
            return;
        {
            _catapiltInput.currentActionMap.Enable();
        }
        }

        protected void OnDisable()
    {
        if (_catapiltInput == null || _catapiltInput.currentActionMap == null)
            return;
        {
            _catapiltInput.currentActionMap.Disable();
        }
    }
    public void UpdateInput() //Fonction appelée par le General Input Command pour éviter d'avoir un update en trop
    {
        if (canmove && _catapiltInput != null)
        {
            move = _catapiltInput.currentActionMap.FindAction("Movement").ReadValue<Vector2>(); //on va chercher à savoir si le bouton appuyé correspond à l'action mouvement dans l'input actions
            move3 = new Vector3(move.x, 0, move.y); //on fait un vecteur à partir des boutons 
            if (move3.magnitude > 0)
            {
                transform.Translate(move3 * Time.deltaTime * _speed, Space.World); //la catapulte bouge selon le vecteur
            }
        }
    }

    public void ReceiveLookInput(Vector2 lookDelta)
    {
        yaw += lookDelta.x * sensitivity;
        pitch -= lookDelta.y * sensitivity;
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        baseYaw.localRotation = Quaternion.Euler(0f, 0f, yaw);
        barrelPitch.localRotation = Quaternion.Euler(0f, pitch, 0f);
    }

    private void Start()
    {
        yaw = baseYaw.localRotation.z;
        pitch = barrelPitch.localRotation.x;
    }
}
