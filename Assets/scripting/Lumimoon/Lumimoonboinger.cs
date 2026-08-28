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
        if (bouncingtime) //on veut que le bouncer reprenne sa forme initiale
        {
            if (Vector3.Distance(boneparent.transform.position, InitialPosition) < 0.01f) //s'il est à peu près à sa place
            {
                boneparent.transform.position = InitialPosition;
                bouncingtime = false;
                boneparent.linearVelocity = Vector3.zero;
            }
            else
            boneparent.linearVelocity = (InitialPosition - boneparent.transform.position).normalized * (InitialPosition - boneparent.transform.position).magnitude; //va bouger comme un ressort jusqu'à reprendre sa place
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!BouncyRoutineHasStarted)
        {
            if (collision.GetComponent<Rigidbody>() != null)
            {
                bouncingrigid = collision.GetComponent<Rigidbody>();
                StartCoroutine(BouncyRoutine());
                BouncyRoutineHasStarted = true;
            }
        }
    }
    private IEnumerator BouncyRoutine() //routine qui indique ce qu'il se passe lorsqu'un objet percute le bouncer
    {
        Vector3 bouncingrigidVelocity = bouncingrigid.linearVelocity;
        Vector3 surfaceNormal = (bouncingrigid.position - boneparent.position).normalized; //indique où l'objet a pénétré le trigger
        Vector3 bouncingVelocity = Vector3.Reflect(bouncingrigidVelocity, surfaceNormal); //on veut appliquer une force égale à la force de déformation du bouncer par l'objet
        boneparent.linearVelocity = bouncingrigid.linearVelocity; //on fait visuellement s'effondrer le parent en conséquence, pour donner l'effet de déformation
        yield return new WaitForSeconds(0.2f);
        bouncingrigid.linearVelocity += bouncingVelocity * 2f;
        bouncingtime = true;
        BouncyRoutineHasStarted = false;
    }
}
