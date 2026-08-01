using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CubesRemainingTextDisplay : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string prefix = "Cubes Remaining: ";
    [HideInInspector] public float valeurtotale = 2f;

    private TMP_Text textComponent;
    private float lastValeurVisualisee;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        lastValeurVisualisee = valeurtotale;
    }

    private void Start()
    {
    }
    public void RefreshDisplay()
    {
        if (valeurtotale != lastValeurVisualisee)
        {
            if (textComponent != null)
            {
                textComponent.text = $"{prefix}{valeurtotale}";
            }
            lastValeurVisualisee = valeurtotale;
        }
    }
}