using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PostPressingStrategy", menuName = "ObjSwitchStrategy/PostPressingStrategy")]
public class PostPressingStrategy : ObjectSwitchStrategy
{
    public override void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
    {
        IObjectSwitch[] filterTypes = obj.OrderBy(t => t.SwitchType.Equals(type)).ToArray();

        foreach(IObjectSwitch volume in filterTypes)
        {
            volume.SwitchMeshes(active);
        }
    }
}
