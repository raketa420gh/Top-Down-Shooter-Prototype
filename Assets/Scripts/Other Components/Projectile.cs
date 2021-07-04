using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private float speed = 1000;
    [SerializeField] private float destroyTime = 3f;
    
    private Rigidbody2D rigidbody;
    
    #endregion
    

    #region Unity lifecycle

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = speed * Time.fixedDeltaTime * -transform.up;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagNames.Obstacle))
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
