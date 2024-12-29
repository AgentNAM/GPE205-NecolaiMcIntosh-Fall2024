using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangedToMusicVolume : MonoBehaviour
{
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        // Debug.Log("Music volume active");
        // SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        if (GameManager.instance != null)
        {
            if (slider != null)
            {
                // Debug.Log("Music volume: " + slider.value);
                GameManager.instance.SetMusicVolume();
            }
        }
    }
}
