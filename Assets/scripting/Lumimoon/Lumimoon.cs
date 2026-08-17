using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable] //permet de la modifier dans l'inspecteur
public class Componentslist //ceci permet d'ajouter une liste dans l'inspecteur. Vu qu'on travaille sur des éléments du préfab c'est beacoup plus simple que de chercher dans la liste des parents en boucle
{
    public Component component;
}
public class Lumimoon: MonoBehaviour
{
    public enum LumimoonState
    {
        evil,
        neutral,
        cool
    }
    [SerializeField]
    private List<Componentslist> Components;
    [SerializeField]
    private Color neutralcolour;
    [SerializeField]
    private Color neutralcolourE;
    [SerializeField]
    private Color neutralcolourL;
    [SerializeField]
    private Color evilcolour;
    [SerializeField]
    private Color evilcolourE;
    [SerializeField]
    private Color evilcolourL;
    [SerializeField]
    private Color coolcolour;
    [SerializeField]
    private Color coolcolourE;
    [SerializeField]
    private Color coolcolourL;
    public LumimoonState _lumimoonState;
    private LumimoonState _currentlumimoonState;
    private Rigidbody _blobbedRigid;
    private SkinnedMeshRenderer _meshRenderer;
    [SerializeField]
    private GameObject _blobObject;
    [SerializeField]
    private Light _light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       _meshRenderer = _blobObject.GetComponent<SkinnedMeshRenderer>();
        _currentlumimoonState = _lumimoonState;
        Physics.IgnoreLayerCollision(7, 7, true);
        ChangeState();

    }
    void ChangeState()
    {
        switch (_lumimoonState) //la fonction switch permet de check dans quel state on est au lieu de faire if / else
        {
            case LumimoonState.neutral:
            {
            _meshRenderer.material.color = neutralcolour;
                _meshRenderer.material.SetColor("_EmissionColor", neutralcolourE);
                _light.color = neutralcolourL;

                break; //on a trouvé l'état neutral
             }
        case LumimoonState.cool:
        {
            _meshRenderer.material.color = coolcolour;
            _meshRenderer.material.SetColor("_EmissionColor", coolcolourE);
            _light.color = coolcolourL;
                    break; //on a trouvé l'état cool
        }
        case LumimoonState.evil:
        {
            _meshRenderer.material.color = evilcolour;
            _meshRenderer.material.SetColor("_EmissionColor", evilcolourE);
            _light.color = evilcolourL;
                    break; //on a trouvé l'état evil
        }
        }
        bool colliderEnabled = _lumimoonState != LumimoonState.cool; //tant qu'on n'est pas sur LumimoonState.cool, le colliderEnabled est true, sinon il devient false
        foreach (Componentslist item in Components) //pour chacun des items de type Componentslist de la liste components
        {
            Collider _itemcollider = item.component.GetComponent<Collider>();
            if (_itemcollider != null)
            {
                item.component.GetComponent<Collider>().enabled = colliderEnabled; //le composant de l'item, ou item's component, soit item.component
            }
        }
        _currentlumimoonState = _lumimoonState;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        _blobbedRigid = other.attachedRigidbody;
        if (other.attachedRigidbody != null)
        {
            switch (_lumimoonState) //la fonction switch permet de check dans quel state on est au lieu de faire if / else
            {
                case LumimoonState.cool:
                    {
                        _blobbedRigid.linearVelocity += _blobbedRigid.linearVelocity * 0.5f;
                        break;
                            }
                case LumimoonState.evil:
                    {
                        _blobbedRigid.linearVelocity -= _blobbedRigid.linearVelocity * 0.2f;
                        break; }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeScript>() != null)
        {
            switch (_lumimoonState)
            {
                case LumimoonState.cool:
                    {
                        _lumimoonState = LumimoonState.neutral;
                        ChangeState();
                        break;
                    }
                case LumimoonState.neutral:
                    {
                        _lumimoonState = LumimoonState.evil;
                        ChangeState();
                        break;
                    }
                case LumimoonState.evil:
                    {
                        _lumimoonState = LumimoonState.cool;
                        ChangeState();
                        break;
                    }
            }
        }
    }
}
