using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAnimation))]

public class Zombie : Character
{
    #region Variables
    
    [Header("Vision Settings")]
    [SerializeField] [Range(5f, 15f)] private float visionRadius;
    [SerializeField] [Range(2f, 5f)] private float attackRadius;
    [SerializeField] private LayerMask obstacleMask;
    
    [Header("Attack Settings")]
    [SerializeField] [Min(1)] private int attackDamage;
    [SerializeField] [Min(0.5f)] private float attackCooldown;
    
    [Header("States Settings")]
    [SerializeField] private StateMachine zombieStateMachine;
    [SerializeField] private ZombieIdleState idleState;
    [SerializeField] private ZombieChasingState chasingState;
    [SerializeField] private ZombieAttackState attackState;
    
    [Header("Read Only")]
    [ReadOnly] [SerializeField] private State currentState;
    [ReadOnly] [SerializeField] private float attackTimer;

    private EnemyMovement movement;
    private EnemyAnimation animation;
    private Player player;
    private Collider2D selfCollider;
    private SpriteRenderer selfSpriteRenderer;
    private HealthBar healthBar;
    
    private Vector3 playerPosition;
    private Vector3 selfPosition;
    private Vector3 playerDirection;
    private float distanceToPlayer;

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
    
    private void Awake()
    {
        animation = GetComponent<EnemyAnimation>();
        movement = GetComponent<EnemyMovement>();
        selfCollider = GetComponent<Collider2D>();
        selfSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        
        InvokeOnCreated();
    }

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        attackTimer = 0;
        
        zombieStateMachine = new StateMachine();
        idleState = new ZombieIdleState(this, zombieStateMachine);
        chasingState = new ZombieChasingState(this, zombieStateMachine);
        attackState = new ZombieAttackState(this, zombieStateMachine);
        
        zombieStateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        ZombieAnimationEventHandler.OnAttacked += ZombieAnimationEventHandlerOnAttacked;
    }

    private void OnDisable()
    {
        ZombieAnimationEventHandler.OnAttacked -= ZombieAnimationEventHandlerOnAttacked;
    }

    private void Update()
    {
        if (!IsAlive || !player.IsAlive)
        {
            return;
        }
        
        UpdateDistanceToPlayer();
        zombieStateMachine.CurrentState.LogicUpdate();
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

    
    #region Private methods
    
    private void UpdateDistanceToPlayer()
    {
        selfPosition = transform.position;
        playerPosition = player.transform.position;
        playerDirection = playerPosition - selfPosition;
        distanceToPlayer = Vector3.Distance(selfPosition, playerPosition);
    }

    private void UpdateStates()
    {
        if (distanceToPlayer < attackRadius)
        {
            //SetState(State.Attack);
        }
        else if (distanceToPlayer < visionRadius)
        {
            //SetState(State.Chasing);
        }
        else
        {
            //SetState(State.Idle);
        }
    }
    

    protected override void Death()
    {
        base.Death();
        animation.ActivateTriggerDie(true);
        animation.SetBoolIsMoving(false);
        selfSpriteRenderer.sortingOrder = -1;
        movement.SetTargetToChase(null);
        movement.ActivateAIPath(false);
        selfCollider.enabled = false;
        healthBar.gameObject.SetActive(false);
        
        InvokeOnDied();
    }

    private void ResetAttackTimer()
    {
        attackTimer = attackCooldown;
    }
    
    #endregion


    #region Event Handlers

    private void ZombieAnimationEventHandlerOnAttacked()
    {
        player.TakeDamage(attackDamage);
    }

    #endregion
}
