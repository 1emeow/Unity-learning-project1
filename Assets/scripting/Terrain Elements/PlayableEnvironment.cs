using UnityEngine;

public class PlayableEnvironment : MonoBehaviour
{
    private Rigidbody DampableObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        DampableObject = other.GetComponent<Rigidbody>(); //l'objet est dans la zone de jeu
        if (DampableObject != null)
            DampableObject.linearDamping -= 1f; //un clamp pour ne pas avoir d'amortissement négatif
    }
    void OnTriggerExit(Collider other)
    {
        DampableObject = other.GetComponent<Rigidbody>(); //l'objet n'est plus dans la zone de jeu
        if (DampableObject != null)
            DampableObject.linearDamping += 1f;
    }
}
