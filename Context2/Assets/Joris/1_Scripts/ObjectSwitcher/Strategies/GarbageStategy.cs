using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GarbageStategy", menuName = "ObjSwitchStrategy/GarbageStategy")]
public class GarbageStategy : ObjectSwitchStrategy
{
    [SerializeField] private int amount = 1;

    public override void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
    {
        new GarbageBuilder()
            .WithAmount(amount)
            .Build(obj, type, active);
    }

    private class GarbageBuilder
    {
        private int amount;

        public GarbageBuilder WithAmount(int amount)
        {
            this.amount = amount;
            return this;
        }

        public void Build(IObjectSwitch[] obj, ObjectSwitchType type, bool active)
        {
            IObjectSwitch[] filterTypes = obj.Where(t => t.SwitchType.Equals(type)).ToArray();

            foreach (IObjectSwitch item in filterTypes)
            {
                for (int i = 0; i < amount; i++)
                    item.SwitchMeshesRandom(active);
            }
        }
    }
}
