using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{
    [SerializeField] //fait apparaitre l'élément dans l'inspecteur un élément normalement discret, il n'apparait pas pour les autres scripts
    private Menu_General_Input_Command InputCommandScript;
    [SerializeField]
    private MenuCameraManager CameraManager;
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
    public float RestartTimer = 1f;
    private float firststart = 1f;
    [SerializeField]
    private Canvas _canvasMenu;
    [SerializeField]
    private Canvas _levelsMenu;
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
        CubesTable.Add(_cubeInstance);
        if (cubeScript != null)
        {
            cubeScript._playerCatapult = cubeScript.transform.root.GetComponent<CatapultController>();
            InputCommandScript.LaCatapult = cubeScript._playerCatapult;
            InputCommandScript.CubeListening(cubeScript);
        }
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine());
    }
    public IEnumerator LoadGameRoutine()
    {
        CameraManager.Isloading = true;
        yield return new WaitForSeconds(0.1f);
        _levelsMenu.enabled = false;
        yield return new WaitForSeconds(1f); //très au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
            SceneManager.LoadScene("Level One");
        CameraManager.CurrentCamera = CameraManager.SecondCam;
    }
    public void NewGameMenu()
    {
        StartCoroutine(NewGameMenuRoutine());
    }
    public IEnumerator NewGameMenuRoutine()
    {
        CameraManager.CurrentCamera = CameraManager.OtherCamera;
        yield return new WaitForSeconds(0.1f);
        _canvasMenu.enabled = false;
        yield return new WaitForSeconds(1f); //très au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
        _levelsMenu.enabled = true;
    }
    public void MainMenu()
    {
        StartCoroutine(MainMenuRoutine());
    }
    public IEnumerator MainMenuRoutine()
    {
        CameraManager.CurrentCamera = CameraManager.FirstCam;
        yield return new WaitForSeconds(0.1f);
        _levelsMenu.enabled = false;
        yield return new WaitForSeconds(1f); //très au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
        _canvasMenu.enabled = true;
    }
    public void QuitGame()
    {
        StartCoroutine(QuitGameRoutine());
    }
    public IEnumerator QuitGameRoutine()
    {
        CameraManager.CurrentCamera = CameraManager.SecondCam;
        yield return new WaitForSeconds(0.1f);
        _canvasMenu.enabled = false;
        _levelsMenu.enabled = false;
        yield return new WaitForSeconds(1f); //très au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
        Application.Quit();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
{
        StartCoroutine(StartGame());
}
private void PausedStatusChanged() //déclenche la pause
{
}
private void RestartGame()
{
 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
// Update is called once per frame
void Update()
{
}
    private IEnumerator StartGame()
{
    yield return new WaitForSeconds(firststart); //très au lancement du jeu sinon la catapult fait n'importe quoi en suivant le curseur ce qui détruit tout
    firststart = 0;
    yield return new WaitForSecondsRealtime(RestartTimer); //le temps ne s'écoule pas, on veut donc le temps réel. Ce temps de reprise modulable est là pour permettre au joueur de se concentrer à nouveau
    Time.timeScale = 1f;
    InputCommandScript.StartGame = true;
}
}
