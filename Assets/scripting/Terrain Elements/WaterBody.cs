using UnityEngine;

public class WaterBody : MonoBehaviour
{
    [SerializeField]
    private GameObject _waterSurfaceEffect;
    [SerializeField]
    private GameObject _waterBody;
    private float surface;
    private float sinkValue;
    private Rigidbody sinkingCube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        surface = _waterBody.GetComponent<Collider>().bounds.max.y; //recherche le bord de l'objet avec bounds.max
        _waterSurfaceEffect.GetComponent<MeshRenderer>().enabled = true; //met en place le shader parce que dans l'éditeur c'est un enfer
        _waterBody.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CubeScript>() != null)
        {
            sinkingCube = other.attachedRigidbody;
            if (sinkingCube.linearVelocity.magnitude > 0.1) //on va vérifier que le cube bouge avant de le ralentir
            {
                sinkValue = Mathf.Abs(Mathf.Clamp(surface - sinkingCube.position.y, 1, 6));
                //  sinkingCube.linearVelocity *= 1 - ((sinkValue - 1) / 20); si on veut ralentir le cube jusqu'à l'arrêter
                sinkingCube.linearDamping = sinkValue - 1; //on va émousser la vitesse du cube avec un damping
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == sinkingCube)
        {
            sinkingCube.linearDamping = 0;
            sinkingCube = null;
        }
    }
}
