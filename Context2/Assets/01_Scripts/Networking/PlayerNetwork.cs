using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    public static event Action<PlayerNetwork> OnConnected;

    [SerializeField] private GameEvent clientIsLinked;
    [SerializeField] private EventList eventListScripableObject;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        OnConnected?.Invoke(this);
        clientIsLinked?.Invoke();

        if (OwnerClientId == 1)
        {
            gameObject.AddComponent<Client>();
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.B) && OwnerClientId == 1)
        {
            //PrepareEventpackage(gameEvent);
        }
    }

    public void PrepareEventpackage(int _index)
    {
        Debug.Log(" prepare Event Invoked : " + eventListScripableObject.gameEvents[_index].name);
        TestServerRpc(_index);
    }

    [ServerRpc]
    private void TestServerRpc(int _index)
    {

        Debug.Log("Rpc Event Invoked : " + eventListScripableObject.gameEvents[_index].name);
        eventListScripableObject.gameEvents[_index]?.Invoke();
        //_dataEventPackage._gameEvent?.Invoke();

    }
}
