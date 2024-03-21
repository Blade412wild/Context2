using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSwitcherManager : PersistentSingleton<ObjectSwitcherManager>
{
    [SerializeField, Space(10)]
    private List<ObjectSwitchStrategy> _objSwitchStrategy;

    private IObjectSwitch[] _objectSwitchTracers;

    private void Start()
    {
        List<IObjectSwitch> objectSwitchTracersList = FindAllDataPersistanceObjects();

        static List<IObjectSwitch> FindAllDataPersistanceObjects()
        {
            IEnumerable<IObjectSwitch> objectSwitchObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IObjectSwitch>();

            return new List<IObjectSwitch>(objectSwitchObjects);
        }

        _objectSwitchTracers = objectSwitchTracersList.ToArray();
    }

    public void OnEvent(ObjectSwitchType type, bool active) => _objSwitchStrategy[(int)type].PerformStrategy(_objectSwitchTracers, type, active);
}