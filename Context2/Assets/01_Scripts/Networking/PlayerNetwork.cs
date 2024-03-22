using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<PlayerNetwork> OnConnected;

    [SerializeField] private GameEvent gameEvent;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        OnConnected?.Invoke(this);

    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.B) && OwnerClientId == 1)
        {
            TestServerRpc();
        }
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("TestServerRpc : " + OwnerClientId);
        gameEvent?.Invoke();
    }

    [ClientRpc]
    private void TestClientRpc()
    {
        Debug.Log("TestServerRpc : " + OwnerClientId);
    } 
}
