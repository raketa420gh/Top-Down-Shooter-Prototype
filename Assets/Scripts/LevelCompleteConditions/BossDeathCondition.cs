using UnityEngine;

public class BossDeathCondition : LevelCompleteCondition
{
    #region Variables

    [SerializeField] private Character boss;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        boss.OnDied += BossOnDied;
    }

    private void OnDisable()
    {
        boss.OnDied -= BossOnDied;
    }

    #endregion


    #region Events Handlers

    private void BossOnDied()
    {
        InvokeOnCompleted();
    }

    #endregion
}
