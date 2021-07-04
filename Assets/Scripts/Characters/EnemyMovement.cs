using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    
    [Header("Movement Settings")]
    [SerializeField] [Min(1f)] private float moveSpeed;
    
    private Transform playerBodyTransform;
    private Rigidbody2D selfRigibody;

    #endregion


    #region Properties

    public float MoveSpeed => moveSpeed;
    public Rigidbody2D SelfRigidbody2D => selfRigibody;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        playerBodyTransform = transform;
        selfRigibody = GetComponent<Rigidbody2D>();
    }

    #endregion


    #region Public methods

    public void Move(Vector3 direction)
    {
        selfRigibody.velocity = direction.normalized * (moveSpeed * Time.fixedDeltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        playerBodyTransform.up = direction;
    }

    public void SetVelocitiesToZero()
    {
        selfRigibody.velocity = Vector2.zero;
        selfRigibody.angularVelocity = 0;
    }

    public void RbSleepOn()
    {
        selfRigibody.Sleep();
    }

    #endregion
}
