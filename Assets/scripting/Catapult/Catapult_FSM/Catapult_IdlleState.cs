using UnityEngine;

public class Catapult_IdleState : State
{
    private Catapult_StateController _catapult;

    public Catapult_IdleState(Catapult_StateController sm) : base(sm) 
    {
        _catapult = (Catapult_StateController)sm;
    }

    public override void Enter() {
    
    }

    public override void Exit() {
    
    }

    public override void Update() {
    
    }
}