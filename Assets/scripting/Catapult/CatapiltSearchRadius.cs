using UnityEngine;

public class CatapiltSearchRadius : MonoBehaviour
{
    [SerializeField]
    private GameManagerScript _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        CanBePicked _canBePicked = other.GetComponentInParent<CanBePicked>();
        if (_canBePicked != null && _canBePicked.pickupable == true)
        {
            GameObject _pickupable = ((MonoBehaviour)_canBePicked).gameObject;
            if (!_pickupable.GetComponent<CubeSys>().Dormant)
            _gameManager.GetANewCube(_pickupable.gameObject);
        }
           // other.attachedRigidbody.mass = 1e-07f; //attachedRigidbody va chercher le Rb dans tous les objets affiliés à l'objet
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() && other.GetComponentInParent<CanBePicked>() != null)
            other.GetComponentInParent<CanBePicked>().pickupable = true;
        //    other.attachedRigidbody.mass = 20f;
    }
}
