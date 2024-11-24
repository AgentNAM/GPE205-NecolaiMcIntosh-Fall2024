using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleClickToMapOfDay : MonoBehaviour
{
    Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();

        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMapOfTheDay();
        }
        toggle.interactable = false;
    }

    public void ActivateMapOfDay()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle.isOn)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ActivateMapOfTheDay();
                toggle.interactable = false;
            }
        }
    }
}
