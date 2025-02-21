using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected Player stateMachine;
    
    public PlayerBaseState(Player stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected void Move()
    {
        float yCurrentVelocity = stateMachine.Rigidbody.velocity.y;
        float xCurrentVelocity = stateMachine.Rigidbody.velocity.x;
        
        if (stateMachine.InputReader.MovementDirection == 0) {
            stateMachine.Rigidbody.velocity = new Vector2(
                Mathf.Lerp(xCurrentVelocity, 0, Time.deltaTime * stateMachine.Config.TimeToStop),
                yCurrentVelocity
            );
            return;
        }
        
        float xVelocity = stateMachine.InputReader.MovementDirection * stateMachine.Config.Speed;
        stateMachine.Rigidbody.velocity = new Vector3(xVelocity, yCurrentVelocity);
    }
}