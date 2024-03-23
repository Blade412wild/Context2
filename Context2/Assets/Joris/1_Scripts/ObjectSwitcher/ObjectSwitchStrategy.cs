using UnityEngine;

public abstract class ObjectSwitchStrategy : ScriptableObject
{
    public abstract void PerformStrategy(IObjectSwitch[] obj, ObjectSwitchType type, bool active);
}

public interface IObjectSwitch
{
    ObjectSwitchType SwitchType { get; }
    Vector3 Position { get; }

    bool GetState();

    void SwitchMeshes(bool active);
    void SwitchMeshesRandom(bool active);
    void SwitchNevMeshObstacle(bool active);
}

public enum ObjectSwitchType
{
    RoadBlock = 0,
    Garbage = 1,
    PostProcessing = 2,
    Park = 3,
    Grannies = 4,
    Trees = 5
}