using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    [SerializeField] private KeyCode KeyInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyInput))
        {
            TimerManager.Instance.AddTimerToList(new Timer(5));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            TimerManager.Instance.AddTimerToList(new Timer(5, true, 3));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TimerManager.Instance.AddTimerToList(new Timer(2, true));
        }
    }
}
