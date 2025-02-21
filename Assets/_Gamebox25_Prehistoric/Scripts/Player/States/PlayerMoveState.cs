public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player stateMachine) : base(stateMachine)
    {}
    
    public override void Enter()
    {
        stateMachine.InputReader.OnJump += OnJump;
    }

    public override void Tick()
    {
        Move();
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnJump -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}