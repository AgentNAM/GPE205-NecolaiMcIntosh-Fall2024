using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToPlay : MonoBehaviour
{
    public void ChangeToGameplay()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameplay();
            GameManager.instance.PlayButtonPressSFX();
        }
    }
}
