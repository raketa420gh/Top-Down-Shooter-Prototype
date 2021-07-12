using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(NPCAnimation))]
[RequireComponent(typeof(StateMachine))]

public class NPC : Character
{
    #region Variables
    
    [Header("Vision Settings")]
    [SerializeField] [Range(5f, 15f)] private float visionRadius;
    [SerializeField] [Range(2f, 5f)] private float attackRadius;
    [SerializeField] private LayerMask obstacleMask;
    
    [Header("Attack Settings")]
    [SerializeField] [Min(1)] private int attackDamage;
    [SerializeField] [Min(0.5f)] private float attackCooldown;

    [Header("Read Only")]
    [ReadOnly] [SerializeField] private float attackTimer;
    
    private StateMachine stateMachine;
    private IdleState idleState;
    private ChaseState chaseState;
    private AttackState attackState;

    private NPCMovement movement;
    private NPCAnimation animation;
    private Player player;
    private Collider2D selfCollider;
    private SpriteRenderer selfSpriteRenderer;
    private HealthBar healthBar;
    private Vector3 playerPosition;
    private Vector3 selfPosition;
    private Vector3 playerDirection;
    private float distanceToPlayer;
    
    #endregion
    
    
    #region Properties

    public float VisionRadius => visionRadius;
    public float AttackRadius => attackRadius;
    public int AttackDamage => attackDamage;
    public float AttackTimer => attackTimer;
    public LayerMask ObstacleMask => obstacleMask;
    public NPCMovement Movement => movement;
    public NPCAnimation Animation => animation;
    public Player Player => player;
    public Collider2D SelfCollider => selfCollider;
    public SpriteRenderer SelfSpriteRenderer => selfSpriteRenderer;
    public HealthBar HealthBar => healthBar;
    public Vector3 PlayerPosition => playerPosition;
    public Vector3 SelfPosition => selfPosition;
    public Vector3 PlayerDirection => playerDirection;
    public float DistanceToPlayer => distanceToPlayer;

    #endregion
    
    
    #region Unity lifecycle
    
    private void OnDrawGizmos()
    {
        selfPosition = transform.position;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(selfPosition, visionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(selfPosition, attackRadius);
    }

    protected override void Awake()
    {
        animation = GetComponent<NPCAnimation>();
        movement = GetComponent<NPCMovement>();
        selfCollider = GetComponent<Collider2D>();
        selfSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        stateMachine = GetComponent<StateMachine>();
        
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        attackTimer = 0;
        
        idleState = new IdleState(this, stateMachine);
        chaseState = new ChaseState(this, stateMachine);
        attackState = new AttackState(this, stateMachine);
        
        stateMachine.Initialize(idleState);
    }
    
    private void Update()
    {
        if (!IsAlive || !player.IsAlive)
        {
            return;
        }
        
        UpdateDistanceToPlayer();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null && other.gameObject.CompareTag(TagNames.PlayerBullet))
        {
            TakeDamage(damageDealer.Damage);
            Destroy(other.gameObject);
        }
    }

    #endregion


    #region Public methods
    
    public void ResetAttackTimer()
    {
        attackTimer = attackCooldown;
    }

    public void SubtractAttackTime()
    {
        attackTimer -= Time.deltaTime;
    }

    #endregion

    
    #region Private methods
    
    private void UpdateDistanceToPlayer()
    {
        selfPosition = transform.position;
        playerPosition = player.transform.position;
        playerDirection = playerPosition - selfPosition;
        distanceToPlayer = Vector3.Distance(selfPosition, playerPosition);
    }

    protected override void Death()
    {
        base.Death();
        animation.ActivateTriggerDie();
        animation.SetBoolIsMoving(false);
        selfSpriteRenderer.sortingOrder = -1;
        movement.SetDestinationTarget(null);
        movement.ActivateAIPath(false);
        selfCollider.enabled = false;
        healthBar.gameObject.SetActive(false);
        
        InvokeOnDied();
    }
    
    #endregion
}
