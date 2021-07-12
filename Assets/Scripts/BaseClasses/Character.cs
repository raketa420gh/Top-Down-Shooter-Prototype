using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Variables

    [Header("Character Settings")] 
    [SerializeField] [Min(1)] private int maxHealth;
    
    private int currentHealth;
    private bool isAlive = true;
    
    #endregion
    
    
    #region Properties
    
    public bool IsAlive => isAlive;
    
    #endregion


    #region Events
    
    public event Action OnCreated;
    public event Action OnDied;
    public event Action<float> OnHealthPercentChanged = delegate {  };
    
    #endregion


    #region Unity lifecycle

    protected virtual void Awake()
    {
        OnCreated?.Invoke();
    }

    protected virtual void Start()
    {
        RestoreHealth();
    }

    #endregion
    
    
    #region Public Methods

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} получил {damageAmount} урона. Осталось {currentHealth}.");

        UpdateHealthBar();
        
        if (isAlive && currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        
        UpdateHealthBar();

        if (currentHealth > maxHealth)
        {
            RestoreHealth();
        }
    }

    #endregion


    #region Private methods

    private void RestoreHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        var currentHealthPercent = (float) currentHealth / (float) maxHealth;
        OnHealthPercentChanged?.Invoke(currentHealthPercent);
    }

    protected virtual void Death()
    {
        isAlive = false;
        Debug.Log($"{gameObject.name} погиб.");
        Destroy(gameObject, 120f);
    }
    
    protected void InvokeOnDied()
    {
        OnDied?.Invoke();
    }
    
    #endregion

}
