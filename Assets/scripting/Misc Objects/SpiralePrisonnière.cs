using UnityEngine;
using System.Collections;

public class SpiralePrisonnière : MonoBehaviour
{

    public float _velocity = 5f;
    [SerializeField]
    private Collider BeamCollider;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity += Vector3.up * _velocity;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), BeamCollider);
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ChangeDirection());
    }
    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(1f);
        _velocity = -_velocity;
        rb.linearVelocity += Vector3.up * _velocity;
    }
}