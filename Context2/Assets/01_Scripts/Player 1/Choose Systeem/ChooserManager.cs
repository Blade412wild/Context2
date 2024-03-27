using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChooserManager : MonoBehaviour
{
    public static event Action OnPlayAnimation;
    public static event Action<GameEvent> OnSendEvent;

    private int choiceCounter = 0;
    private int previousChoiceCounter;

    [SerializeField] private Choices[] AllChoices;

    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI ChoiceText;
    [SerializeField] private TextMeshProUGUI TimerText;

    [Header("Timers")]
    [SerializeField] private CustomTimer yesAnimationTimer;
    [SerializeField] private CustomTimer noAnimationTimer;
    [SerializeField] private CustomTimer dayTimer;
    [SerializeField] private CustomTimer beginWaitTime;


    [Header("GameObjects")]
    [SerializeField] private GameObject textUi;

    [Header("Scritps")]
    [SerializeField] private StapelBehaviour stapelBehaviour;

    private Dictionary<ImpactChoices, GameEvent> currentChoiceDict;

    // Start is called before the first frame update
    void Start()
    {
        textUi.SetActive(false);

        ChoiceObject.OnChoiceMade += NextChoice;
        Timer.OnTimerIsDone += UpdateUI;

        stapelBehaviour.StapelSetup(AllChoices.Length);

        currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
        dayTimer.CreateTimer();
        beginWaitTime.CreateTimer();
        //beginWaitTime.timerInstance.OnTimerIsDonePublic += UpdateUI;
    }

    private void Update()
    {
        TimerText.text = "Time before shitft ends : " + dayTimer.ShowTime();
    }

    public void NextChoice(ChoiceObject.ChoiceImpact _madeChoice)
    {
        stapelBehaviour.NextSprite();
        PlayChoiceConsequenceEvent(_madeChoice);
        WaitForAnimation(_madeChoice);
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
            Debug.Log("textUi : " + textUi);
            textUi.SetActive(true);
        }
    }

    private void WaitForAnimation(ChoiceObject.ChoiceImpact _madeChoice)
    {
        OnPlayAnimation?.Invoke();
        textUi.SetActive(false);
        if (_madeChoice == ChoiceObject.ChoiceImpact.Yes)
        {
            yesAnimationTimer.CreateTimer();
        }
        else
        {
            noAnimationTimer.CreateTimer();
        }
    }

    private void PlayChoiceConsequenceEvent(ChoiceObject.ChoiceImpact _madeChoice)
    {
        if (_madeChoice == ChoiceObject.ChoiceImpact.Yes)
        {
            GameEvent choiceEvent = currentChoiceDict[ImpactChoices.Yes];

            if (choiceEvent != null)
            {
                Debug.Log(" Answer: " + _madeChoice + ", play " + choiceEvent.name + " event from choice : " + AllChoices[choiceCounter].name);
                OnSendEvent?.Invoke(choiceEvent);
            }
        }
        else
        {

            GameEvent choiceEvent = currentChoiceDict[ImpactChoices.No];

            if (choiceEvent != null)
            {
                OnSendEvent?.Invoke(choiceEvent);
                Debug.Log(" Answer: " + _madeChoice + ", play " + choiceEvent.name + " event from choice : " + AllChoices[choiceCounter].name);
            }
        }
    }





}
