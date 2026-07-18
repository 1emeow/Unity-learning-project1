using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _generalInputCommand;
    private General_Input_Command InputCommandScript;
    [SerializeField]
    private bool Paused;
    private bool PausedStatus;
    private float firststart = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputCommandScript = _generalInputCommand.GetComponent<General_Input_Command>();
        InputCommandScript.StartGame = false;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
      if (Paused != PausedStatus)
        {
            if (Paused)
            {
                InputCommandScript.StartGame = false;
                Time.timeScale = 0f;
            }
            else
            {
                StartCoroutine(StartGame());
            }
                PausedStatus = Paused;

        }
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
