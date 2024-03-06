using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float steeringSpeed = 10;
    [SerializeField] float gravityForce = 10;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] Transform steeringPoint;

    [SerializeField] float maxVelocity = 10;
    [SerializeField] float velocityLossSpeed = 1;

    [SerializeField] Transform crashCollider;

    [SerializeField] KeyCode gasInput = KeyCode.Mouse0;
    [SerializeField] KeyCode reverseInput = KeyCode.Mouse1;

    public float currentVelocity;
    public bool isGassing;
    public bool isReversing;

    Vector3 velocity;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SimulateGravity();
        ApplyMovement();
        LoseVelocity();
        Steering();
        CrashCheck();

        if (Input.GetKey(gasInput))
        {
            Gas();
            isGassing = true;
        }
        else
        {
            isGassing = false;
        }
        if (Input.GetKey(reverseInput))
        {
            Reverse();
            isReversing = true;
        }
        else
        {
            isReversing = false;
        }
    }

    void Gas()
    {
        currentVelocity += Time.deltaTime * moveSpeed;
    }

    void Reverse()
    {
        if (currentVelocity > 0)
        {
            currentVelocity -= Time.deltaTime * moveSpeed * 2;
        }
        else
        {
            currentVelocity -= Time.deltaTime * moveSpeed;
        }
    }

    void Steering()
    {
        //transform.Rotate(transform.rotation.x, steeringSpeed * Input.GetAxis("Horizontal"), transform.rotation.z);
        transform.RotateAround(steeringPoint.position, Vector3.up, steeringSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }

    void ApplyMovement()
    {
        if (currentVelocity >= maxVelocity)
        {
            currentVelocity = maxVelocity;
        }
        if (currentVelocity <= -maxVelocity)
        {
            currentVelocity = -maxVelocity;
        }
        Vector3 moveDirection = transform.TransformDirection(Vector3.forward) * currentVelocity * Time.deltaTime;
        controller.Move(moveDirection);
    }

    void LoseVelocity()
    {
        if (!isGassing && !isReversing)
        {
            if (currentVelocity > 0)
            {
                currentVelocity -= Time.deltaTime * velocityLossSpeed;
            }
            else if (currentVelocity < 0)
            {
                currentVelocity += Time.deltaTime * velocityLossSpeed;
            }
        }
    }

    void CrashCheck()
    {
        if (Physics.CheckSphere(crashCollider.position, 0.4f, ~playerLayer))
        {
            Crash();
        }
    }

    void Crash()
    {
        if (currentVelocity > 0)
        {
            currentVelocity = 0;
        }
    }

    void SimulateGravity()
    {
        velocity.y += -gravityForce * Time.deltaTime;
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, ~playerLayer);
    }
}
