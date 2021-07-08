using UnityEngine;

public class ZombieAttackState : State
{
    public ZombieAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        movement.ActivateAIPath(true);
        animation.SetBoolIsMoving(false);
        animation.ActivateTriggerIdle(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        movement.SetTargetToChase(player.transform);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            animation.SetTriggerAttack();
            ResetAttackTimer();
        }
    }
}