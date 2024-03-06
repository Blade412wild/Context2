using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public int totalDeliveries;

    public List<DeliverySpot> spots;

    // Start is called before the first frame update
    void Start()
    {
        spots = new List<DeliverySpot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
