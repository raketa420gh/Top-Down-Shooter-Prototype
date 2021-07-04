using UnityEngine;

public class EnterTriggerCondition : LevelCompleteCondition
{
    #region Unity lifecycle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagNames.Player))
        {
            InvokeOnCompleted();
        }
    }

    #endregion
}
