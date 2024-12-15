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
        GameManager.instance.SetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusicVolume()
    {
        if (GameManager.instance != null)
        {
            if (slider != null)
            {
                // Debug.Log(slider.value);
                GameManager.instance.SetMusicVolume();
            }
        }
    }
}
