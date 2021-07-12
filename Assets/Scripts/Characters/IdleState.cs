public class IdleState : State
{
    public IdleState(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        npc.Animation.SetBoolIsMoving(false);
        npc.Animation.ActivateTriggerIdle();
        npc.Movement.SetDestinationTarget(null);
        npc.Movement.ActivateAIPath(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (npc.DistanceToPlayer < npc.AttackRadius)
        {
            stateMachine.ChangeState(new AttackState(npc, stateMachine));
        }
        else if (npc.DistanceToPlayer < npc.VisionRadius)
        {
            stateMachine.ChangeState(new ChaseState(npc, stateMachine));
        }
    }
}