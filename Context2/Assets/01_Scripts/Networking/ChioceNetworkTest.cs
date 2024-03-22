using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChioceNetworkTest : MonoBehaviour
{
    [SerializeField] private GameEvent TurnOnRed;
    [SerializeField] private GameEvent TurnOffRed;
    [SerializeField] private GameEvent TurnOnGreen;
    [SerializeField] private GameEvent TurnOnYellow;

    private GameEvent _gameEvent;
    private NetworkBehaviour networkBehaviour;

    //public ChioceNetworkTest(NetworkBehaviour _networkBehaviour)
    //{
    //    Debug.Log("Chooser script");
    //    networkBehaviour = _networkBehaviour;
    //}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hallo");
        if (networkBehaviour.IsOwner) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");

            _gameEvent = TurnOnRed;
            EventServerRpc();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T");

            _gameEvent = TurnOffRed;
            EventServerRpc();

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y");

            _gameEvent = TurnOnGreen;
            EventServerRpc();

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U");

            _gameEvent = TurnOnYellow;
            EventServerRpc();

        }
    }

    [ServerRpc]
    private void EventServerRpc()
    {
        Debug.Log("GameEvent : " + _gameEvent);
        _gameEvent?.Invoke();
    }
}
