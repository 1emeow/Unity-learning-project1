//Cette classe permet d'enregistrer les paramètres de l'objet capturable. Une classe est pratique à mettre en place car elle peut retenir différents paramètres de différents types d'objets, par exemple, Lumimoon et Mangeflow.

using UnityEngine;
[System.Serializable] //pour utiliser la partie system et pouvoir y accéder à tout moment via l'inspecteur
public class PickedUpData
{
    //on met les paramètres communs aux fichiers ici
    public string prefabId;
    public Vector3 scale;

    //on convertit les paramètres spécifiques à chaque objet en string qui sera ensuite retranscrit en infomation ici
    public string specificData;
}
