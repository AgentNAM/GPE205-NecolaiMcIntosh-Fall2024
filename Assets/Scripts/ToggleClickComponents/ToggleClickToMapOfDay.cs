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

        /*
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMapOfTheDay();
        }
        */
        toggle.interactable = false;
    }

    public void ActivateMapOfDay()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            if (GameManager.instance != null && toggle.isOn)
            {
                GameManager.instance.ActivateMapOfTheDay();
                GameManager.instance.PlayToggleClickSFX();
                toggle.interactable = false;
            }
        }
    }
}
