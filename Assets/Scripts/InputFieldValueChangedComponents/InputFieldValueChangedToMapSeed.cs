using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldValueChangedToMapSeed : MonoBehaviour
{
    TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void SetMapSeed()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetMapSeed();
        }
    }
}
