using UnityEngine;

public class PlayerStopState : PlayerBaseState
{
    public PlayerStopState(Player stateMachine) : base(stateMachine)
    {}

    public override void Enter()
    {
        stateMachine.Rigidbody.velocity = Vector3.zero;
    }

    public override void Tick()
    {}

    public override void Exit()
    {}
}