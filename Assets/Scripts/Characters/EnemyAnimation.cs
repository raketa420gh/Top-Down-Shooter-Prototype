using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region Variables

    private Animator animator;
    private EnemyMovement movement;

    #endregion
    

    #region Unity lifecycle
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    #endregion


    #region Public Methods
    
    public void SetWalkAnimationTrigger()
    {
        animator.SetFloat(AnimationTriggerNames.MoveSpeed, movement.SelfRigidbody2D.velocity.magnitude);
    }
    
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Die);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Attack);
    }

    #endregion
}
