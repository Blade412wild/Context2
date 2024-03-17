using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChooserManager : MonoBehaviour
{
    public static event Action OnPlayAnimation;

    private int choiceCounter = 0;
    private int previousChoiceCounter;

    [SerializeField] private Choices[] AllChoices;

    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI ChoiceText;
    [SerializeField] private TextMeshProUGUI TimerText;


    [Header("Timers")]
    [SerializeField] private CustomTimer animationTimer;
    [SerializeField] private CustomTimer dayTimer;


    [Header("GameObjects")]
    [SerializeField] private GameObject textUi;


    private Dictionary<ImpactChoices, int> currentChoiceDict;


    // Start is called before the first frame update
    void Start()
    {
        ChoiceObject.OnChoiceMade += NextChoice;
        Timer.OnTimerIsDone += UpdateUI;
        currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
        UpdateUI();
        Debug.Log(AllChoices[choiceCounter].name);
        dayTimer.CreateTimer();

    }

    private void Update()
    {
        TimerText.text = "Time before shitft ends : " + dayTimer.ShowTime();
    }

    public void NextChoice(ChoiceObject.ChoiceImpact _madeChoice)
    {
        PlayChoiceConsequenceEvent(_madeChoice);
        WaitForAnimation();
        if (choiceCounter < AllChoices.Length - 1)
        {
            currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
            choiceCounter++;
            Debug.Log(AllChoices[choiceCounter].name);
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

    private void WaitForAnimation()
    {
        OnPlayAnimation?.Invoke();
        textUi.SetActive(false);
        animationTimer.CreateTimer();
    }

    private void PlayChoiceConsequenceEvent(ChoiceObject.ChoiceImpact _madeChoice)
    {
        if (_madeChoice == ChoiceObject.ChoiceImpact.Yes)
        {
            Debug.Log(" play " + _madeChoice + " event from choice : " + AllChoices[choiceCounter].name);
        }
        else
        {
            Debug.Log(" play " + _madeChoice + " event from choice : " + AllChoices[choiceCounter].name);
        }
    }





}
