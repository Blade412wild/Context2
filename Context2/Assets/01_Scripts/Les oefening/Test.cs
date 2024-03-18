using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Test : MonoBehaviour
{
    public GameEvent gameEvent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(" try to play event");
            gameEvent?.Invoke();
        }
    }

    public void EventsTest()
    {
        Debug.Log("event plays");
    }
}
