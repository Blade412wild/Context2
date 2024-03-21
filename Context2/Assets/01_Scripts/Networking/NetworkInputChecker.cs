using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInputChecker : MonoBehaviour
{
    public GameEvent gameEventOn;
    public GameEvent gameEventOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(" play event on");
            gameEventOn?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(" play event off");
            gameEventOff?.Invoke();
        }
    }
}
