using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndSequence : MonoBehaviour
{
    [SerializeField] private CustomTimer timer;


    [Space]
    [Header("UI objecten")]
    [SerializeField] private GameObject UIpanel;
    [SerializeField] private GameObject UIInput;

    [SerializeField] TextMeshProUGUI speeltijd;
    [SerializeField] TextMeshProUGUI bezorgingen;
    [SerializeField] TextMeshProUGUI gemBezorging;
    [SerializeField] Image score;

    [Space]
    [Header("sprites & image")]
    [SerializeField] Sprite scoreS;
    [SerializeField] Sprite scoreA;
    [SerializeField] Sprite scoreB;
    [SerializeField] Sprite scoreC;


    private bool mayQuit = false;
    private StatTracker statTracker;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartEndSequence()
    {
        statTracker = FindAnyObjectByType<StatTracker>();
        openUI();
        SetUI();

        timer.CreateTimer();
        timer.timerInstance.OnTimerIsDonePublic += ActiveInput;
    }

    private void Update()
    {
        if (mayQuit == false) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
    private void openUI()
    {
        UIpanel.SetActive(true);
    }

    private void ActiveInput()
    {
        UIInput.SetActive(true);
        mayQuit = true; 
    }

    private void SetUI()
    {
        if (statTracker == null) return;

        float timePlayed = statTracker.playTime / 60;
        int time = (int)timePlayed;

        int gemTime = (int)statTracker.gemTime;

        speeltijd.text = "Speeltijd : " + time.ToString() + " min";
        bezorgingen.text = "Bezorgingen : " + statTracker.deliveriesMade.ToString();
        gemBezorging.text = "Tijd per bezorging : " + gemTime.ToString() + " sec"; ;

        switch (statTracker.gemScore)
        {
            case 0: score.sprite = scoreS; break;
            case 1: score.sprite = scoreA; break;
            case 2: score.sprite = scoreB; break;
            case 3: score.sprite = scoreC; break;
        }

    }
}
