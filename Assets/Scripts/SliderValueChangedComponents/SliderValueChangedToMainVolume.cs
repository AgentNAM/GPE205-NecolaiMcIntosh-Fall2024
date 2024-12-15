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
        GameManager.instance.SetMainVolume();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMainVolume()
    {
        if (GameManager.instance != null)
        {
            if (slider != null)
            {
                // Debug.Log(slider.value);
                GameManager.instance.SetMainVolume();
            }
        }
    }
}
