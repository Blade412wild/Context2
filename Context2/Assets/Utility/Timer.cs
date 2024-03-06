using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Timer
{
    public event Action OnTimerIsDone;
    public event Action<Timer> OnRemoveTimer;


    // timer 
    private float startTime = 0;
    private float currentTime;
    private int endTime;
    private bool repeat = false;
    private int repeatAmount = 0;
    private int currentAmount = 0;


    public Timer(int _seconds, bool _repeat, int _amount)
    {
        endTime = _seconds;
        repeat = _repeat;
        repeatAmount = _amount;

    }

    public Timer(int _seconds)
    {
        endTime = _seconds;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        currentTime += Time.deltaTime;
        //Debug.Log(currentTime);
        RunTimer();
    }

    private void RunTimer()
    {
        //int currentAmount = 0;
        //float startTime = currentTime;

        if(currentTime >= endTime)
        {
            Debug.Log(" Timer is finished, " + endTime + "have past");
            if (repeat == true && currentAmount <= repeatAmount)
            {
                Debug.Log("repeat Timer");
                currentTime = 0;
                currentAmount++;
            }
            else
            {
                OnRemoveTimer?.Invoke(this);
            }
        }
    }
}
