using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleClickToRandomMap : MonoBehaviour
{
    Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();

        /*
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateRandomMap();
        }
        */
        toggle.interactable = true;
    }

    public void ActivateRandomMap()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle.isOn)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ActivateRandomMap();
                toggle.interactable = false;
            }
        }
    }
}
