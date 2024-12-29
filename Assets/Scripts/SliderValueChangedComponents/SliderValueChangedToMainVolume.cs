using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChangedToMainVolume : MonoBehaviour
{
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        // Debug.Log("Main volume active");
        // SetMainVolume();
    }

    public void SetMainVolume()
    {
        if (GameManager.instance != null)
        {
            if (slider != null)
            {
                // Debug.Log("Main volume: " + slider.value);
                GameManager.instance.SetMainVolume();
            }
        }
    }
}
