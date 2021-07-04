using System;
using UnityEngine;

public class Exploded : MonoBehaviour
{
    #region Variables

    [SerializeField] [Min(0.5f)] private float explosionRadius;
    [SerializeField] [Min(10f)] private int damage;

    #endregion


    #region Events

    public static event Action OnDestroy; 

    #endregion
    

    #region Unity lifecycle
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,  explosionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();
        
        if (damageDealer != null && other.gameObject.CompareTag(TagNames.PlayerBullet))
        {
            BlowUp();
        }
    }

    #endregion
    

    #region Private methods

    private void BlowUp()
    {
       DamageForRadius();
       Destroy(gameObject);
       OnDestroy?.Invoke();
    }

    private void DamageForRadius()
    {
        var layerMask = LayerMask.GetMask(LayerNames.Enemy, LayerNames.Player);
        var collidersInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);

        foreach (Collider2D collider in collidersInRadius)
        {
            var character = collider.GetComponent<Character>();
            character.TakeDamage(damage);
        }
    }

    #endregion
}
