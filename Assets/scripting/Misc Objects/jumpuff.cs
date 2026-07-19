using UnityEngine;

public class jumpuff : MonoBehaviour
{
    public bool Touche;
    public CubeScript CubeProcess;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        if (Touche == false)
        {
            if (collision.gameObject.GetComponent<CubeScript>() != null)
            {
                CubeProcess = collision.gameObject.GetComponent<CubeScript>();
                CubeProcess.jumpbuffer = this.gameObject;
                StartCoroutine(CubeProcess.Coroutineofcollisionjumpbuffer());
                Touche = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
