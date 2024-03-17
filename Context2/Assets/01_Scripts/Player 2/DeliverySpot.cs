using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySpot : MonoBehaviour
{
    [SerializeField] GameObject indicator;

    public DeliveryManager DeliveryManager;

    bool delivered;

    [Space]
    [Header("Timer")]
    public float passedTime;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseTimer();
    }

    void PlayerCollision()
    {
        delivered = true;
        DeliveryManager.Delivered();
        indicator.SetActive(false);
    }

    void IncreaseTimer()
    {
        passedTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !delivered)
        {
            PlayerCollision();
        }
    }
}
