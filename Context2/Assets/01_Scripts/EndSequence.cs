using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndSequence : MonoBehaviour
{
    [SerializeField] private GameObject UIpanel;
    private bool mayQuit = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartEndSequence()
    {
        openUI();
        mayQuit = true;
    }

    private void Update()
    {
        if (mayQuit == false) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
    private void openUI()
    {
        UIpanel.SetActive(true);
    }
}
