using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToTitle : MonoBehaviour
{
    public void ChangeToTitle()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateTitleScreen();
            GameManager.instance.PlayButtonPressSFX();
        }
    }
}
