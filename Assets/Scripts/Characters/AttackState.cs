using UnityEngine;

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

        npc.SubtractAttackTime();

        if (npc.AttackTimer <= 0)
        {
            npc.Animation.SetTriggerAttack();
            npc.ResetAttackTimer();
        }
    }

    #region Unity lifecycle

    private void OnEnable()
    {
        ZombieAnimationEventHandler.OnAttacked += ZombieAnimationEventHandlerOnAttacked;
    }

    private void OnDisable()
    {
        ZombieAnimationEventHandler.OnAttacked -= ZombieAnimationEventHandlerOnAttacked;
    }

    #endregion
    
    
    #region Event Handlers
    
    private void ZombieAnimationEventHandlerOnAttacked()
    {
        npc.Player.TakeDamage(npc.AttackDamage);
    }
    
    #endregion
}