using System;
using System.Collections.Generic;
using UnityEngine;

public enum ImpactChoices {Yes, No};

[CreateAssetMenu(fileName = "ChoiceData", menuName = "ScriptableObjects/Choices", order = 1)]
public class Choices : ScriptableObject
{
    [Header("Text")]
    public string ChoiceText;

    [Header("Choices Impact")]
    public NewDict ChoiceImpactDict = new NewDict();

}


[Serializable]
public class NewDict
{
    [SerializeField]
    private NewDictItem[] thisDictItems;

    public Dictionary<ImpactChoices, GameEvent> ToDictionary()
    {
        Dictionary<ImpactChoices, GameEvent> newDict = new Dictionary<ImpactChoices, GameEvent>();

        foreach(var item in thisDictItems)
        {
            newDict.Add(item.typeImpact, item.Impact);
        }

        return newDict;
    }
}

[Serializable]
public class NewDictItem
{
    [SerializeField]
    public ImpactChoices typeImpact;

    [SerializeField]
    public GameEvent Impact;
}

