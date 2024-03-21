using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Summary: 
//      Attach to Player!
//      
//      Calculates new Destinations based on navmesh pathfinding.
public class DestinationManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _playerAgent;
    [SerializeField] private List<Transform> _destinationsEditor;

    public Vector3 CurrentDestination { get; private set; }

    private Transform[] _destinations;

    private void Awake()
    {
        gameObject.GetOrAdd<NavMeshAgent>();
        _destinations = _destinationsEditor.ToArray();
    }

    private void NextDestination()
    {
        Vector3 newPos = _destinations.RandomItem().position;

        if (_playerAgent.HasPath(newPos))
            SetDestination(newPos);
        else
            NextDestination();
    }

    private void SetDestination(Vector3 pos)
    {
        CurrentDestination = pos;

        // Whatever the fuck is supposed to be happening here!!
    }
}