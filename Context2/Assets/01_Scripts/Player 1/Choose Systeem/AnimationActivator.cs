using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    private Animator animator;
    private Animator eventAnimator;
    private bool IsHovering;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MouseDetection.OnMouseHoveringEnter += SetAnimationBoolTrue;
        MouseDetection.OnMouseHoveringExit += SetAnimationBoolFalse;

    }
    private void SetAnimationBoolTrue(Animator _animator)
    {
        Debug.Log(" set bool");
        if (animator == _animator)
        {
            eventAnimator = _animator;
            Debug.Log(_animator);
            animator.SetBool("IsHovering", true);
        }
    }

    private void SetAnimationBoolFalse()
    {
        if (eventAnimator == animator)
        {
            eventAnimator.SetBool("IsHovering", false);
        }
    }
}
