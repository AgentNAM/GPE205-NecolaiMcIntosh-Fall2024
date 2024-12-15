using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleClickToRandomMapSeed : MonoBehaviour
{
    Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();

        /*
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateRandomMapSeed();
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

    public void ActivateRandomMapSeed()
    {
        Toggle toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            if (GameManager.instance != null && toggle.isOn)
            {
                GameManager.instance.ActivateRandomMapSeed();
                toggle.interactable = false;
            }
        }
    }
}
