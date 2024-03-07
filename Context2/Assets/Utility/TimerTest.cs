using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerTest : MonoBehaviour
{

    [SerializeField] private KeyCode keyInput;
    [SerializeField] private CustomTimer TimerSettings;
    [SerializeField] private int timerDuration;
    private Timer dayTimer;
    
    

    

    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI TimerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyInput))
        {
            TimerManager.Instance.AddTimerToList(dayTimer = new Timer(timerDuration));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            TimerManager.Instance.AddTimerToList(new Timer(timerDuration, true, 3));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TimerManager.Instance.AddTimerToList(new Timer(timerDuration, true));
        }

        if(dayTimer != null)
        {
            TimerText.text = "Time Left : " + dayTimer.ShowTime().ToString();
        }
    }
}
