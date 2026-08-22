using UnityEngine;

public class PlayableEnvironment : MonoBehaviour
{
    private Rigidbody DampableObject;

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
