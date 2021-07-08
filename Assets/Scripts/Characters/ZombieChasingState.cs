using UnityEngine;

public class ZombieChasingState : State
{
    public ZombieChasingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animation.ActivateTriggerIdle(false);
        animation.SetBoolIsMoving(true);
        movement.ActivateAIPath(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Debug.DrawRay(selfPosition, playerDirection, Color.blue);

        var ray = Physics2D.Raycast(selfPosition, playerDirection, distanceToPlayer, obstacleMask);

        if (ray.collider != null) return;
        
        movement.SetTargetToChase(player.transform);
    }
}