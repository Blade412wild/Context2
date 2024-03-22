using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject cameraScooterPrefab;
    [SerializeField] GameObject cameraChooserPrefab;


    // Start is called before the first frame update
    void Start()
    {
        PlayerNetwork.OnConnected += SwitchCameras2;
        MainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //cameraScooter.GetComponent<Camera>().enabled = true;
            //cameraChooser.GetComponent<Camera>().enabled = false;

        }
    }

    private void SwitchCameras(PlayerNetwork _playerNetwork)
    {
        Debug.Log(" player " + _playerNetwork.OwnerClientId);
        //if (_playerNetwork.OwnerClientId == 0)
        //{
        //    Debug.Log("Enable Scooter");
        //    //Debug.Log(" cameraChooser " + cameraChooser);
        //   // Debug.Log(" cameraScooter " + cameraScooter);
        //    //cameraScooter.GetComponent<Camera>().enabled = true;
        //    //cameraChooser.GetComponent<Camera>().enabled = false;
        //    //MainCamera.GetComponent<Camera>().enabled = false;

        //}

        //if (_playerNetwork.OwnerClientId == 1)
        //{
        //    Debug.Log("Enable Chooser");
        //    Debug.Log(" cameraChooser " + cameraChooser);
        //    Debug.Log(" cameraScooter " + cameraScooter);
        //    cameraScooter.GetComponent<Camera>().enabled = false;
        //    cameraChooser.GetComponent<Camera>().enabled = true;
        //    MainCamera.GetComponent<Camera>().enabled = false;

        //}
    }

    private void SwitchCameras2(PlayerNetwork _playerNetwork)
    {
        Debug.Log(" player " + _playerNetwork.OwnerClientId);
        if (_playerNetwork.OwnerClientId == 0)
        {
            GameObject cameraScooter = Instantiate(cameraScooterPrefab);

        }

        if (_playerNetwork.OwnerClientId == 1)
        {
            GameObject cameraChooser = Instantiate(cameraChooserPrefab);
        }
    }

    public void TurnOnChooser()
    {
        //Debug.Log("Enable Chooser");
        //Debug.Log(" cameraChooser " + cameraChooser);
        //Debug.Log(" cameraScooter " + cameraScooter);
        //cameraScooter.GetComponent<Camera>().enabled = false;
        //cameraChooser.GetComponent<Camera>().enabled = true;
        //MainCamera.GetComponent<Camera>().enabled = false;
    }

    public void TurnOnScooter()
    {
        //Debug.Log("Enable Scooter");
        //Debug.Log(" cameraChooser " + cameraChooser);
        //Debug.Log(" cameraScooter " + cameraScooter);
        //cameraScooter.GetComponent<Camera>().enabled = true;
        //cameraChooser.GetComponent<Camera>().enabled = false;
        //MainCamera.GetComponent<Camera>().enabled = false;
    }


}
