using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] GameObject[] wheels;
    [SerializeField] float rotationSpeed;
    ScooterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ScooterController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed * controller.currentVelocity);
        }
    }
}
