using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManager : MonoBehaviour
{
    public int deliveriesMade;

    [Space]
    [Header("Timer")]
    public float passedTime;
    public enum Ranks { S, A, B, C };
    Ranks currentRank;
    [SerializeField] float sRankTime = 30;
    [SerializeField] float aRankTime = 45;
    [SerializeField] float bRankTime = 60;
    [SerializeField] TextMeshProUGUI timerText;
    [Space]
    [SerializeField] Slider timeBar;

    [Space]
    [Header("UI")]
    [SerializeField] GameObject sRankPopup;
    [SerializeField] GameObject aRankPopup;
    [SerializeField] GameObject bRankPopup;
    [SerializeField] GameObject cRankPopup;
    [SerializeField] TextMeshProUGUI deliveriesMadeText;
    [SerializeField] TextMeshProUGUI rankInfoText;

    Animator timerAnimator;
    float second = 0;

    // Start is called before the first frame update
    void Start()
    {
        deliveriesMadeText.text = "Deliveries made: " + deliveriesMade.ToString();
        timerAnimator = timerText.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTime();
        TimeSlider();

        timerText.text = passedTime.ToString("F2");

        if (currentRank == Ranks.S)
        {
            rankInfoText.text = "/ " + sRankTime.ToString("F2") + " - " + currentRank.ToString() + " Rank";
        }
        if (currentRank == Ranks.A)
        {
            rankInfoText.text = "/ " + aRankTime.ToString("F2") + " - " + currentRank.ToString() + " Rank";
        }
        if (currentRank == Ranks.B)
        {
            rankInfoText.text = "/ " + bRankTime.ToString("F2") + " - " + currentRank.ToString() + " Rank";
        }
        if (currentRank == Ranks.C)
        {
            rankInfoText.text = currentRank.ToString() + " Rank";
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            Time.timeScale = 3;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void Delivered()
    {
        // Display Aquired Rank
        Debug.Log("Rank " + currentRank + "! ~ Delievered in " + passedTime + " seconds.");
        if (currentRank == Ranks.S) { sRankPopup.SetActive(true); }
        else if (currentRank == Ranks.A) { aRankPopup.SetActive(true); }
        else if (currentRank == Ranks.B) { bRankPopup.SetActive(true); }
        else if (currentRank == Ranks.C) { cRankPopup.SetActive(true); }

        // Reset time
        passedTime = 0;
        second = 0;

        // Increase delivery counter
        deliveriesMade++;
        deliveriesMadeText.text = "Deliveries made: " + deliveriesMade.ToString();
    }

    void IncreaseTime()
    {
        passedTime += Time.deltaTime;

        second += Time.deltaTime;
        if (second >= 1) { second = 0; timerAnimator.SetTrigger("shake"); }

        // Check deserved rank based on time + set enum to this rank
        if (passedTime < sRankTime) { currentRank = Ranks.S; }
        else if (passedTime < aRankTime) { currentRank = Ranks.A; }
        else if (passedTime < bRankTime) { currentRank = Ranks.B; }
        else if (passedTime > bRankTime) { currentRank = Ranks.C; }
    }

    void TimeSlider()
    {
        if (passedTime <= sRankTime)
        {
            timeBar.maxValue = sRankTime;
            timeBar.value = passedTime;
        }
        else if (passedTime <= aRankTime)
        {
            timeBar.maxValue = aRankTime - sRankTime;
            timeBar.value = passedTime - sRankTime;
        }
        else if (passedTime <= bRankTime)
        {
            timeBar.maxValue = bRankTime - aRankTime;
            timeBar.value = passedTime - aRankTime;
        }
        else if (passedTime > bRankTime)
        {
            timeBar.value = 0;
        }
    }

    IEnumerator ShowRank()
    {

        yield return new WaitForSeconds(3);
    }
}
