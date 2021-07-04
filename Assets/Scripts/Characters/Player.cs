using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player : Character
{
    #region Variables

    [Header("Shoot Settings")] 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform parentProjectile;
    [SerializeField] private float shootCooldown;

    private PlayerMovement movement;
    private PlayerAnimation animation;
    private Collider2D selfCollider;
    private SpriteRenderer selfSpriteRenderer;
    private HealthBar healthBar;
    private Camera camera;
    private float currentShootCooldown;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        animation = GetComponent<PlayerAnimation>();
        selfCollider = GetComponent<Collider2D>();
        selfSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        camera = Camera.main;
        
        InvokeOnCreated();
    }

    private void Update()
    {
        if (!IsAlive)
        {
            return;
        }
        
        Move();
        Rotate();

        if (Input.GetButton(AxesNames.Fire1))
        {
            Shooting();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null && other.gameObject.CompareTag(TagNames.EnemyBullet))
        {
            base.TakeDamage(damageDealer.Damage);
            Destroy(other.gameObject);
        }
    }

    #endregion


    #region Private methods

    private void Move()
    {
        var inputAxesDirection = new Vector2(Input.GetAxis(AxesNames.Horizontal), Input.GetAxis(AxesNames.Vertical));
        
        movement.Move(inputAxesDirection);
    }

    private void Rotate()
    {
        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDirection = transform.position - mousePosition;
        
        movement.Rotate(mouseDirection);
    }

    private void InstantiateBullet()
    {
        Instantiate(bulletPrefab, muzzle.position, transform.rotation, parentProjectile);
    }

    private void Shooting()
    {
        if (currentShootCooldown <= 0)
        {
            currentShootCooldown = shootCooldown;
            InstantiateBullet();
            animation.PlayShootAnimation();
        }

        currentShootCooldown -= Time.deltaTime;
    }

    protected override void Death()
    {
        base.Death();
        animation.PlayDeathAnimation();
        selfSpriteRenderer.sortingOrder = -1;
        healthBar.enabled = false;
        selfCollider.enabled = false;
        movement.enabled = false;
        movement.SelfRigidbody.Sleep();
        
        InvokeOnDied();
    }

    #endregion
}
