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
        move3 = _catapultController.move3;
        

        //Moving Forward

        _CatapiltAnimator.SetBool("DoneWalking", move3.sqrMagnitude < 0.0001f); //DoneWalking is only true when move3.sqrMagnitude < 0,0001f 

        //Strafing

        _CatapiltAnimator.SetBool("Strafing", Mathf.Abs(move3.x) > 0.01f); //Strafing is true as long as Mathf.Abs(move3.x) > 0.01f, Mathf being Math Function

        /*  if (move3.z != 0)
          {
           //   _CatapiltAnimator.SetFloat("Speed", Mathf.Abs(move3.z * _catapultController._speed));
              _CatapiltAnimator.SetBool("DoneWalking", false);
           //   Debug.Log(move3.magnitude < 0.01f);
          }
        //Strafing
          if (move3.x != 0)
          {
           //   _CatapiltAnimator.SetFloat("Speed", Mathf.Abs(move3.x * _catapultController._speed));
              _CatapiltAnimator.SetBool("Strafing", true);
              _CatapiltAnimator.SetBool("DoneWalking", false);
          }
          else
          {
           //   _CatapiltAnimator.SetFloat("Speed", 0);
              _CatapiltAnimator.SetBool("Strafing", false);
          }
          if (move3.magnitude < 0.01f)
          {
              //    _CatapiltAnimator.SetFloat("Speed", 0);
              _CatapiltAnimator.SetBool("DoneWalking", true);
          }
        */
    }
}
