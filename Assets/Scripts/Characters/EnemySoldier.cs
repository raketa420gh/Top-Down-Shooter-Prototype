using UnityEngine;

public class EnemySoldier : Character
{
    #region Variables
    
    [Header("Vision Settings")] 
    [SerializeField] [Range(3f, 15f)] private float visionRadius;
    
    [Header("Gun Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform parentProjectile;
    [SerializeField] private float shootCooldown;
    
    private Player player;
    private Animator animator;
    private Collider2D selfCollider;
    private Rigidbody2D selfRigibody;
    private Vector3 playerPosition;
    private Vector3 selfPosition;
    private float distanceToPlayer;
    private float currentShootCooldown;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        selfCollider = GetComponent<Collider2D>();
        selfRigibody = GetComponent<Rigidbody2D>();
    }
    
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!IsAlive || !player.IsAlive)
        {
            return;
        }
        
        UpdateDistanceToPlayer();
        UpdateCurrentState();
    }
    
    private void OnDrawGizmos()
    {
        selfPosition = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(selfPosition, visionRadius);
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
        distanceToPlayer = Vector3.Distance(selfPosition, playerPosition);
    }
    
    private void UpdateCurrentState()
    {
        if (distanceToPlayer < visionRadius)
        {
            AttackState();
        }
        else
        {
            IdleState();
        }
    }
    
    private void AttackState()
    {
        Rotate();
        Shoot();
    }
    
    private void IdleState()
    {
        selfRigibody.velocity = Vector2.zero;
    }

    private void InstantiateBullet()
    {
        var transformSelf = transform;
        Instantiate(bulletPrefab, muzzle.position, transformSelf.rotation, parentProjectile);
    }

    private void PlayShootAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Shoot);
    }

    private void PlayDeathAnimation()
    {
        animator.SetTrigger(AnimationTriggerNames.Die);
    }
    
    private void Rotate()
    {
        selfPosition = transform.position;
        playerPosition = player.transform.position;
        Vector2 direction = selfPosition - playerPosition;
        
        transform.up = direction;
    }
    
    protected override void Death()
    {
        base.Death();
        PlayDeathAnimation();
        selfCollider.enabled = false;
    }

    private void Shoot()
    {
        if (currentShootCooldown <= 0)
        {
            currentShootCooldown = shootCooldown;
            InstantiateBullet();
            PlayShootAnimation();
        }

        currentShootCooldown -= Time.deltaTime;
    }

    #endregion
}