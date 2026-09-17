using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[Serializable] //permet de la modifier dans l'inspecteur
public class Buttonslist //ceci permet d'ajouter une liste dans l'inspecteur. Vu qu'on travaille sur des éléments du préfab c'est beacoup plus simple que de chercher dans la liste des parents en boucle
{
    public Button button;
}
public class FinalScoreDisplay : MonoBehaviour
{
    [Header("Configuration")]
    [HideInInspector] public float _valeurCubesUsed = 0f;
    [HideInInspector] public float _valeurCubesLost = 0f;
    [HideInInspector] public float _valeurDirectImpact = 0f;
    [HideInInspector] public float _valeurIndirectImpact = 0f;
    [HideInInspector] public float _valeurEnergyCollected = 0f;
    [HideInInspector] public float _valeurBuffersCollected = 0f;
    [HideInInspector] public float _valeurTotalScore = 0f;
    [HideInInspector] public float ScoreScroll = 0.3f;
    [HideInInspector] public bool strikeAchieved;
    [HideInInspector] public bool strikeToBeAchieved;

    [SerializeField]
    private float _minimalScore = 10f;
    [SerializeField]
    private float _goodScore = 15f;
    [SerializeField]
    private InputActionReference clickAction;
    [SerializeField]
    private GameManagerScript _gameManagerScript;
    [SerializeField]
    private TMP_Text _cubesUsed;
    [SerializeField]
    private TMP_Text _cubesLost;
    [SerializeField]
    private TMP_Text _directImpact;
    [SerializeField]
    private TMP_Text _indirectImpact;
    [SerializeField]
    private TMP_Text _energyCollected;
    [SerializeField]
    private TMP_Text _buffersCollected;
    [SerializeField]
    private TMP_Text _totalScore;
    [SerializeField]
    private List<Buttonslist> Buttons;

    void OnDisable()
    {
        clickAction.action.performed -= OnClick;
        clickAction.action.Disable();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        ScoreScroll = 0f;
    }
    public IEnumerator FinalDisplay()
    {
        clickAction.action.Enable();
        clickAction.action.performed += OnClick;
        yield return new WaitForSeconds(ScoreScroll);
        _cubesUsed.text = $"{_valeurCubesUsed}";
        yield return new WaitForSeconds(ScoreScroll);
        _cubesLost.text = $"{_valeurCubesLost}";
        yield return new WaitForSeconds(ScoreScroll);
        _directImpact.text = $"{_valeurDirectImpact}";
        yield return new WaitForSeconds(ScoreScroll);
        _indirectImpact.text = $"{_valeurIndirectImpact}";
        yield return new WaitForSeconds(ScoreScroll);
        _energyCollected.text = $"{_valeurEnergyCollected}";
        yield return new WaitForSeconds(ScoreScroll);
        _buffersCollected.text = $"{_valeurBuffersCollected}";
        yield return new WaitForSeconds(ScoreScroll);
        if ((_gameManagerScript.Catapult != null || _gameManagerScript.CinematicTime) && !strikeAchieved)
        {
            _valeurTotalScore = (_valeurEnergyCollected + 0.25f * _valeurDirectImpact + 0.5f * _valeurIndirectImpact) / (_valeurCubesUsed + 0.5f * _valeurCubesLost);
            if (_valeurTotalScore < _minimalScore)
                _totalScore.text = $"<color=#9F340C>{_valeurTotalScore} </color>";
            else if (_valeurTotalScore > _goodScore)
                _totalScore.text = $"<color=#D3F300>{_valeurTotalScore} </color>";
            else
                _totalScore.text = $"<color=#FFFFFF>{_valeurTotalScore}";
        }
        else if ((_gameManagerScript.Catapult == null && !_gameManagerScript.CinematicTime) && !strikeAchieved)
        {
            _totalScore.text = $"<color=#FB0B0B>Catapilt Was Destroyed</color>";
        }
        else if (strikeAchieved)
        {
            _totalScore.text = $"<color=#1FFFFF>Strike</color>";
        }
        foreach (Buttonslist item in Buttons)
        {
            item.button.gameObject.SetActive(true);
            if (item.button.name == "NextMap" && !strikeAchieved)
            {
                if (_valeurTotalScore < _minimalScore || (_gameManagerScript.Catapult == null && !_gameManagerScript.CinematicTime))
                    item.button.interactable = false;
            }
        }
    }
}