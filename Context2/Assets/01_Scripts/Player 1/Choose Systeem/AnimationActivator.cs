using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    private Animator ownAnimator;
    private Animator eventAnimator;
    private bool IsHovering;

    // Start is called before the first frame update
    void Start()
    {
        ownAnimator = GetComponent<Animator>();
        MouseDetection.OnMouseHoveringEnter += SetAnimationBoolTrue;
        MouseDetection.OnMouseHoveringExit += SetAnimationBoolFalse;
        ChoiceObject.OnChoiceMade += PlayMainAnimation;
    }




    private void SetAnimationBoolTrue(Animator _animator)
    {
        if (ownAnimator == _animator)
        {
            eventAnimator = _animator;
            ownAnimator.SetBool("IsHovering", true);
        }
    }

    private void SetAnimationBoolFalse()
    {
        if (eventAnimator == ownAnimator)
        {
            eventAnimator.SetBool("IsHovering", false);
        }
    }

    private void PlayMainAnimation(ChoiceObject.ChoiceImpact _choiceMade)
    {
        if (_choiceMade == ChoiceObject.ChoiceImpact.Yes)
        {
            ownAnimator.SetTrigger("Yes");
        }

        if (_choiceMade == ChoiceObject.ChoiceImpact.No)
        {
            ownAnimator.SetTrigger("No");
        }

    }
}
