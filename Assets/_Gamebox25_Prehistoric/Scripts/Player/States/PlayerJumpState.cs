using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(Player stateMachine) : base(stateMachine)
    {}
    
    public override void Enter()
    {
        Jump();
    }

    public override void Tick()
    {
        Move();
        if (stateMachine.GroundChecker.IsGrounded)
        {
            if (stateMachine.InputReader.IsJumpHeld)
            {
                Jump();
            }
            else
            {
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            }
        }
    }

    public override void Exit()
    {}

    private void Jump()
    {
        if (!stateMachine.GroundChecker.IsGrounded)
        {
            return;
        }

        stateMachine.Rigidbody.velocity = new Vector3(stateMachine.Rigidbody.velocity.x, stateMachine.Config.JumpForce);
    }
}