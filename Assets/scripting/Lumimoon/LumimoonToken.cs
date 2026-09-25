using UnityEngine;
using System;
using System.Collections.Generic;
public class LumimoonToken : MonoBehaviour, CanBePicked, HasStorableData
{
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
    private Vector3 scaleToSet;
    public PickedUpData StoreData()
    {
        LumimoonData lumimoonData = new LumimoonData
        {
        };
        return new PickedUpData
        {
        };
    }
    public bool pickupable { get; set; }
    public LumimoonState _lumimoonState;
    private LumimoonState _currentlumimoonState;
    private Rigidbody _blobbedRigid;
    private SkinnedMeshRenderer _meshRenderer;
    [SerializeField]
    private GameObject _blobObject;
    [SerializeField]
    private Light _light;

    void Awake()
    {
       _meshRenderer = _blobObject.GetComponent<SkinnedMeshRenderer>();
        _currentlumimoonState = _lumimoonState;
        ChangeState();
        _blobbedRigid = this.GetComponentInParent<Rigidbody>();
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
        _currentlumimoonState = _lumimoonState;
    }
    public void IsPickedUp()
    {

    }
    public void LoadData(PickedUpData data) //Cette fonction charge les paramètres récupérés avec StoreData
    {
        LumimoonData lumimoonData = JsonUtility.FromJson<LumimoonData>(data.specificData);
        if (Enum.TryParse(lumimoonData.state, out LumimoonState state))
            {
            _lumimoonState = state;
            }
            else
            {
            _lumimoonState = LumimoonState.neutral;
            }

            transform.root.localScale = data.scale;
            scaleToSet = data.scale;

        ChangeState();
    }
    public void IsReleased()
    {

    }
}
