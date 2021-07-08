public class ZombieIdleState : State
{
    public ZombieIdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animation.SetBoolIsMoving(false);
        animation.ActivateTriggerIdle(true);
        movement.SetTargetToChase(null);
        movement.ActivateAIPath(false);
    }
}