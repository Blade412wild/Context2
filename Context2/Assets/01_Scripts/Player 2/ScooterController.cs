using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScooterController : MonoBehaviour, IPlayerTracer
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float steeringSpeed = 10;
    [SerializeField] float driftingMultiplier = 1.5f;
    [SerializeField] float driftingVelocityLoss = 5;
    [SerializeField] float gravityForce = 10;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] Transform steeringPoint;

    [SerializeField] float maxVelocity = 10;
    [SerializeField] float velocityLossSpeed = 1;

    [SerializeField] Transform crashCollider;
    [SerializeField] ParticleSystem crashParticlesPrefab;

    [Header("Input")]
    [SerializeField] KeyCode gasInput = KeyCode.W;
    [SerializeField] KeyCode reverseInput = KeyCode.S;
    [SerializeField] KeyCode gasInputB = KeyCode.Space;
    [SerializeField] KeyCode reverseInputB = KeyCode.LeftShift;

    [Header("NavTrash")]
    private NavMeshAgent agent;

    [Header("RunTimeValues")]
    public float currentVelocity;
    public float steeringDirection;
    public bool isGassing;
    public bool isReversing;
    public bool isDrifting;
    bool crashed;

    float appliedSteeringSpeed;

    Vector3 velocity;

    CharacterController controller;

    #region IPlayerTracer
    GameObject IPlayerTracer.GetGameObject => gameObject;
    NavMeshAgent IPlayerTracer.GetNavAgent => agent;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();

        Cursor.lockState = CursorLockMode.Locked;

        appliedSteeringSpeed = steeringSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        SimulateGravity();
        ApplyMovement();
        LoseVelocity();
        Steering();
        CrashCheck();

        if (Input.GetKey(reverseInput) || Input.GetKey(reverseInputB))
        {
            Reverse();
            isReversing = true;
        }
        else
        {
            isReversing = false;
        }

        if (Input.GetKey(gasInput) || Input.GetKey(gasInputB))
        {
            Gas();
            isGassing = true;
        }
        else
        {
            isGassing = false;
            isDrifting = false;
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
        steeringDirection = Input.GetAxis("Horizontal");

        //transform.Rotate(transform.rotation.x, steeringSpeed * Input.GetAxis("Horizontal"), transform.rotation.z);
        transform.RotateAround(steeringPoint.position, Vector3.up, appliedSteeringSpeed * steeringDirection * Time.deltaTime);
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
        if (Physics.CheckSphere(crashCollider.position, 0.4f, groundLayer))
        {
            Crash();
        }
        else
        {
            crashed = false;
        }
    }

    void Crash()
    {
        if (currentVelocity > 0)
        {
            currentVelocity = 0;
            if (!crashed)
            {
                Instantiate(crashParticlesPrefab, crashCollider.position, Quaternion.identity);
            }
            crashed = true;
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
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        Gizmos.DrawSphere(crashCollider.position, 0.4f);
    }
}
