using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chooser : MonoBehaviour
{
    int choiceCounter = 0;

    [SerializeField] private Choices[] AllChoices;

    [Header(" UI COMPONETS")]
    [SerializeField] private TextMeshProUGUI ChoiceText;
    [SerializeField] private TextMeshProUGUI Value1;
    [SerializeField] private TextMeshProUGUI Value2;


    // Start is called before the first frame update
    void Start()
    {
        Dictionary<ImpactChoices, int> choiceValues = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
        UpdateUI(choiceValues);
    }

    public void NextChoice()
    {
        if (choiceCounter < AllChoices.Length - 1)
        {
            choiceCounter++;
            Dictionary<ImpactChoices, int> choiceValues = AllChoices[choiceCounter].ChoiceImpactDict.ToDictionary();
            UpdateUI(choiceValues);
        }
        else
        {
            Debug.Log("no more choices to make");
        }
    }

    private void UpdateUI(Dictionary<ImpactChoices, int> dict)
    {
        ChoiceText.text = AllChoices[choiceCounter].ChoiceText;
        Value1.text = " Climate : " + dict[ImpactChoices.climate].ToString();
        Value2.text = " Economy : " + dict[ImpactChoices.economy].ToString();

        Debug.Log("Climate : " + dict[ImpactChoices.climate]);
        Debug.Log("Economy : " + dict[ImpactChoices.economy]);

    }

    public void onUIEnter()
    {
        Debug.Log("enterUI");
    }

    public void onUIExit()
    {
        Debug.Log("exitUI");
    }
}
