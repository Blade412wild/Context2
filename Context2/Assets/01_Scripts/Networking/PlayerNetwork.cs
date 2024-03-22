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
        ChooserManager.OnSendEvent += PrepareEventpackage;
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.B) && OwnerClientId == 1)
        {
            PrepareEventpackage(gameEvent);
        }
    }

    private void PrepareEventpackage(GameEvent _gameEvent)
    {
        gameEvent = _gameEvent;
        TestServerRpc();
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("Test Event : " + gameEvent.name);
        //_dataEventPackage._gameEvent?.Invoke();
        gameEvent?.Invoke();
    }
}
