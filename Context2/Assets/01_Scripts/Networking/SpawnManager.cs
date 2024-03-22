using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private GameObject cameraScooterPrefab;
    [SerializeField] private GameObject SpawnPositionScooter;

    [Header("Player2")]
    [SerializeField] private GameObject cameraChooserPrefab;
    [SerializeField] private GameObject SpawnPositionChooser;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNetwork.OnConnected += ChoosePlayer;
    }

    private void ChoosePlayer(PlayerNetwork _playerNetwork)
    {
        Debug.Log(" player " + _playerNetwork.OwnerClientId);
        if (_playerNetwork.OwnerClientId == 0)
        {
            SpawnPlayerScooter();
        }

        if (_playerNetwork.OwnerClientId == 1)
        {
            SpawnPlayerChooser();
        }
    }
    private void SpawnPlayerScooter()
    {
        GameObject ScooterPackage = Instantiate(cameraScooterPrefab);
        ScooterPackage.transform.position = SpawnPositionScooter.transform.position;
    }

    private void SpawnPlayerChooser()
    {
        GameObject cameraChooser = Instantiate(cameraChooserPrefab);
        cameraChooser.transform.position = SpawnPositionChooser.transform.position;
        cameraChooser.transform.rotation = SpawnPositionChooser.transform.rotation;
    }
}
