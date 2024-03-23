using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ParkStrategy", menuName = "ObjSwitchStrategy/ParkStrategy")]
public class ParkStrategy : ObjectSwitchStrategy
{
    public override void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
    {
        IObjectSwitch[] filterTypes = obj.Where(t => t.SwitchType.Equals(type)).ToArray();

        foreach(IObjectSwitch item in filterTypes)
        {
            item.SwitchMeshes(active);
        }
    }
}
