using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gère la logique du score de manière isolée.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Données")]
    [SerializeField] private int currentScore = 0;

    [Header("Événements")]
    [Tooltip("Envoie le nouveau score à chaque modification.")]
    public UnityEvent<int> OnScoreChanged;

    [Header("Affichage Score Final")]
    [SerializeField]
    private FinalScoreDisplay _finalScoreDisplay;
    [SerializeField]
    private ScoreStatusHold _scoreAndStatusHolder;
    public int bestScore = 27;

    private void Awake()
    {
        // Setup du Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        if (_finalScoreDisplay != null)
        {
            if (_finalScoreDisplay._valeurEnergyCollected != currentScore)
                _finalScoreDisplay._valeurEnergyCollected = currentScore;
            if (currentScore >= bestScore)
                _finalScoreDisplay.strikeToBeAchieved = true;
        }
    }

    /// <summary>
    /// Ajoute des points et prévient les abonnés.
    /// </summary>
    public void AddScore(int points)
    {
        currentScore += points;
        // On lance l'événement avec la nouvelle valeur
        OnScoreChanged?.Invoke(currentScore);
        
      /*  Debug.Log($"<color=green>SCORE :</color> {currentScore} (+{points})");
        Debug.Log($"Called in {GetType().Name}");
      */
    }

    public int GetCurrentScore() => currentScore; //est une autre méthode pour faire return currentScore
    void OnDestroy()
    {
        if (ScoreStatusHold.Instance != null)
        {
            ScoreStatusHold.Instance.CumulativeScore += currentScore;
        }
    }
}