using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ChoiceObject : MonoBehaviour
{
    public event Action<ChoiceImpact> OnChoiceMade;
    public enum ChoiceImpact {Yes, No};

    public ChoiceImpact choice;

    //[SerializeField] private bool ChoiceImpact;

    private void Start()
    {
        
    }

    private void MadeChoice()
    {
        OnChoiceMade?.Invoke(choice);
    }


}
