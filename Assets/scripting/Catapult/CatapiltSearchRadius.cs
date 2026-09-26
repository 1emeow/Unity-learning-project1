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
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            CanBePicked _canBePicked = other.GetComponentInParent<CanBePicked>();
            if (_canBePicked != null && _canBePicked.pickupable == true)
            {
                GameObject _pickupable = ((MonoBehaviour)_canBePicked).gameObject;
                if (!_pickupable.GetComponent<CubeSys>().Dormant)
                    _gameManager.GetANewCube(_pickupable.gameObject);
            }
        }
           // other.attachedRigidbody.mass = 1e-07f; //attachedRigidbody va chercher le Rb dans tous les objets affiliés à l'objet
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            CanBePicked _canBePicked = other.GetComponentInParent<CanBePicked>();
            if (_canBePicked != null && !_canBePicked.pickupable && other.GetComponent<Rigidbody>() != null)
            {
                _canBePicked.pickupable = true;
                //    other.attachedRigidbody.mass = 20f;

                MonoBehaviour _monoCanBePicked = (MonoBehaviour)_canBePicked;
                if (_monoCanBePicked is CubeSys cubeSys)
                {
                    cubeSys.Detached = true;
                    cubeSys.UpdateCubeState.Invoke(cubeSys);
                }
            }
        }
    }
}
