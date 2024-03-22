using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<PlayerNetwork> OnConnected;

    public GameEvent gameEvent;

    public GameEvent Chooser;
    public GameEvent Scooter;

    private int amount = 1;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        OnConnected?.Invoke(this);
    }

    //private void Start()
    //{
    //    if (!IsOwner) return;
    //    OnConnected?.Invoke(this);
    //}
    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestServerRpc();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            TestClientRpc();
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;


        float moveSpeed = 3f;
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("TestServerRpc : " + OwnerClientId);
        Scooter.Invoke();
    }

    [ClientRpc]
    private void TestClientRpc()
    {
        Debug.Log("TestServerRpc : " + OwnerClientId);
        Chooser.Invoke();
    } 
}
