using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySpot : MonoBehaviour
{
    [SerializeField] GameObject indicator;

    DeliveryManager deliveryManager;

    bool delivered;

    // Start is called before the first frame update
    void Awake()
    {
        deliveryManager = FindObjectOfType<DeliveryManager>();

        deliveryManager.ChangeTotalDeliveries(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerCollision()
    {
        delivered = true;
        deliveryManager.AddDeliveries(1);
        indicator.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !delivered)
        {
            PlayerCollision();
        }
    }
}
