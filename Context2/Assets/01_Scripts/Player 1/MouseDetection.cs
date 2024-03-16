using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    // events
    public static event Action<Animator> OnMouseHoveringEnter;
    public static event Action OnMouseHoveringExit;
    public event Action OnMouseClick;

    //animation Variables
    private Vector3 previousMousePos;
    private int activationCall = 1;
    private int deActivationCall = 0;

    // choice Variables
    private bool hovering = false;
    private ChoiceObject choiceObject = null;

    private void Start()
    {
        OnMouseClick += CheckMouseClick;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("  mouse clicked");
            CheckMouseClick();
        }
    }
    private void FixedUpdate()
    {
        Vector3 currentMousePos = Input.mousePosition;
        if (CheckMouseMoved(currentMousePos) == true)
        {
            ShootRay(currentMousePos);
        }

        previousMousePos = currentMousePos;
    }

    private bool CheckMouseMoved(Vector3 _currentMousePos)
    {
        if (previousMousePos == null)
        {
            previousMousePos = _currentMousePos;
        }

        if (previousMousePos == _currentMousePos)
        {
            return false;
        }

        return true;
    }
    private void ShootRay(Vector3 _currentMousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(_currentMousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            CheckHitInfo(hitInfo);
        }
    }
    private void CheckHitInfo(RaycastHit _hitInfo)
    {
        Animator animator = _hitInfo.collider.gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            choiceObject = _hitInfo.collider.gameObject.GetComponent<ChoiceObject>();
            hovering = true;
            if (activationCall == 1) // checks if this is the first event call 
            {
                OnMouseHoveringEnter?.Invoke(animator);
                activationCall = 0; // makes sure that the event won't be activated multiple times
                deActivationCall = 1;  //makes sure that the hoverExit event can be called 
            }


        }
        else
        {
            hovering = false;

            choiceObject = null;
            if (deActivationCall == 1)
            {
                OnMouseHoveringExit?.Invoke();
                activationCall = 1; // makes sure that the hoverEnter event can be called 
                deActivationCall = 0; // makes sure that the event won't be activated multiple times
            }

        }
    }
    private void CheckMouseClick()
    {
        Debug.Log("Hovering : " + hovering + " | choiceObject : " + choiceObject);
        if (hovering == true && choiceObject != null)
        {
            Debug.Log(" Check mouse click");
        }
    }
}
