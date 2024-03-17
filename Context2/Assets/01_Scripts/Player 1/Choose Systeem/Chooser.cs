using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chooser : MonoBehaviour
{
    int choiceCounter = 0;

    [SerializeField] private Choices[] AllChoices;
    [Header("Scripts")]
    [SerializeField] private Stats player1Stats;


    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI ChoiceText;
    [SerializeField] private TextMeshProUGUI Value1;
    [SerializeField] private TextMeshProUGUI Value2;

    [Header("Timers")]
    [SerializeField] private CustomTimer customTimer;

    private Dictionary<ImpactChoices, int> currentChoiceDict;


    // Start is called before the first frame update
    void Start()
    {
        currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
        UpdateUI();
        Debug.Log(choiceCounter);

    }
    public void onUIEnter()
    {

        CalculateTempValue(currentChoiceDict, true);
    }

    public void onUIExit()
    {
        CalculateTempValue(currentChoiceDict, false);
    }

    public void NextChoice()
    {
        Debug.Log(choiceCounter);

        if (choiceCounter < AllChoices.Length)
        {
            currentChoiceDict = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
            CalculateNewValue(currentChoiceDict, 1);

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
            Value1.text = " Climate : " + player1Stats.Value1.ToString();
            Value2.text = " Economy : " + player1Stats.Value2.ToString();
        }
    }

    private void UpdateTempUI(int value1, int value2)
    {
        Value1.text = " Climate : " + value1.ToString();
        Value2.text = " Economy : " + value2.ToString();
    }

    private void CalculateNewValue(Dictionary<ImpactChoices, int> dict, int scaler)
    {
        player1Stats.Value1 = player1Stats.Value1 + (dict[ImpactChoices.Yes] * scaler);
        player1Stats.Value2 = player1Stats.Value2 + (dict[ImpactChoices.No] * scaler);
    }
    private void CalculateTempValue(Dictionary<ImpactChoices, int> dict, bool scaler)
    {
        int tempValue1 = 0;
        int tempValue2 = 0;
        
        int previousValue1 = player1Stats.Value1;
        int previousValue2 = player1Stats.Value2;



        if (scaler == true)
        {
            tempValue1 = previousValue1 + dict[ImpactChoices.Yes];
            tempValue2 = previousValue2 + dict[ImpactChoices.No];
        }
        else
        {
            tempValue1 = previousValue1;
            tempValue2 = previousValue1;
        }

        UpdateTempUI(tempValue1, tempValue2);
    }

}
