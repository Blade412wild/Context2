using System.Collections.Generic;
using System.Linq;
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

        Debug.Log(GetClosestInstance(transform.position, ObjectSwitchType.RoadBlock));
    }

    public IObjectSwitch GetClosestInstance(Vector3 referencePos, ObjectSwitchType objType)
    {
        var nType = _objectSwitchTracers.OrderBy(t => t.GetSwitchType().Equals(objType)).ToArray();
        var naType = nType.OrderBy(t => t.GetState().Equals(false)).ToArray();
        var naTypePos = nType.OrderBy(t => (t.GetPosition() - referencePos).sqrMagnitude).Take(3).ToArray();

        return naTypePos[^1];
    }
}
