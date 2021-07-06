using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        SetTargetToChase(null);
    }

    #endregion


    #region Public methods

    public void ActivateAIPath(bool isActive)
    {
        aiPath.enabled = isActive;
    }

    public void SetTargetToChase(Transform target)
    {
        aiDestinationSetter.target = target;
    }

    #endregion
}
