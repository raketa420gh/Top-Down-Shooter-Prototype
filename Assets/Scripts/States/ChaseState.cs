using UnityEngine;

public class ChaseState : State
{
    public ChaseState(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        npc.Animation.SetBoolIsMoving(true);
        npc.Movement.ActivateAIPath(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (npc.DistanceToPlayer < npc.AttackRadius)
        {
            stateMachine.ChangeState(new AttackState(npc, stateMachine));
        }
        
        if (npc.DistanceToPlayer > npc.VisionRadius)
        {
            stateMachine.ChangeState(new IdleState(npc, stateMachine));
        }
        
        Debug.DrawRay(npc.SelfPosition, npc.PlayerDirection, Color.blue);

        var ray = Physics2D.Raycast(npc.SelfPosition, npc.PlayerDirection, 
            npc.DistanceToPlayer, npc.ObstacleMask);

        if (ray.collider != null) return;
        
        npc.Movement.SetDestinationTarget(npc.Player.transform);
    }
}