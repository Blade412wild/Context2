using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using TMPro;

public class NetworkingUIManager : MonoBehaviour
{
    [Header("Network")]
    [SerializeField] private NetworkManager networkManager;
    private UnityTransport networkTransport;
    private string[] lettersArray = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };

    [Header("UI Buttons")]
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    [Header("UI Overige")]
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private GameObject ClientMenu;



    private string playerInput;
    private bool permissionToLink;



    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            ClickedOnHostButton();
        });

        clientButton.onClick.AddListener(() =>
        {
            ClickedOnClientButton();
        });
    }
    private void Start()
    {

    }

    private void ClickedOnHostButton()
    {
        string ip = GetLocalIPAddress();
        Debug.Log(" Ip Adress : " + ip);

        SetIpAdress(ip);

        List<int> digitsList = GetDigits(ip);
        string encodedNumber = EncodeDigits(digitsList);
        Debug.Log(" Ecoded Ip : " + encodedNumber);


        NetworkManager.Singleton.StartHost();
        TurnOffMenuUI();

    }

    private void ClickedOnClientButton()
    {
        ClientMenu.SetActive(true);
        // wait for button press event "Submit button" 
    }

    private void LinkWithHost(string _encodedNumber)
    {
        List<string> letterList = GetLetters(_encodedNumber);
        string decodedNumber = DecodeDigits(letterList);
        Debug.Log(" Ip Decoded Ip : " + decodedNumber);


        SetIpAdress(decodedNumber);
        NetworkManager.Singleton.StartClient();
        TurnOffMenuUI();
    }

    private void SetIpAdress(string _ip)
    {
        networkTransport = networkManager.GetComponent<UnityTransport>();


        Debug.Log("current network adress " + networkTransport.ConnectionData.Address);

        networkTransport.ConnectionData.Address = _ip;

        Debug.Log("current network adress " + networkTransport.ConnectionData.Address);
    }


    private List<int> GetDigits(string _ip)
    {
        string[] parts = _ip.Split(".");
        string numberString = "";
        List<int> digitList = new List<int>();

        foreach (string part in parts)
        {
            numberString += part;
        }

        for (int i = 0; i < numberString.Length; i++)
        {
            int digit = int.Parse(numberString[i].ToString());
            digitList.Add(digit);
        }
        return digitList;

    }
    private string EncodeDigits(List<int> _digitList)
    {
        List<string> encodedListDigits = new List<string>();
        string encodedNumber = "";
        foreach (int digit in _digitList)
        {
            string ecodedDigit = lettersArray[digit - 1];
            encodedNumber += ecodedDigit;
            encodedListDigits.Add(ecodedDigit);
        }
        return encodedNumber;

    }
    private string DecodeDigits(List<string> _letterList)
    {
        List<string> decodedListDigits = new List<string>();
        string decodedNumber = "";
        int counter = 0;

        for (int i = 0; i < _letterList.Count; i++)
        {
            if (counter == 3)
            {
                decodedNumber += ".";
                counter = 0;
            }
            string letter = _letterList[i];
            int digit = (Array.IndexOf(lettersArray, letter) + 1);
            decodedNumber += digit;

            counter++;
        }
        return decodedNumber;
    }

    private List<string> GetLetters(string _encodedIp)
    {
        List<string> encodedListDigits = new List<string>();

        for (int i = 0; i < _encodedIp.Length; i++)
        {
            char character = _encodedIp[i];
            string letter = character.ToString();

            encodedListDigits.Add(letter);
        }
        return encodedListDigits;
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

    public void ReadPlayerInput()
    {
        string playerInput = inputfield.text;
        playerInput.ToLower();
        LinkWithHost(playerInput);


        //        client needs to get a conformation that it is linked
        //playerInput = inputfield.text;
    }

    private void TurnOffMenuUI()
    {
        this.gameObject.SetActive(false);
    }
}
