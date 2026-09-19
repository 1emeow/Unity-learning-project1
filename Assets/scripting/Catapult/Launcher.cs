using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Launcher : MonoBehaviour
{
    private Vector3 InitialPosition;
    public float PullDistance = 0f;
    [SerializeField]
    private float MaximalPullDistance;
    private bool Launched;
    private float PullSpeed = 0.01f;
    private bool Launching;
    public Vector3 velocity; 
    public GameObject Backlimit;
    public GameObject Frontlimit;
    public float LaunchFactor = 10f ;
    private GameObject Towed;
    private List<CrosshairFrame> CrosshairComponents = new List<CrosshairFrame>();
    private List<HorizontalCrosshairFrame> HorizontalCrosshairComponents = new List<HorizontalCrosshairFrame>();
    private GameObject Launchable;
    private Rigidbody LaunchableBody;
    private Transform _verticalParent;
    private Transform _horizontalParent;
    private GameObject _verticalCrosshair;
    private GameObject _horizontalCrosshair;
    public GameObject _horizontalCrosshairType;
    public GameObject _verticalCrosshairType;
    private bool HasStartedClicking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitialPosition = Frontlimit.transform.localPosition; //point de départ de la langue
        MaximalPullDistance = Mathf.Abs(InitialPosition.x - Backlimit.transform.localPosition.x);  //la distance en valeur absolue entre la position initiale de la langue et du mur de fond
        _verticalParent = transform.parent;
        if (_verticalParent != null)
            _horizontalParent = _verticalParent.parent;
    }
    public void Clicking() //tant qu'on clique
    {
        HasStartedClicking = true;
        Launching = true;
    }
    public void Clicked() //une fois que le clic est lâché
    {
        if (HasStartedClicking)
        { 
        Launched = true;
        Launching = false;
        StartCoroutine(RemoveCrosshairRoutine());
            if (GetComponentInChildren<CanBePicked>() != null) //si l'objet a les fonctions indiquées dans l'interface CanBePicked
            {
                CanBePicked LaunchableScript = GetComponentInChildren<CanBePicked>();
                LaunchableBody = ((MonoBehaviour)LaunchableScript).gameObject.GetComponentInChildren<Rigidbody>();
                LaunchableBody.isKinematic = false;
                //la vitesse est établie par un vecteur correpondant au rapport de distance entre la langue et le mur de fond. Elle est toujours proportionnelle au lanceur
                LaunchableBody.linearVelocity = (Frontlimit.transform.position - transform.position).normalized * (Frontlimit.transform.position - transform.position).magnitude * LaunchFactor;
                LaunchableScript.IsReleased();
            }
            HasStartedClicking = false;
        }
    }
    private IEnumerator RemoveCrosshairRoutine()
    {
        foreach (CrosshairFrame frame in CrosshairComponents)
            frame.ReinitializeEffect();
        yield return new WaitForSeconds(0.1f);
        Destroy(_verticalCrosshair);
        Destroy(_horizontalCrosshair);
        CrosshairComponents.Clear();
        HorizontalCrosshairComponents.Clear();
    }
    /* Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, (Frontlimit.transform.position - transform.position).normalized * 5f, Color.red, 5f);
    }*/
    public void LaunchUpdate() //fonction appelée par le GeneralInputCommand
    {
        if (Launching)
        {
            PullDistance += PullSpeed * Time.deltaTime;
            PullDistance = Mathf.Clamp(PullDistance, 0, MaximalPullDistance);
            transform.localPosition = InitialPosition + Vector3.left * PullDistance;
            foreach (CrosshairFrame frame in CrosshairComponents)
            {
                frame.ChargeEffect();
                if (frame._chargeSpeed == 0f)
                frame._chargeSpeed = frame.MaximalPullDistance / (MaximalPullDistance * 100);
            }
            foreach (HorizontalCrosshairFrame frame in HorizontalCrosshairComponents)
            {
                frame.ChargeEffect();
                if (frame._chargeSpeed == 0f)
                    frame._chargeSpeed = frame.MaximalPullDistance / (MaximalPullDistance * 100);
            }
        }
        if (Launched)
        {
            transform.localPosition = InitialPosition + Vector3.left * PullDistance;
            PullDistance -= PullSpeed * Time.deltaTime *20;
            velocity = Vector3.left * PullDistance;

        }
        if (PullDistance <= 0)
        {
            PullDistance = 0;
            Launched = false;
        }
        if (GetComponentInChildren<CanBePicked>() != null)
        {
            if (_horizontalCrosshair == null && _horizontalParent != null)
            {
                _horizontalCrosshair = Instantiate(_horizontalCrosshairType, Frontlimit.transform.position + Frontlimit.transform.right * 2f, Frontlimit.transform.rotation * Quaternion.Euler(0f, 90f, 90f));
                _horizontalCrosshair.transform.SetParent(_horizontalParent);

            }
            if (_verticalCrosshair == null && _verticalParent != null)
            {
                _verticalCrosshair = Instantiate(_verticalCrosshairType, Frontlimit.transform.position + Frontlimit.transform.right * 2f, Frontlimit.transform.rotation * Quaternion.Euler(0f, 90f, 90f));
                _verticalCrosshair.transform.SetParent(_verticalParent);
                    foreach (Transform child in _verticalCrosshair.transform)
                    {
                        CrosshairFrame frame = child.GetComponent<CrosshairFrame>();

                        if (frame != null)
                        {
                            CrosshairComponents.Add(frame);
                        }
                    }
                foreach (Transform child in _verticalCrosshair.transform)
                {
                    HorizontalCrosshairFrame frame = child.GetComponent<HorizontalCrosshairFrame>();

                    if (frame != null)
                    {
                        HorizontalCrosshairComponents.Add(frame);
                    }
                }
            }
            }
        }
    }
