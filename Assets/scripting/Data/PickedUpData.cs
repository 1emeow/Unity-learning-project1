//Cette classe permet d'enregistrer les paramètres de l'objet capturable. Une classe est pratique à mettre en place car elle peut retenir différents paramètres de différents types d'objets, par exemple, Lumimoon et Mangeflow.

using UnityEngine;
[System.Serializable] //pour utiliser la partie system et pouvoir y accéder à tout moment via l'inspecteur
public class PickedUpData
{
    public string prefabId;
    public Vector3 scale;
    public Lumimoon.LumimoonState _lumimoonState;
}
