public class AttackState : State
{
    public AttackState(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        npc.Movement.ActivateAIPath(true);
        npc.Animation.SetBoolIsMoving(false);
        npc.Movement.SetDestinationTarget(npc.Player.transform);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        npc.SubtractAttackTime();

        if (npc.AttackTimer <= 0)
        {
            npc.Animation.SetTriggerAttack();
            npc.ResetAttackTimer();
        }
    }
}