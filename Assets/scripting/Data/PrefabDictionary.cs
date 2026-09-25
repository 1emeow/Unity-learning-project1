using UnityEngine;
using System.Collections.Generic;
public class PrefabDictionary : MonoBehaviour
{
    public static PrefabDictionary Instance;
    [System.Serializable]
    public class PrefabEntry //on fait la classe de notre liste, PrefabEntry, dans laquelle on indique quel préfab instantier en fonction de son identifiant dans l'inventaire (le string enregistré)
    {
        public GameObject prefab;
    }
    [SerializeField]
    private List<PrefabEntry> entries = new List<PrefabEntry>(); //on n'a pas besoin de l'interférence d'autres scripts
    public string prefabId;
    private Dictionary<string, GameObject> prefabDictionary; //dans le dictionnaire, l'information est divisée en clé et valeur, la clé est l'identifiant (prefabID) et la valeur correspond au préfab dans la liste
    private void Awake()
    {
        Instance = this;
        prefabDictionary = new Dictionary<string, GameObject>(); //on fait notre dictionnaire de préfabs

        foreach (PrefabEntry entry in entries)
        {
            prefabId = entry.prefab.name;
            if (entry.prefab == null) //si jamais la valeur ne correspond à aucun préfab
            {
                continue;
            }
            if (prefabDictionary.ContainsKey(prefabId)) //si tout va bien
            {
                continue;
            }
            prefabDictionary.Add(prefabId, entry.prefab); //on ajoute au dictionnaire
        }
    }
    public GameObject GetPrefab(string prefabId)
    {
        if (prefabDictionary.TryGetValue(prefabId, out GameObject prefab))
        {
            return prefab;
        }
        Debug.LogError($"No prefab found");
        return null;
    }
}

