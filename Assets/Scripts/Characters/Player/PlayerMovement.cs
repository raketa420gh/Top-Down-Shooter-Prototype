using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region MyRegion

    [Header("Movement Settings")] 
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D selfRigibody;
    private Transform bodyTransform;

    #endregion

    
    #region Properties

    public float MoveSpeed => moveSpeed;
    public Rigidbody2D SelfRigidbody => selfRigibody;

    #endregion
    

    #region Unity lifecycle

    private void Awake()
    {
        selfRigibody = GetComponent<Rigidbody2D>();
        bodyTransform = transform;
    }

    #endregion


    #region Public methods

    public void Move(Vector3 direction)
    {
        selfRigibody.velocity = direction.normalized * (moveSpeed * Time.fixedDeltaTime); 
        Vector2.ClampMagnitude(direction, 1);
    }

    public void Rotate(Vector3 direction)
    {
        bodyTransform.up = direction;
    }

    #endregion
}
