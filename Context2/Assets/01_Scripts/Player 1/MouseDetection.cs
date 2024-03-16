using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    public static event Action<Animator> OnMouseHoveringEnter;
    public static event Action OnMouseHoveringExit;


    private Vector3 previousMousePos;
    private int activationCall = 1;
    private int deActivationCall = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        CheckIfMouseClickIsEnemy();
    }
    private void CheckIfMouseClickIsEnemy()
    {
        Vector3 currentMousePos = Input.mousePosition;

        if (previousMousePos == null)
        {
            previousMousePos = currentMousePos;
        }

        if (previousMousePos == currentMousePos) return;

        Ray ray = Camera.main.ScreenPointToRay(currentMousePos);


        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Animator animator = hitInfo.collider.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                //Debug.Log("name hitobject: " + hitInfo.transform.name);

                if (activationCall == 1) // checks if this is the first event call 
                {
                    OnMouseHoveringEnter?.Invoke(animator);
                    activationCall = 0; // makes sure that the event won't be activated multiple times
                    deActivationCall = 1;  //makes sure that the hoverExit event can be called 
                }


            }
            else
            {
                if (deActivationCall == 1)
                {
                    OnMouseHoveringExit?.Invoke();
                    activationCall = 1; // makes sure that the hoverEnter event can be called 
                    deActivationCall = 0; // makes sure that the event won't be activated multiple times
                }

            }
        }

        previousMousePos = currentMousePos;
    }
}
