using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToCredits : MonoBehaviour
{
    public void ChangeToCredits()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCredits();
        }
    }
}
