using UnityEngine.AI;
using UnityEngine;
using System.Linq;

public static class ObjectSwitcherExtensions
{
    public static bool HasPath(this NavMeshAgent agent, Vector3 targetPos)
    {
        var path = new NavMeshPath();

        if (agent.CalculatePath(targetPos, path) && path.status == NavMeshPathStatus.PathComplete)
            return true;

        return false;
    }

    public static T RandomItem<T>(this T[] array)
    {
        if (array.Length == 0)
            throw new System.IndexOutOfRangeException("Cannot select a random item from an empty array");

        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (!component) component = gameObject.AddComponent<T>();

        return component;
    }

    public static bool IsIn<T>(this T value, params T[] list)
    {
        return list.Contains(value);
    }
}
