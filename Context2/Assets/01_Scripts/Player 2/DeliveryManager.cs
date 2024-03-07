using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{
    public int totalDeliveries;
    public int currentDeliveries;

    [SerializeField] TextMeshProUGUI totalDeliveriesText;
    [SerializeField] TextMeshProUGUI currentDeliveriesText;

    // Start is called before the first frame update
    void Start()
    {
        totalDeliveriesText.text = totalDeliveries.ToString();
        currentDeliveriesText.text = currentDeliveries.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTotalDeliveries(int amount)
    {
        totalDeliveries += amount;
        currentDeliveriesText.text = currentDeliveries.ToString();
    }

    public void AddDeliveries(int amount)
    {
        currentDeliveries += amount;
        currentDeliveriesText.text = currentDeliveries.ToString();
    }
}
