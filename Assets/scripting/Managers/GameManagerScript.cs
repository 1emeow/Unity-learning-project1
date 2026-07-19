using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _generalInputCommand;
    [SerializeField]
    private General_Input_Command InputCommandScript;
    [SerializeField]
    private CameraManager CameraManager;
    [SerializeField]
    private GameObject Catapult;
    private Transform Spawner;
    private List<GameObject> CubesTable = new List<GameObject>();
    private GameObject _cubeInstance;
    [SerializeField]
    private GameObject CubesysObject;
    public bool Paused;
    public float RestartTimer = 1f;
    private float firststart = 1f;

    void Awake()
    {
        Spawner = Catapult.GetComponentInChildren<SpawnPosition>().transform;
        Debug.Log(Catapult);
        if (Spawner != null)
            SpawnFunction();
    }
    private void SpawnFunction()
    {
        InputCommandScript.PausedStatusChanged.AddListener(PausedStatusChanged);
        InputCommandScript.StartGame = false;
        StartCoroutine(StartGame());
        _cubeInstance = Instantiate(CubesysObject, Spawner.position, Spawner.rotation);
        CubeSys cubeScript = _cubeInstance.GetComponent<CubeSys>();
        _cubeInstance.transform.SetParent(Spawner.parent.GetComponentInChildren<Launcher>().transform, true);
        _cubeInstance.transform.localScale = Vector3.one * 0.01f;
        CubesTable.Add(_cubeInstance);
        if (cubeScript != null)
        {
            cubeScript.UpdateCubeState.AddListener(UpdateCubeState);
            CameraManager.CubeListening(cubeScript);
            InputCommandScript.CubeListening(cubeScript);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void UpdateCubeState(CubeSys cubesys)
    {

    }
  private void PausedStatusChanged()
    {
        Paused = !Paused;
        if (Paused)
        {
            InputCommandScript.StartGame = false;
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine(StartGame());
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator StartGame()
    {             
        yield return new WaitForSeconds(firststart);
        firststart = 0;
        yield return new WaitForSecondsRealtime(RestartTimer);
        Time.timeScale = 1f;
        InputCommandScript.StartGame = true;
    }
}
