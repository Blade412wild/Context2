using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chooser2 : MonoBehaviour
{
    int choiceCounter = 0;

    [SerializeField] private Choices[] AllChoices;

    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI ChoiceText;

    [Header("Timers")]
    [SerializeField] private CustomTimer animationTimer;

    [Header("GameObjects")]
    [SerializeField] private GameObject textUi;


    private Dictionary<ImpactChoices, int> currentChoiceDict;


    // Start is called before the first frame update
    void Start()
    {
        currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
        Debug.Log(choiceCounter);
        WaiForAnimation();
    }

    public void NextChoice()
    {
        Debug.Log(choiceCounter);

        if (choiceCounter < AllChoices.Length)
        {
            currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
            choiceCounter++;
            UpdateUI();
        }
        else
        {
            Debug.Log("no more choices to make");
        }
    }

    private void UpdateUI()
    {
        if (choiceCounter < AllChoices.Length)
        {
            ChoiceText.text = AllChoices[choiceCounter].ChoiceText;
            textUi.SetActive(true);
        }
    }

    private void WaiForAnimation()
    {
        textUi.SetActive(false);
        animationTimer.CreateTimer();
        animationTimer.timerInstance.OnTimerIsDone += UpdateUI;
    }



}
