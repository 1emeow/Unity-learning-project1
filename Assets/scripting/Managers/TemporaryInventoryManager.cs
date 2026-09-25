using UnityEngine;
using System.Collections.Generic;

public class TemporaryInventory : MonoBehaviour, MapManager
{
    public static TemporaryInventory Instance; //indique la présence d'une instance
    public List<PickedUpData> InventoryList;
    public bool mapChangeOrTryAgain { get; set; }
    void Awake()
    {
        // Setup du Singleton. On fait en sorte que seule cette instance reste à chaque fois
        if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
    }
    void Start()
    {
        if (ScoreStatusHold.Instance != null)
            InventoryList = new List<PickedUpData>(ScoreStatusHold.Instance.InventoryList);
        else
            InventoryList = new List<PickedUpData>(); //on s'assure que la liste n'est jamais nulle.
    }
    void OnDestroy()
    {
        if (ScoreStatusHold.Instance != null && mapChangeOrTryAgain)
            ScoreStatusHold.Instance.InventoryList = new List<PickedUpData>(InventoryList); //On va copier la liste dans le fichier permanent, ça évitera d'oblitérer l'inventaire pendant la destruction de cette instance.
    }
}
