using UnityEngine;

public class Catapult_StateController : StateMachine
{
    [SerializeField]
    private Animator _CatapiltAnimator;
    [SerializeField]
    private CatapultController _catapultController;
    private Vector3 move3;
    void Start()
    {
    }
    void Update()
    {

        //Movement
        move3 = _catapultController.move3; //on récupére le move3 dans catapult controller
        

        //Moving Forward

        _CatapiltAnimator.SetBool("DoneWalking", move3.sqrMagnitude < 0.0001f); //DoneWalking is only true when move3.sqrMagnitude < 0,0001f 

        //Strafing

        _CatapiltAnimator.SetBool("Strafing", Mathf.Abs(move3.x) > 0.01f); //Strafing is true as long as Mathf.Abs(move3.x) > 0.01f, Mathf being Math Function
    }
}
