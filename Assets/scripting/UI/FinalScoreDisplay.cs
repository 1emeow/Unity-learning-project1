using UnityEngine;
using TMPro;
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

    /*   private void Awake()
       {
           textComponent = GetComponent<TMP_Text>();
       }
    */
    public void FinalDisplay()
    {
        _cubesUsed.text = $"{_valeurCubesUsed}";
        _cubesLost.text = $"{_valeurCubesLost}";
        _directImpact.text = $"{_valeurDirectImpact}";
        _indirectImpact.text = $"{_valeurIndirectImpact}";
        _energyCollected.text = $"{_valeurEnergyCollected}";
        _buffersCollected.text = $"{_valeurBuffersCollected}";
        _valeurTotalScore = (_valeurEnergyCollected - _valeurBuffersCollected)/(_valeurCubesUsed + 0.5f * _valeurCubesLost);
        _totalScore.text = $"{_valeurTotalScore}";
    }
}