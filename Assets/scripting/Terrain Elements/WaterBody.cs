using UnityEngine;

public class WaterBody : MonoBehaviour
{
    [SerializeField]
    private GameObject _water;
    [SerializeField]
    private GameObject _waterBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water.GetComponent<MeshRenderer>().enabled = true;
        _waterBody.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
