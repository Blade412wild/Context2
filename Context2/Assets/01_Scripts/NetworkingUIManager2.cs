using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkingUIManager2 : MonoBehaviour
{
    [Header("Network")]
    [SerializeField] private NetworkManager networkManager;
    private UnityTransport networkTransport;
    private string[] lettersArray = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
    private char[] charArray = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };



    [Header("Overige")]
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private TMP_Text StatusTekst;
    [SerializeField] private GameObject ClientMenu;
    [SerializeField] private CustomTimer timer;

    private bool playerIsLinked = false;
    private int firstDotCount;
    private int secondDotCount;
    private int thirdDotCount;
    private int fourthDotCount = 6;

    private void Start()
    {
        if (networkManager != null) return;
        networkManager = FindAnyObjectByType<NetworkManager>();
    }
    public void ClickedOnHostButton()
    {
        string ip = GetLocalIPAddress();
        Debug.Log(" Ip Adress : " + ip);

        SetIpAdress(ip);

        List<int> digitsList = GetDigits(ip);
        string encodedNumber = EncodeDigits(digitsList);
        Debug.Log(" Ecoded Ip : " + encodedNumber);

        List<char> letterList = GetLetters(encodedNumber);
        string decodedNumber = DecodeDigits(letterList);
        Debug.Log(" Ip Decoded Ip : " + decodedNumber);

        NetworkManager.Singleton.StartHost();
    }

    public void ClickedOnClientButton()
    {
        ClientMenu.SetActive(true);
        StatusTekst.text = "Status : Waiting";
        // wait for button press event "Submit button" 
    }

    private void LinkWithHost(string _encodedNumber)
    {
        List<char> letterList = GetLetters(_encodedNumber);
        string decodedNumber = DecodeDigits(letterList);
        Debug.Log(" Ip Decoded Ip : " + decodedNumber);


        SetIpAdress(decodedNumber);
        NetworkManager.Singleton.StartClient();

        timer.CreateTimer();
        timer.timerInstance.OnTimerIsDonePublic += CheckIfPlayerIsLinked;
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
        int counter = 0;
        foreach (string part in parts)
        {
            if (counter == 0)
            {
                firstDotCount = part.Length;
                //Debug.Log(" first dot L " + firstDotCount);
            }
            if (counter == 1)
            {
                secondDotCount = firstDotCount + part.Length;
                //Debug.Log(" second dot L " + secondDotCount);

            }
            if (counter == 2)
            {
                thirdDotCount = secondDotCount + part.Length;
                //Debug.Log(" third dot L " + thirdDotCount);


            }
            if (counter == 3)
            {
                fourthDotCount = thirdDotCount + part.Length;
                //Debug.Log(" fourth dot L " + fourthDotCount);

            }

            numberString += part;
            counter++;
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

        int counter = 0;
        foreach (int digit in _digitList)
        {
            if (counter == firstDotCount || counter == secondDotCount || counter == thirdDotCount || counter == fourthDotCount)
            {
                encodedNumber += "-";
            }

            string ecodedDigit = lettersArray[digit];
            encodedNumber += ecodedDigit;

            encodedListDigits.Add(ecodedDigit);
            counter++;
        }
        return encodedNumber;

    }
    private string DecodeDigits(List<char> _letterList)
    {
        List<string> decodedListDigits = new List<string>();
        string decodedNumber = "";
        int counter = 0;

        for (int i = 0; i < _letterList.Count; i++)
        {


            char letter = _letterList[i];
            //Debug.Log(" char : " +  letter);
            if (letter == '-')
            {
                decodedNumber += ".";
            }
            else
            {
                int digit = (Array.IndexOf(charArray, letter) + 0);
                decodedNumber += digit;
            }

            counter++;
        }
        return decodedNumber;
    }

    private List<char> GetLetters(string _encodedIp)
    {
        List<char> encodedListDigits = new List<char>();

        for (int i = 0; i < _encodedIp.Length; i++)
        {
            char character = _encodedIp[i];
            string letter = character.ToString();

            encodedListDigits.Add(character);
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
        StatusTekst.text = "Status : Checking";
        string playerInput = inputfield.text;
        playerInput.ToLower();

        LinkWithHost(playerInput);


        //        client needs to get a conformation that it is linked
        //playerInput = inputfield.text;
    }

    public void TurnOffMenuUI()
    {
        Debug.Log(" connected");
        this.gameObject.SetActive(false);
    }



    public void CheckIfPlayerIsLinked()
    {
        if (playerIsLinked == true)
        {
            TurnOffMenuUI();
        }
        else
        {
            StatusTekst.text = "Status : IsNotConnected";
        }
    }
}
