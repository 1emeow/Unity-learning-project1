using UnityEngine;
using System.Collections;

public class Lumimoonboinger : MonoBehaviour
{
    private Rigidbody boneparent;
    private Rigidbody bouncingrigid;
    private bool BouncyRoutineHasStarted;
    private float originalDamping;
    private bool bouncingtime;
    private Vector3 InitialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boneparent = GetComponentInParent<Rigidbody>();

        if (boneparent == null)
        {
            return;
        }
   /*     Debug.Log(
    $"[{GetInstanceID()}] {gameObject.name}: FOUND {boneparent.name}",
    gameObject
);*/
        InitialPosition = boneparent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bouncingtime)
        {
            if (Vector3.Distance(boneparent.transform.position, InitialPosition) < 0.01f)
            {
                boneparent.transform.position = InitialPosition;
                bouncingtime = false;
                boneparent.linearVelocity = Vector3.zero;
            }
            else
            boneparent.linearVelocity = (InitialPosition - boneparent.transform.position).normalized * (InitialPosition - boneparent.transform.position).magnitude;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!BouncyRoutineHasStarted)
        {
            if (collision.GetComponent<Rigidbody>() != null)
            {
                bouncingrigid = collision.GetComponent<Rigidbody>();
                BouncyFunction();
                BouncyRoutineHasStarted = true;
            }
        }
    }
    private void BouncyFunction()
    {
        Vector3 bouncingrigidVelocity = bouncingrigid.linearVelocity;
        Vector3 surfaceNormal = (bouncingrigid.position - boneparent.position).normalized;
        Vector3 bouncingTrajectory = bouncingrigid.linearVelocity.normalized; //normalized donne un vecteur de longueur 1, ce qui permet de faire une direction et avoir une vitesse
        Vector3 bouncingVelocity = Vector3.Reflect(bouncingrigidVelocity, surfaceNormal);
        boneparent.linearVelocity = bouncingrigid.linearVelocity;
        bouncingrigid.linearVelocity = bouncingVelocity;
        bouncingtime = true;
        BouncyRoutineHasStarted = false;
    }
}
