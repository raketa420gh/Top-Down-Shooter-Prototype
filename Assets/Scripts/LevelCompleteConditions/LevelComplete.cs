using System;
using System.Collections;
using UnityEngine; 

public class LevelComplete : MonoBehaviour
{
    #region Variables

    [SerializeField] private float completeDelayInSeconds;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private LevelCompleteCondition[] conditions;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        DoWithConditions(condition => condition.OnCompleted += LevelCompleteConditionOnCompleted);
    }

    private void OnDisable()
    {
        DoWithConditions(condition => condition.OnCompleted -= LevelCompleteConditionOnCompleted);
    }

    #endregion


    #region Private methods

    private void DoWithConditions(Action<LevelCompleteCondition> action)
    {
        if (conditions == null)
        {
            return;
        }

        foreach (var condition in conditions)
        {
            action?.Invoke(condition);
        }
    }
    
    #endregion


    #region Coroutines

    private IEnumerator LoadNextSceneWithPause(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);

        sceneLoader.LoadNextScene();
    }

    #endregion


    #region Event Handlers

    private void LevelCompleteConditionOnCompleted()
    {
        StartCoroutine(LoadNextSceneWithPause(completeDelayInSeconds));
    }

    #endregion
}
