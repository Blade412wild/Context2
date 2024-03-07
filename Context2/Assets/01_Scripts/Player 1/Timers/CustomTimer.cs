using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



[CreateAssetMenu(fileName = "CustomTimer", menuName = "ScriptableObjects/CustomTimer", order = 2)]
public class CustomTimer : ScriptableObject
{
    [Header("TimerSettings")]
    [SerializeField] private float timerDuration;
    [SerializeField] private bool repeat;
    [SerializeField] private int repeatAmount;
    [SerializeField] private bool giveTimeBack;

    [SerializeField] private event Action hallo;

    public void StartTimer()
    {
        //TimerManager.Instance.AddTimerToList(new Timer());
    }


}
