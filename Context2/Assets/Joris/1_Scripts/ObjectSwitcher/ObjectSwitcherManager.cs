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
        List<IObjectSwitch> objectSwitchTracersList = FindAllSwitchTracers();

        static List<IObjectSwitch> FindAllSwitchTracers()
        {
            IEnumerable<IObjectSwitch> objectSwitchObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IObjectSwitch>();

            return new List<IObjectSwitch>(objectSwitchObjects);
        }

        _objectSwitchTracers = objectSwitchTracersList.ToArray();
    }

    private void OnEvent(ObjectSwitchType type, bool active) => _objSwitchStrategy[(int)type].PerformStrategy(_objectSwitchTracers, type, active);

    #region HARDCODED GARBAGE (if you want to expand fix your event)
    public void RoadBlock_OFF() => OnEvent(ObjectSwitchType.RoadBlock, false);
    public void RoadBlock_ON() => OnEvent(ObjectSwitchType.RoadBlock, true);

    public void Garbage_OFF() => OnEvent(ObjectSwitchType.Garbage, false);
    public void Garbage_ON() => OnEvent(ObjectSwitchType.Garbage, true);

    public void PostProcessing_OFF() => OnEvent(ObjectSwitchType.PostProcessing, false);
    public void PostProcessing_ON() => OnEvent(ObjectSwitchType.PostProcessing, true);

    public void Park_OFF() => OnEvent(ObjectSwitchType.Park, false);
    public void Park_ON() => OnEvent(ObjectSwitchType.Park, true);

    public void Grannies_OFF() => OnEvent(ObjectSwitchType.Grannies, false);
    public void Grannies_ON() => OnEvent(ObjectSwitchType.Grannies, true);

    public void Trees_OFF() => OnEvent(ObjectSwitchType.Trees, false);
    public void Trees_ON() => OnEvent(ObjectSwitchType.Trees, true);
    #endregion
}