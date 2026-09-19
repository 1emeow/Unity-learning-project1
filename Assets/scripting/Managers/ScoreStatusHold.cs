using UnityEngine;

public class ScoreStatusHold : MonoBehaviour
{
    public static ScoreStatusHold Instance; //indique la présence d'une instance
    public float CumulativeScore;
    public bool WasAMoveSetterAcquiredThisGame;
    public bool WasAJumperBuffAcquiredThisGame;
    [SerializeField]
    private GameManagerScript _gameManager;
    void Awake()
    {
            // Setup du Singleton. On fait en sorte que seule cette instance reste à chaque fois
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        if (_gameManager != null)
        {
            _gameManager.WasMoveSetterReached = WasAMoveSetterAcquiredThisGame;
            _gameManager.WasJumpBufferReached = WasAJumperBuffAcquiredThisGame;
        }
    }
}
