using UnityEngine;
using System.Collections;

public class CaptureOrb : MonoBehaviour
{
    private GameObject _cubeSys;
    private GameManagerScript _gameManagerScript;
    private GameObject _captureCube;
    private bool isNowCapturing;
    private bool CanSpawn;
    [SerializeField] private GameObject _captureParticles;
    [SerializeField] private GameObject _innerOrb;
    [SerializeField] private Texture CaptureWhite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }
    void OnCollisionEnter(Collision collision)
    {
        CubeScript scriptofcube = collision.gameObject.GetComponent<CubeScript>();
        if (scriptofcube != null && !isNowCapturing)
        {
            _captureCube = scriptofcube.gameObject;
            StartCoroutine(CaptureProcess());
            isNowCapturing = true;
        }
    }
    private IEnumerator CaptureProcess()
    {
        HasStorableData _pickedObject = GetComponentInChildren<HasStorableData>();
        if (_pickedObject != null && TemporaryInventory.Instance != null)
        {
            PickedUpData data = _pickedObject.StoreData(); //data est l'ensemble des données mentionnées dans la StoreData()
            TemporaryInventory.Instance.InventoryList.Add(data);
            string json = JsonUtility.ToJson(data);
        }
        else
        {
          CanSpawn = true;
        }

            _captureCube.GetComponent<Rigidbody>().isKinematic = true;
        _cubeSys = _captureCube.GetComponentInParent<CubeSys>().gameObject;
        _captureParticles.SetActive(true);
        _innerOrb.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        _captureCube.GetComponent<Renderer>().material.SetTexture("_BaseMap", CaptureWhite);
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) //on va chercher le renderer dans chaque child
        {
            if (renderer.transform.IsChildOf(_captureParticles.transform))
            {
                continue; //on lui dit de ne pas chercher ici
            }
                foreach (Material material in renderer.materials)
                {
                    if (material.HasProperty("_BaseMap")) //on vérifie que le child material possède une texture de base
                    {
                        material.SetTexture("_BaseMap", CaptureWhite);
                        if (material.HasProperty("_MetallicGlossMap")) //on vérifie qu'il a une texture métallique
                            material.SetTexture("_MetallicGlossMap", CaptureWhite);
                        material.color = Color.white;
                    }

                    if (material.HasProperty("_EmissionMap")) //on vérifie que le child material possède une texture émissive
                    {
                        material.SetTexture("_EmissionMap", CaptureWhite);
                        material.SetColor("_EmissionColor", Color.white);
                }
                }
        }
            yield return new WaitForSeconds(1f);
        if (CanSpawn)
        {
            if (TemporaryInventory.Instance.InventoryList.Count > 0)
            {
                PickedUpData data = TemporaryInventory.Instance.InventoryList[0];
                GameObject prefab = PrefabDictionary.Instance.GetPrefab(data.prefabId);
                if (prefab != null)
                {
                    GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
                    HasStorableData dataLoader = instance.GetComponentInChildren<HasStorableData>();
                    if (dataLoader != null)
                    { 
                    dataLoader.LoadData(data);
                    }
                    //   TemporaryInventory.Instance.InventoryList.RemoveAt(selectedSlot);
                }
            }
        }
        _gameManagerScript.GetANewCube(_cubeSys);
            Destroy(this.gameObject);
        }
    }
