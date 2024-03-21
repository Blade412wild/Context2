using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEventTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOnCube()
    {
        Debug.Log(" set object on");
        gameObject.SetActive(true);
    }

    public void TurnDownCube()
    {
        Debug.Log(" set object off");

        gameObject.SetActive(false);

    }


}
