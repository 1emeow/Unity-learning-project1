using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TotalScoreDisplay : MonoBehaviour
{
    #region Variables
    [Header("Configuration")]
    private string prefix = "Total Energy: ";
    
    // Indispensable pour ton script 'spiraleapoints.cs'
    [HideInInspector] public float valeurtotale; 
    
    [SerializeField] private TMP_Text textComponent;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        if (ScoreStatusHold.Instance != null)
        {
            valeurtotale = ScoreStatusHold.Instance.CumulativeScore;
            RefreshDisplay(valeurtotale);
        }

    }
    #endregion
    #region Méthode de Mise à jour
    /// <summary>
    /// Reçoit le score depuis le ScoreStatusHold et met à jour l'affichage.
    /// </summary>


    public void RefreshDisplay(float nouvelleValeur)
    {
        valeurtotale = nouvelleValeur; // On met à jour la variable de crédits
        Debug.Log(valeurtotale);

        if (textComponent != null)
            textComponent.text = $"{prefix}{valeurtotale}";
        else
            Debug.Log(textComponent == null);
        if (ScoreStatusHold.Instance != null)
        {
            ScoreStatusHold.Instance.CumulativeScore = valeurtotale;
        }
    }
    #endregion
}