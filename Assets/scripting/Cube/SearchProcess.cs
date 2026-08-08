using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gère le radar du joueur : détecte les spirales et active leur aspiration.
/// </summary>
public class SearchProcess : MonoBehaviour
{
    private GameObject spirale;
  /*  #region Variables
    [Header("Radar")]
    [Tooltip("Liste de tous les objets collectables enregistrés au Start.")]
    public List<GameObject> Ofinterestlist = new List<GameObject>(); //sert fichtrement à rien ce truc à part me faire planter pendant 3 jours ur pourquoi le deuxième cube n'a rien
    #endregion
  */

    /// <summary>
    /// Se déclenche quand un objet entre dans le cercle (Trigger) du radar.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // 1. On vérifie si l'objet qui entre possède le script SpiralePoints
        if (other.TryGetComponent<SpiralePoints>(out SpiralePoints spirale))
        {
            Debug.Log("il en a un");
                spirale.ActiverAspiration(this.transform.parent.gameObject);
        }
        else
        {
            // 4. Si ce n'est pas une spirale, on demande à la physique d'ignorer la collision mais en fait juste non c'est même pas pour ça que je le laisse parce que c'est déjà un trigger, c'est pour empêcher un éventuel pic de mémoire qui est virtuellement inexistant
            // pour ne pas que le radar "pousse" les objets ou les quilles.
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
    }
}