using UnityEngine;

public class HorizontalCrosshairFrame : MonoBehaviour
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
        InitialPosition = _spriteMask.transform.localPosition; //point de départ de la langue
        MaximalPullDistance = Mathf.Abs(InitialPosition.x - _spriteFrame.transform.localPosition.x);
    }
    public void ChargeEffect()
    {
        _chargeEffect += _chargeSpeed * Time.deltaTime;
        _chargeEffect = Mathf.Clamp(_chargeEffect, 0, MaximalPullDistance);
        _spriteMask.transform.localPosition = InitialPosition + Vector3.left * _chargeEffect;
    }
    public void ReinitializeEffect()
    {
        _chargeEffect = 0f;
        _spriteMask.transform.localPosition = InitialPosition;
    }
}
