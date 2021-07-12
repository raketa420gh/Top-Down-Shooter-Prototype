using UnityEngine;

public class NPCAnimation : MonoBehaviour
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

    public void ActivateTriggerIdle()
    {
        animator.SetTrigger(AnimationTriggerNames.Idle);
    }
    
    public void ActivateTriggerDie()
    {
        animator.SetTrigger(AnimationTriggerNames.Die);
    }

    public void SetTriggerAttack()
    {
        animator.SetTrigger(AnimationTriggerNames.Attack);
    }

    #endregion
}
