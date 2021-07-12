using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]

public class NPCMovement : MonoBehaviour
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
        SetDestinationTarget(null);
    }

    #endregion


    #region Public methods

    public void ActivateAIPath(bool isActive)
    {
        aiPath.enabled = isActive;
    }

    public void SetDestinationTarget(Transform target)
    {
        aiDestinationSetter.target = target;
    }

    #endregion
}
