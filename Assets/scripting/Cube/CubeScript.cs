using UnityEngine;
using System.Collections;


public class CubeScript : MonoBehaviour
{
    public GameObject buffer;
    public Color normal;
    public Color buff;
    public Color catabuff;
    [SerializeField]
    private CubeController cubeController;
    [SerializeField]
    private bool cansling;

    void Start()
    {
    }
    void Update()
    {  
    }
    public IEnumerator Coroutineofcollisionbuffer() //ce qu'il se passe lorsqu'on rencontre un buff
    {
       yield return null;
        if (buffer.GetComponent<IsACubeBuffer>() != null)
        {
            this.GetComponent<MeshRenderer>().material.color = buff;
            this.GetComponent<Rigidbody>().linearVelocity = this.GetComponent<Rigidbody>().linearVelocity += new Vector3(0, 10, 0); // un effet pour montrer que c'est un buff de cube.
        }
        else if (buffer.GetComponent<IsACataBuffer>() != null)
        {
            this.GetComponent<MeshRenderer>().material.color = catabuff;
        }
                for (var i = buffer.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buffer.transform.GetChild(i).gameObject);
        }
        cubeController.canjump -= 1f;
        Destroy(buffer.GetComponent<BoxCollider>());
        yield return new WaitForSeconds(2.0f);
        this.GetComponent<MeshRenderer>().material.color = normal;
        Destroy(buffer);
    }
}