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

        /*
        if (toggle != null)
        {
            toggle.interactable = true;
        }
        */
        /*
        if (toggle != null)
        {
            toggle.isOn = true;
        }
        */
    }

    public void ActivateRandomMap()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            if (GameManager.instance != null && toggle.isOn)
            {
                GameManager.instance.ActivateRandomMap();
                GameManager.instance.PlayToggleClickSFX();
                toggle.interactable = false;
            }
        }
    }
}
