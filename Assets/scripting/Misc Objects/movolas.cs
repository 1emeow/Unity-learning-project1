using UnityEngine;

public class Movolas : MonoBehaviour
{
    public bool Touche;
    public CubeScript CubeProcess;
    public GameManagerScript _gameManagerScript;

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
                CubeProcess.moveSetter = this.gameObject;
                _gameManagerScript.WasMoveSetterReached = true;
                StartCoroutine(CubeProcess.Coroutineofcollisionmovesetter());
                Touche = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
