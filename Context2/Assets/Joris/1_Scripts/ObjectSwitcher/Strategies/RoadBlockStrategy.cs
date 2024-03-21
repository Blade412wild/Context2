using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RoadBlockStrategy", menuName = "ObjSwitchStrategy/RoadBlockStrategy")]
public class RoadBlockStrategy : ObjectSwitchStrategy
{
    [SerializeField] private GameObject _player;

    public override void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
    {
        new RoadBlockBuilder()
            .WithAgent(_player)
            .WithDestination(_player)
            .WithPosition(_player)
            .Build(obj, type, active);
    }

    private class RoadBlockBuilder
    {
        private NavMeshAgent _playerAgent;
        private Vector3 _playerDestinion;
        private Vector3 _playerPosition;

        public RoadBlockBuilder WithAgent(GameObject player)
        {
            _playerAgent = player.GetOrAdd<NavMeshAgent>();
            return this;
        }

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

        public void Build(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
        {
            IObjectSwitch[] filterTypes = obj.OrderBy(t => t.SwitchType.Equals(type)).ToArray();
            IObjectSwitch[] nearestAvailableTypes = filterTypes.OrderBy(t => t.GetState().Equals(active))
                .OrderBy(t => (t.Position - _playerPosition).sqrMagnitude).ToArray();

            foreach (var roadblock in nearestAvailableTypes)
            {
                roadblock.SwitchNevMeshObstacle(!active);

                if (_playerAgent.HasPath(_playerDestinion))
                    roadblock.SwitchNevMeshObstacle(active);
                else
                {
                    roadblock.SwitchNevMeshObstacle(!active);
                    break;
                }
            }
        }
    }
}
