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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        npc.Movement.SetDestinationTarget(npc.Player.transform);

        //attackTimer -= Time.deltaTime;

        //if (attackTimer <= 0)
        //{
        //    animation.SetTriggerAttack();
        //    ResetAttackTimer();
        //}
    }
}