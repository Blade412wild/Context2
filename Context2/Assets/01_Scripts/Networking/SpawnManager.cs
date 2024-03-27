using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using UnityEngine.Networking;
using Unity.Netcode.Components;

public class SpawnManager : MonoBehaviour
{
    [Header("Network")]
    [SerializeField] private NetworkManager networkManager;
    private UnityTransport networkTransport;

    [Header("Player 1")]
    [SerializeField] private GameObject cameraScooterPrefab;
    [SerializeField] private GameObject SpawnPositionScooter;

    [Header("Player2")]
    [SerializeField] private GameObject cameraChooserPrefab;
    [SerializeField] private Transform SpawnPositionChooser;



    // Start is called before the first frame update
    void Start()
    {
        PlayerNetwork.OnConnected += ChoosePlayer;

        //setIPAdress();

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
        GameObject chooserPackagePrefab = Instantiate(cameraChooserPrefab);
        SpawnPositionChooser = FindObjectOfType<ChoosePos>().transform;
        chooserPackagePrefab.transform.position = SpawnPositionChooser.position;
       // chooserPackagePrefab.transform.rotation = SpawnPositionChooser.transform.rotation;
    }

    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    private void setIPAdress()
    {
        var networkrtest = networkManager.GetComponent<UnityTransport>();
        networkTransport = networkrtest;

        Debug.Log("current network adress " + networkrtest.ConnectionData.Address);

        string ip = GetLocalIPAddress();
        Debug.Log("Ip Adress: " + ip);

        networkrtest.ConnectionData.Address = ip;

        Debug.Log("current network adress " + networkrtest.ConnectionData.Address);
    }
}
