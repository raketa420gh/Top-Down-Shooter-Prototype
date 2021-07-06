using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region Variables

    private Animator animator;

    #endregion
    

    #region Unity lifecycle
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    #endregion


    #region Public Methods
    
    public void SetBoolIsMoving(bool isActive)
    {
        animator.SetBool(AnimationTriggerNames.IsMoving, isActive);
    }

    public void ActivateTriggerIdle(bool isActive)
    {
        if (isActive)
        {
            animator.SetTrigger(AnimationTriggerNames.Idle);
        }
        else
        {
            animator.ResetTrigger(AnimationTriggerNames.Idle);
        }
    }
    
    public void ActivateTriggerDie(bool isActive)
    {
        if (isActive)
        {
            animator.SetTrigger(AnimationTriggerNames.Die);
        }
        else
        {
            animator.ResetTrigger(AnimationTriggerNames.Die);
        }
    }

    public void SetTriggerAttack()
    {
        animator.SetTrigger(AnimationTriggerNames.Attack);
    }

    #endregion
}
