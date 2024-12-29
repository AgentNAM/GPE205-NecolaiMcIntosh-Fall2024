using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToQuit : MonoBehaviour
{
    public void QuitGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.QuitGame();
            GameManager.instance.PlayButtonPressSFX();
        }
    }
}
