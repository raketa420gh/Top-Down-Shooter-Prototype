using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variables

    private Animator animator;
    private PlayerMovement movement;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    #endregion
    

    #region Public methods
    
    public void SetWalkAnimationTrigger()
    {
        animator.SetFloat(AnimationTriggerNames.MoveSpeed, movement.SelfRigidbody.velocity.magnitude);
    }

    public void PlayShootAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Shoot);
    }
    
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Die);
    }

    #endregion
}
