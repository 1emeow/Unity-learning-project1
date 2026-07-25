using UnityEngine;

public class jumpuff : MonoBehaviour, IsACataBuffer
{
    public bool Touche;
    public CubeScript _cubeProcess;
    public GameManagerScript _gameManagerScript;
    private CubeController _cubeController;

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
                _cubeProcess = collision.gameObject.GetComponent<CubeScript>();
                _cubeController = collision.gameObject.GetComponent<CubeController>();
                _cubeProcess.buffer = this.gameObject;
                _cubeController.hasreceivedjumpbuff = true;
                _gameManagerScript.WasJumpBufferReached = true;
                StartCoroutine(_cubeProcess.Coroutineofcollisionbuffer());
                Touche = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
