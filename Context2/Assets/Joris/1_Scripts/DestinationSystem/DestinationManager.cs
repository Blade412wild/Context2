using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Summary: 
//      Attach to Player!
//      
//      Calculates new Destinations based on navmesh pathfinding.
public class DestinationManager : MonoBehaviour
{
    [SerializeField] private GameObject deliveryPointPrefab;
    [SerializeField] private NavMeshAgent _playerAgent;
    [SerializeField] private List<Transform> _destinationsEditor;

    private int _recursionExit = 10;
    private int _recursionExitCount = 0;

    public Vector3 CurrentDestination { get; private set; }

    private Transform[] _destinations;

    private void Awake()
    {
        _playerAgent.gameObject.GetOrAdd<NavMeshAgent>();
        _destinations = _destinationsEditor.ToArray();
    }

    private void Start()
    {
        NextDestination();
    }

    public void NextDestination()
    {
        Vector3 newPos = _destinations.RandomItem().position;

        _recursionExitCount++;
        if (_recursionExitCount == _recursionExit)
            return;

        if (newPos.Equals(CurrentDestination))
            NextDestination();

        if (_playerAgent.HasPath(newPos))
            SetDestination(newPos);
        else
            NextDestination();
    }

    private void SetDestination(Vector3 pos)
    {
        CurrentDestination = pos;
        _recursionExitCount = 0;

        Instantiate(deliveryPointPrefab, CurrentDestination, Quaternion.identity);
    }
}