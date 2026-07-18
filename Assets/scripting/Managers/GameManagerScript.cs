using UnityEngine;
using System.Collections;
public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _generalInputCommand;
    [SerializeField]
    private General_Input_Command InputCommandScript;
    public bool Paused;
    private float firststart = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputCommandScript.PausedStatusChanged.AddListener(PausedStatusChanged);
        InputCommandScript.StartGame = false;
        StartCoroutine(StartGame());
    }
  private void PausedStatusChanged()
    {
        Paused = !Paused;
        Debug.Log(Paused);
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
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        InputCommandScript.StartGame = true;
    }
}
