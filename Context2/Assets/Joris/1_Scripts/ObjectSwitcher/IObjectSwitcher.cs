using UnityEngine;

public interface IObjectSwitch
{
    bool GetState();
    void Activate();
    void Deactivate();

    Vector3 GetPosition();
    ObjectSwitchType GetSwitchType();
}