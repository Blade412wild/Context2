using UnityEngine;

public class ObjectSwitcher : MonoBehaviour, IObjectSwitch
{
    [SerializeField]
    private ObjectSwitchType _switchType;

    [SerializeField]
    private GameObject _navMeshBlock;

    [SerializeField]
    private GameObject[] _objects;

    public void Awake()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj.activeInHierarchy)
                obj.SetActive(false);
        }

        if (_navMeshBlock != null)
            _navMeshBlock.SetActive(false);
    }

    bool IObjectSwitch.GetState()
    {
        foreach (GameObject obj in _objects)
            if (obj.activeInHierarchy == true)
                return true;

        return false;
    }

    Vector3 IObjectSwitch.Position => transform.position;

    ObjectSwitchType IObjectSwitch.SwitchType => _switchType;

    void IObjectSwitch.SwitchMeshes(bool active)
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(active);
    }

    void IObjectSwitch.SwitchMeshesRandom(bool active)
    {
        _objects.RandomItem().SetActive(active);
    }

    void IObjectSwitch.SwitchNevMeshObstacle(bool active)
    {
        _navMeshBlock.SetActive(active);
    }
}