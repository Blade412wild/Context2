using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ChoiceObject : MonoBehaviour
{
    public static event Action<ChoiceImpact> OnChoiceMade;
    public enum ChoiceImpact { Yes, No };

    public ChoiceImpact choice;

    //[SerializeField] private bool ChoiceImpact;

    private void Start()
    {
        MouseDetection.OnMouseClick += MadeChoice;
    }

    private void MadeChoice(ChoiceObject _choiceObject)
    {
        if (_choiceObject == this)
        {
            OnChoiceMade?.Invoke(choice);
        }
    }


}
