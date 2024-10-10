using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a; //declared variable a

        a = 42; // assigning value 42 to variable a

        int b = 43; // declaring and assigning a value to variable b



        char c = '4';

        double d = char.GetNumericValue(c);

        Debug.Log(a);
        Debug.Log(b);
        Debug.Log(c);
        Debug.Log(d);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
