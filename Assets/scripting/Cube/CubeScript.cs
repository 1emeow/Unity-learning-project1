using UnityEngine;
using System.Collections;


public class CubeScript : MonoBehaviour
{
    public Material material1, material2;
    public GameObject jumpbuffer;
    public GameObject moveSetter;
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
    public IEnumerator Coroutineofcollisionjumpbuffer()
    {
        yield return null;
        this.GetComponent<MeshRenderer>().material.color = buff;
        this.GetComponent<Rigidbody>().linearVelocity = this.GetComponent<Rigidbody>().linearVelocity  += new Vector3(0, 10, 0); //* -2;
        Destroy(jumpbuffer.GetComponent<BoxCollider>());
        for (var i = jumpbuffer.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(jumpbuffer.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(2.0f);
        this.GetComponent<MeshRenderer>().material.color = normal;
        Destroy(jumpbuffer);
        cubeController.hasreceivedjumpbuff = true;

    }
    public IEnumerator Coroutineofcollisionmovesetter()
    {
        yield return null;
        this.GetComponent<MeshRenderer>().material.color = catabuff;
        Destroy(moveSetter.GetComponent<BoxCollider>());
        for (var i = moveSetter.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(moveSetter.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(2.0f);
        this.GetComponent<MeshRenderer>().material.color = normal;
        Destroy(moveSetter);
        cubeController.hasreceivedjumpbuff = true;
    }
}