using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public GameEvent gameEvent;
    private int amount = 1;
    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestServerRpc();

        }

        Vector3 moveDir = new Vector3(0,0,0);

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
        gameEvent.Invoke();
    }
}
