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
        npc.Movement.SetDestinationTarget(npc.Player.transform);
    }
}