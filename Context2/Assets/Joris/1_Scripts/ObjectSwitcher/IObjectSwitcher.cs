using UnityEngine;

public interface IObjectSwitch
{
    ObjectSwitchType SwitchType { get; }
    Vector3 Position { get ; }

    bool GetState();

    void Activate();
    void Deactivate();

    void ActivateNavMeshObstacle();
    void DeactivateNavMeshObstacle();
}