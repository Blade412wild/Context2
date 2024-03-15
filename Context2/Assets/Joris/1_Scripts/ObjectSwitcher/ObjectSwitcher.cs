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
    }

    bool IObjectSwitch.GetState()
    {
        foreach (GameObject obj in _objects)
            if (obj.activeInHierarchy == true)
                return true;

        return false;
    }

    void IObjectSwitch.Activate()
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(true);
    }

    void IObjectSwitch.Deactivate()
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(false);
    }

    Vector3 IObjectSwitch.Position => transform.position;
    ObjectSwitchType IObjectSwitch.SwitchType => _switchType;

    void IObjectSwitch.ActivateNavMeshObstacle() => _navMeshBlock.SetActive(true);

    void IObjectSwitch.DeactivateNavMeshObstacle() => _navMeshBlock.SetActive(false);
}