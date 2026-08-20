using UnityEngine;

public class PlayableEnvironment : MonoBehaviour
{
    private Rigidbody DampableObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider r = GetComponent<Collider>();

        if (r != null)
        {
            Debug.Log($"Size: {r.bounds.size} meters");
            Debug.Log(r.gameObject.name);
        }
        Renderer s = GetComponentInParent<Renderer>();

        if (s != null)
        {
            Debug.Log($"Size of surface: {s.bounds.size} meters");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        DampableObject = other.GetComponent<Rigidbody>(); //l'objet est dans la zone de jeu
        if (DampableObject != null)
            DampableObject.linearDamping -= 1f; 
    }
    void OnTriggerExit(Collider other)
    {
        DampableObject = other.GetComponent<Rigidbody>(); //l'objet n'est plus dans la zone de jeu
        if (DampableObject != null)
            DampableObject.linearDamping += 1f;
    }
}
