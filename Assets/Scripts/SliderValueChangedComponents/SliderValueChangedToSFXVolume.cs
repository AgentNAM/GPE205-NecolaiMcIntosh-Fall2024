using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangedToSFXVolume : MonoBehaviour
{
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        // Debug.Log("SFX volume active");
        // SetSFXVolume();
    }

    public void SetSFXVolume()
    {
        if (GameManager.instance != null)
        {
            if (slider != null)
            {
                // Debug.Log("SFX volume: " + slider.value);
                GameManager.instance.SetSFXVolume();
            }
        }
    }
}
