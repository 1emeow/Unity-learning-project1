using UnityEngine;

public class CrosshairFrame : MonoBehaviour
{
    public GameObject _spriteFrame;
    public GameObject _spriteMask;
    private float _chargeEffect;
    [HideInInspector]
    public float _chargeSpeed = 0f;
    private Vector3 InitialPosition;
    [HideInInspector]
    public float MaximalPullDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialPosition = _spriteFrame.transform.localPosition; //point de départ de la langue
        MaximalPullDistance = Mathf.Abs(InitialPosition.y - _spriteMask.transform.localPosition.y);
    }
    public void ChargeEffect()
    {
        _chargeEffect += _chargeSpeed * Time.deltaTime;
        _chargeEffect = Mathf.Clamp(_chargeEffect, 0, MaximalPullDistance);
        _spriteFrame.transform.localPosition = InitialPosition + Vector3.up * _chargeEffect;
    }
    public void ReinitializeEffect()
    {
        _chargeEffect = 0f;
        _spriteFrame.transform.localPosition = InitialPosition;
        _spriteMask.transform.localPosition = InitialPosition;
    }
}
