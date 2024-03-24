using System;
using UnityEngine;

public class ChoiceObject : MonoBehaviour
{
    public static event Action<ChoiceImpact> OnChoiceMade;
    public enum ChoiceImpact { Yes, No };
    public ChoiceImpact choice;

    private Animator animator;

    //[SerializeField] private bool ChoiceImpact;

    private void Start()
    {
        MouseDetection.OnMouseClick += MadeChoice;
        animator = GetComponent<Animator>();
    }

    private void MadeChoice(ChoiceObject _choiceObject)
    {
        if (_choiceObject == this)
        {
            OnChoiceMade?.Invoke(choice);
        }
    }
}
