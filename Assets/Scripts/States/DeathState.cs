public class DeathState : State
{
    public DeathState(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
        npc.Animation.ActivateTriggerDie();
        npc.Animation.SetBoolIsMoving(false);
        npc.SelfSpriteRenderer.sortingOrder = -1;
        npc.Movement.SetDestinationTarget(null);
        npc.Movement.ActivateAIPath(false);
        npc.SelfCollider.enabled = false;
        npc.HealthBar.gameObject.SetActive(false);
    }
}
