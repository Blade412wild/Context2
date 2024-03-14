using UnityEngine;

public class ObjectSwitcher : MonoBehaviour, IObjectSwitch
{
    [SerializeField]
    private GameObject[] _objects;

    [SerializeField]
    private ObjectSwitchType _switchType;

    public void Awake()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj.activeInHierarchy)
                obj.SetActive(false);
        }
    }

    public void Activate()
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(true);
    }

    public void Deactivate()
    {
        foreach (GameObject obj in _objects)
            obj.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool GetState()
    {
        bool state = false;

        foreach (GameObject obj in _objects)
        {
            if (obj.activeInHierarchy)
                state = true;
        }

        return state;
    }

    public ObjectSwitchType GetSwitchType()
    {
        return _switchType;
    }
}