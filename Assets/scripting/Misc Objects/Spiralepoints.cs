using UnityEngine;
using System.Collections;

/// <summary>
/// Gère les spirales de points : l'activation de leur mouvement et leur récolte.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SpiralePoints : MonoBehaviour
{
    #region Variables - Configuration
    [Header("Réglages")]
    public float attractionSpeed = 5f;
    public float detectionRadius = 5f;
    public int valeurPointsBase = 1;

    private Rigidbody rb;
    private GameObject playerObject; // Assigné par le SearchProcess
    private SearchProcess searchRadiusScript;
    private bool pointObtenu = false;
   // private bool estDetruit = false;
    private bool estAttirée = false;
    private bool nebougeplus = false;
    private CubeController _cubeController;
    #endregion

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Start()
    {
        rb.linearDamping = 3f;
      /* la prochaine fois peut-être  // On s'enregistre auprès du radar du joueur
        GameObject potentialPlayer = GameObject.Find("Le Cube");
        if (potentialPlayer != null)
        {
            searchRadiusScript = potentialPlayer.GetComponentInChildren<SearchProcess>();
            if (searchRadiusScript != null) searchRadiusScript.Ofinterestlist.Add(this.gameObject);
        }
      */
    }

    private void Update()
    {
       /* if (pointObtenu && !estDetruit)
        {
            estDetruit = true;
            StartCoroutine(CollectAndDestroySequence());
            return;
        }*/

        // L'aspiration ne se déclenche que si playerObject a été défini par le radar
        if (playerObject != null && !pointObtenu)
        {
            HandlePhysicsMovement();
        }
    }

    /// <summary>
    /// PORTE D'ENTRÉE : Appelée par le SearchProcess quand la spirale entre dans le radar.
    /// </summary>
    public void ActiverAspiration(GameObject joueur)
    {
        playerObject = joueur;
    }

    private void HandlePhysicsMovement()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance > 0.1f)
        {
            Vector3 direction = playerObject.transform.position - transform.position;
            rb.linearVelocity = direction.normalized * attractionSpeed;
        }
        if (!estAttirée)
        {
            estAttirée = true;
            rb.linearDamping = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeScript>() != null && !pointObtenu)
        {
            _cubeController = collision.gameObject.GetComponent<CubeController>();
            _cubeController.canjump -= 1f;
            pointObtenu = true;
            StartCoroutine(CollectAndDestroySequence());
        }
        if (collision.gameObject.layer == 8 && !nebougeplus)
        {
            rb.useGravity = false;
            nebougeplus = true;
        }
        if (collision.gameObject.GetComponent<CubeScript>() == null && !estAttirée)
            rb.linearVelocity = Vector3.zero;
        else if (estAttirée)
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
    }

    private IEnumerator CollectAndDestroySequence()
    {
     //   if (searchRadiusScript != null) searchRadiusScript.Ofinterestlist.Remove(this.gameObject); désuet, comme la liste

        int finalScore = (transform.localScale != Vector3.one) ? valeurPointsBase * 2 : valeurPointsBase;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(finalScore);

        yield return new WaitForFixedUpdate();
        Destroy(this.gameObject);
    }
}