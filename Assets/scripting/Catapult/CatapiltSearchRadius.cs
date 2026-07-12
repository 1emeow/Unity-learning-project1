using UnityEngine;

public class CatapiltSearchRadius : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeScript>())
            other.attachedRigidbody.mass = 1e-07f; //attachedRigidbody va chercher le Rb dans tous les objets affiliés à l'objet
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeScript>())
                other.attachedRigidbody.mass = 20f;
    }
}
