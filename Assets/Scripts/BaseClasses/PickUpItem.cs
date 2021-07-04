using UnityEngine;

public abstract class PickUpItem : MonoBehaviour
{
    #region Unity lifecycle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagNames.Player))
        {
            ApplyEffect();
            Debug.Log($"Вы подобрали {gameObject.name}.");
            Destroy(gameObject);
        }
    }

    #endregion

    #region Private methods

    protected abstract void ApplyEffect();

    #endregion
}
