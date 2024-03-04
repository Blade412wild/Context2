using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        object instance = System.Activator.CreateInstance(typeof(ExampleClass));
         

        ExampleClass b = new ExampleClass();
        foreach (MethodInfo a in typeof(ExampleClass).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            if (a.GetParameters().Length == 0)
            {
                Debug.Log(a.Name);
                string secret = (string)a.Invoke(b, null);
                Debug.Log(secret);
                

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
