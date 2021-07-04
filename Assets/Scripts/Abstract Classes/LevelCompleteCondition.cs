using System;
using UnityEngine;

public abstract class LevelCompleteCondition : MonoBehaviour
{
    #region Events

    public event Action OnCompleted; // Don't delete, realized with wrapper

    #endregion
    

    #region Private methods

    protected void InvokeOnCompleted()
    {
        OnCompleted?.Invoke();
    }

    #endregion
}
