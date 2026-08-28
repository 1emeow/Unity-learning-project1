using UnityEngine;
using System.Collections;


public class MagneflowBehavior : MonoBehaviour
{
    public enum Status
    {
      attracting,
      attract_flying,
      dying,
      fully_packed,
      repelling,
      repel_flying,
      alternative_flying
    }
    [SerializeField]
    private Animator _magneflowAnimator;
    [SerializeField]
    private Status _currentStatus;
    [SerializeField]
    private GameObject _attractionSphere;
    [SerializeField]
    private GameObject _repulsionSphere;
    private Status _previousStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StatusChange();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currentStatus != _previousStatus)
        {
            StatusChange();
        }
    }
    private void ResetAnimatorBoolsAndFields() //on reset tous les paramètres de l'animateur pour pouvoir n'utiliser que celui qui nous intéresse
    {
        foreach (AnimatorControllerParameter parameter in _magneflowAnimator.parameters) 
        {
            if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name != "Is_Dying") //on cible les animateurs de type bool spécifiquement
            {
                _magneflowAnimator.SetBool(parameter.name, false);
            }
        }
        _attractionSphere.SetActive(false);
        _repulsionSphere.SetActive(false);

    }
    void StatusChange()
    {
        ResetAnimatorBoolsAndFields(); //on le fait en tout premier pour ne pas qu'il y ait de cumul de commandes
        switch (_currentStatus)
        { case Status.attracting: //case indique déjà le bloc, le case d'en dessous le limite. Il n'y a donc pas besoin de {}       
                    _magneflowAnimator.SetBool("Deploying_Attraction", true);
                    _attractionSphere.SetActive(true);
                    break;
          case Status.attract_flying: 
                    _magneflowAnimator.SetBool("Attract_While_Flying", true);
                    _attractionSphere.SetActive(true);
                    break;   
          case Status.dying:  
                    _magneflowAnimator.SetBool("Is_Dying", true);
                    break;
          case Status.fully_packed:
                 if (_previousStatus== Status.attracting)
                    {
                        _magneflowAnimator.SetBool("Packing_From_Attract", true);
                    }
                 else if (_previousStatus== Status.repelling)
                    {
                        _magneflowAnimator.SetBool("Packing_From_Repel", true);
                    }
                    break;
          case Status.repelling: 
                    _magneflowAnimator.SetBool("Deploying_Repel", true);
                    _repulsionSphere.SetActive(true);
                    break;
          case Status.repel_flying: 
                    _magneflowAnimator.SetBool("Repel_While_Flying", true);
                    _repulsionSphere.SetActive(true);
                    break;
          case Status.alternative_flying: 
                    _magneflowAnimator.SetBool("Full_Flying", true);
                    _attractionSphere.SetActive(true);
                    _repulsionSphere.SetActive(true);
                    break;
        }
        _previousStatus = _currentStatus;
    }
}
