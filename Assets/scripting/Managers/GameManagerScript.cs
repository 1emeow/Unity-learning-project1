using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] //fait apparaitre l'élément dans l'inspecteur un élément normalement discret, il n'apparait pas pour les autres scripts
    private General_Input_Command InputCommandScript;
    [SerializeField]
    private CameraManager CameraManager;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private Canvas _canvasMenu;
    [SerializeField]
    private MenuScript _menuScript;
    [SerializeField]
    private CubesRemainingTextDisplay _cubesRemainingTextDisplay;
    [SerializeField]
    private GameObject Catapult;
    private Transform Spawner;
    public float MaxCubes = 2f;
    private List<GameObject> CubesTable = new List<GameObject>(); //liste des cubes existants
    private GameObject _cubeInstance;
    [SerializeField]
    private GameObject CubesysObject;
    public bool Paused;
    public float RestartTimer = 0.3f;
    private float firststart = 1f;
    public bool WasJumpBufferReached; //retient pour tous les cubes si on a le buff de saut
    public bool WasMoveSetterReached; //idem pour le mouvement

    void Awake()
    {
        Spawner = Catapult.GetComponentInChildren<SpawnPosition>().transform;
        if (Spawner != null)
        InputCommandScript.PausedStatusChanged.AddListener(PausedStatusChanged);
        InputCommandScript.RestartGame.AddListener(RestartGame);//indique au game manager de s'inscrire à l'évènement de l'input command manager
        InputCommandScript.StartGame = false;
        SpawnFunction();
    }
    private void SpawnFunction() //fonction qui gère la génération de cubes
    {
        _cubeInstance = Instantiate(CubesysObject, Spawner.position, Spawner.rotation);
        CubeSys cubeScript = _cubeInstance.GetComponent<CubeSys>();
        _cubeInstance.GetComponentInChildren<CubeController>().hasreceivedjumpbuff = WasJumpBufferReached;
        _cubeInstance.transform.SetParent(Spawner.parent.GetComponentInChildren<Launcher>().transform, true);
        _cubeInstance.transform.localScale = Vector3.one * 0.01f;
        //InputCommandScript.LaCatapult = Catapult.GetComponent<CatapultController>();
        CubesTable.Add(_cubeInstance);
        if (cubeScript != null)
        {
            cubeScript._playerCatapult = cubeScript.transform.root.GetComponent<CatapultController>();
            InputCommandScript.LaCatapult = cubeScript._playerCatapult;
            cubeScript.UpdateCubeState.AddListener(UpdateCubeState); //indique au game manager de s'inscrire à l'évènement de l'input command manager
            CameraManager.CubeListening(cubeScript); //déclenche la fonction du cameramanger qui permet de s'inscrire à l'évènement du script du cube, on le fait ici parce que le cube est généré ici
            InputCommandScript.CubeListening(cubeScript);
        }
    }
// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{
        StartCoroutine(StartGame());
}
private void UpdateCubeState(CubeSys cubesys) //permet de savoir si le cube est dormant et d'agir en conséquence
{
    if ((cubesys.Dormant || cubesys.Iamdead) && CubesTable.Count < MaxCubes)
    {
        SpawnFunction();
    }
    else if ((cubesys.Dormant || cubesys.Iamdead) && CubesTable.Count >= MaxCubes)
    {
        Debug.Log("The maximum amount of cubes has been reached");
    }
        if (cubesys.Released && !cubesys.Dormant)
        {
            _cubesRemainingTextDisplay.valeurtotale = 2 - CubesTable.Count;
            _cubesRemainingTextDisplay.RefreshDisplay();
        }
    }
public void PausedStatusChanged() //déclenche la pause
{
    Paused = !Paused;
    if (Paused)
    {
        InputCommandScript.StartGame = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _canvas.enabled = false;
            _canvasMenu.enabled = true;
            Time.timeScale = 0f; //empêche le temps du jeu de s'écouler
    }
    else
    {
        StartCoroutine(StartGame()); //arrête la pause
    }
}
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
private IEnumerator StartGame()
{
    _canvas.enabled = true;
    _canvasMenu.enabled = false;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    yield return new WaitForSeconds(firststart); //très important au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
    firststart = 0;
    yield return new WaitForSecondsRealtime(RestartTimer); //le temps ne s'écoule pas, on veut donc le temps réel. Ce temps de reprise modulable est là pour permettre au joueur de se concentrer à nouveau
    Time.timeScale = 1f;
    InputCommandScript.StartGame = true;
}
    public void GetANewCube(GameObject picked)
    {
        Destroy(picked);
        CubesTable.Remove(picked);
        _cubesRemainingTextDisplay.valeurtotale = 2 - CubesTable.Count;
        _cubesRemainingTextDisplay.RefreshDisplay();
        SpawnFunction();
    }
}
