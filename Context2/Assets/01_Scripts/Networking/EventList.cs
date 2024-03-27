using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventList", menuName = "ScriptableObjects/EventList", order = 1)]

public class EventList : ScriptableObject
{
    [SerializeField] public List<GameEvent> gameEvents = new List<GameEvent>();
}
