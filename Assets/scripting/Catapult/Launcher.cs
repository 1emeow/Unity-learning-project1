using UnityEngine;

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
    private GameObject Launchable;
    private Rigidbody LaunchableBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitialPosition = Frontlimit.transform.localPosition; //point de départ de la langue
        MaximalPullDistance = Mathf.Abs(InitialPosition.x - Backlimit.transform.localPosition.x);  //la distance en valeur absolue entre la position initiale de la langue et du mur de fond
    }
    public void Clicking() //tant qu'on clique
    {
        Launching = true;
    }
    public void Clicked() //une fois que le clic est lâché
    {
        Launched = true;
        Launching = false;
        if (GetComponentInChildren<CanBePicked>() != null) //si l'objet a les fonctions indiquées dans l'interface CanBePicked
        {
           CanBePicked LaunchableScript = GetComponentInChildren<CanBePicked>();
            LaunchableBody = ((MonoBehaviour)LaunchableScript).gameObject.GetComponentInChildren<Rigidbody>();
            LaunchableBody.isKinematic = false;
            //la vitesse est établie par un vecteur correpondant au rapport de distance entre la langue et le mur de fond. Elle est toujours proportionnelle au lanceur
            LaunchableBody.linearVelocity = (Frontlimit.transform.position - transform.position).normalized * (Frontlimit.transform.position - transform.position).magnitude * LaunchFactor;
            LaunchableScript.IsReleased();
        }
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
    }
}
