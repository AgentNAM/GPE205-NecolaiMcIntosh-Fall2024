using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameFSM : MonoBehaviour
{
    public KeyCode ActivateTitleScreen;
    public KeyCode ActivateMainMenu;
    public KeyCode ActivateOptions;
    public KeyCode ActivateCredits;
    public KeyCode ActivateGameplay;
    public KeyCode ActivateGameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ActivateTitleScreen))
        {
            GameManager.instance.ActivateTitleScreen();
        }

        if (Input.GetKeyDown(ActivateMainMenu))
        {
            GameManager.instance.ActivateMainMenu();
        }

        if (Input.GetKeyDown(ActivateOptions))
        {
            GameManager.instance.ActivateOptionsMenu();
        }

        if (Input.GetKeyDown(ActivateCredits))
        {
            GameManager.instance.ActivateCredits();
        }

        if (Input.GetKeyDown(ActivateGameplay))
        {
            GameManager.instance.ActivateGameplay();
        }

        if (Input.GetKeyDown(ActivateGameOver))
        {
            GameManager.instance.ActivateGameOver();
        }
    }
}
