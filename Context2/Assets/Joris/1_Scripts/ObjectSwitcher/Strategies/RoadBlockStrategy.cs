using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

// NIET NAAR DEZE CLASS KIJKEN BRO

public interface IPlayerTracer 
{
    GameObject GetGameObject { get; }
    NavMeshAgent GetNavAgent { get; }
}

[CreateAssetMenu(fileName = "RoadBlockStrategy", menuName = "ObjSwitchStrategy/RoadBlockStrategy")]
public class RoadBlockStrategy : ObjectSwitchStrategy
{
    private GameObject _player;
    private NavMeshAgent _playerAgent;

    public override void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
    {
        if (_player == null)
        {
            List<IPlayerTracer> IPlayerTracerList = FindAllTracers();

            static List<IPlayerTracer> FindAllTracers()
            {
                IEnumerable<IPlayerTracer> tracers = FindObjectsOfType<MonoBehaviour>()
                    .OfType<IPlayerTracer>();

                return new List<IPlayerTracer>(tracers);
            }

            IPlayerTracerList.ToArray();

            _player = IPlayerTracerList[0].GetGameObject;
            _playerAgent = IPlayerTracerList[0].GetNavAgent;
        }

        new RoadBlockBuilder()
            .WithDestination(_player)
            .WithPosition(_player)
            .Build(_playerAgent, obj, type, active);
    }

    private class RoadBlockBuilder
    {
        private Vector3 _playerDestinion;
        private Vector3 _playerPosition;

        public RoadBlockBuilder WithDestination(GameObject player)
        {
            _playerDestinion = player.GetOrAdd<DestinationManager>().CurrentDestination;
            return this;
        }

        public RoadBlockBuilder WithPosition(GameObject player)
        {
            _playerPosition = player.transform.position;
            return this;
        }

        public void Build(NavMeshAgent agent, IObjectSwitch[] obj, ObjectSwitchType type, bool active)
        {
            IObjectSwitch[] filterTypes = obj.Where(t => t.SwitchType.Equals(type)).ToArray();
            IObjectSwitch[] nearestAvailableTypes = filterTypes.OrderBy(t => t.GetState().Equals(active))
                .OrderBy(t => (t.Position - _playerPosition).sqrMagnitude).ToArray();

            foreach (var roadblock in nearestAvailableTypes)
            {
                roadblock.SwitchNevMeshObstacle(active);

                if (agent.HasPath(_playerDestinion))
                    roadblock.SwitchMeshes(active);
                else
                {
                    roadblock.SwitchNevMeshObstacle(!active);
                    break;
                }
            }
        }
    }
}
