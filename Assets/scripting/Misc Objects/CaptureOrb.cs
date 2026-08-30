using UnityEngine;

public class CaptureOrb : MonoBehaviour
{
    private GameObject _cubeSys;
    private GameManagerScript _gameManagerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }
    void OnCollisionEnter(Collision collision)
    {
        CubeScript scriptofcube = collision.gameObject.GetComponent<CubeScript>();
        if (scriptofcube != null)
        {
            _cubeSys = scriptofcube.transform.root.gameObject;
            _gameManagerScript.GetANewCube(_cubeSys);
            Destroy(this.gameObject);
        }
    }
}
