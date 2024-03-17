using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] float cRankTime = 75;
    [SerializeField] TextMeshProUGUI timerText;

    [Space]
    [SerializeField] TextMeshProUGUI deliveriesMadeText;
    [SerializeField] TextMeshProUGUI rankInfoText;

    // Start is called before the first frame update
    void Start()
    {
        deliveriesMadeText.text = "Deliveries made: " + deliveriesMade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTime();

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

    }

    public void Delivered()
    {

        Debug.Log("Rank " + currentRank + "! ~ Delievered in " + passedTime + " seconds.");

        // Reset time
        passedTime = 0;

        // Increase delivery counter
        deliveriesMade++;
        deliveriesMadeText.text = "Deliveries made: " + deliveriesMade.ToString();
    }

    void IncreaseTime()
    {
        passedTime += Time.deltaTime;

        // Check deserved rank based on time + set enum to this rank
        if (passedTime < sRankTime) { currentRank = Ranks.S; }
        else if (passedTime < aRankTime) { currentRank = Ranks.A; }
        else if (passedTime < bRankTime) { currentRank = Ranks.B; }
        else if (passedTime < cRankTime) { currentRank = Ranks.C; }
    }

    IEnumerator ShowRank()
    {

        yield return new WaitForSeconds(3);
    }
}
