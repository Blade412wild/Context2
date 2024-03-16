using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;

public class ObjectSwitcherManager : PersistentSingleton<ObjectSwitcherManager>
{
    private List<IObjectSwitch> _objectSwitchTracers;

    private void Start()
    {
        _objectSwitchTracers = FindAllDataPersistanceObjects();

        static List<IObjectSwitch> FindAllDataPersistanceObjects()
        {
            IEnumerable<IObjectSwitch> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IObjectSwitch>();

            return new List<IObjectSwitch>(dataPersistanceObjects);
        }
    }

    public void ActivateNearestAvaiableType(ObjectSwitchType objType, NavMeshAgent agent, Vector3 agentPos, Vector3 targetPos)
    {
        // Store all found objTypes in array
        var nearestTypes = _objectSwitchTracers.OrderBy(t => t.SwitchType.Equals(objType)).ToArray();

        // Filter that array into one with only deactivated objTypes,
        // then order by distance from the player
        var nearestAvailableTypes = nearestTypes.OrderBy(t => t.GetState().Equals(false))
            .OrderBy(t => (t.Position - agentPos).sqrMagnitude).ToArray();

        // Checking if destination can still be reached with roadblock enabled, if yes, activate it
        foreach (var type in nearestAvailableTypes)
        {
            type.ActivateNavMeshObstacle();

            if (!HasPath(agent, targetPos))
                type.DeactivateNavMeshObstacle();
            else
            {
                type.Activate();
                break;
            }
        }
    }

    bool HasPath(NavMeshAgent agent, Vector3 targetPos)
    {
        var path = new NavMeshPath();

        if (agent.CalculatePath(targetPos, path) && path.status == NavMeshPathStatus.PathComplete)
            return true;

        return false;
    }
}
