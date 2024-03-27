using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private PlayerNetwork playerNetwork;
    // Start is called before the first frame update
    void Start()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
    }
    
    public PlayerNetwork GetClientPlayerNetwork()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
        return playerNetwork;
    }
}
