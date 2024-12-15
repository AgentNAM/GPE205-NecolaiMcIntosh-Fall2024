using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    // Variables for keyboard inputs
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Variable for score text
    public Text scoreText;
    // Variable for lives text
    public Text livesText;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it is tracking our players in a list
            if (GameManager.instance.players != null)
            {
                // Register ourselves with the GameManager
                GameManager.instance.players.Add(this);
            }
        }
        // Run the Start() function from the parent (base) class
        base.Start();

        // Set score and lives text at the start of the game
        UpdateScoreText(score);
        UpdateLivesText(lives);
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
            pawn.noiseMaker.volumeDistance = pawn.movingVolumeDistance;
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
            pawn.noiseMaker.volumeDistance = pawn.movingVolumeDistance;
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
            pawn.noiseMaker.volumeDistance = pawn.movingVolumeDistance;
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
            pawn.noiseMaker.volumeDistance = pawn.movingVolumeDistance;
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }

        if (!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey) && !Input.GetKey(rotateClockwiseKey) && !Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.noiseMaker.volumeDistance = 0;
        }
    }

    public void OnDestroy()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it is tracking our players in a list
            if (GameManager.instance.players != null)
            {
                // Remove ourselves from the GameManager's list
                GameManager.instance.players.Remove(this);
            }
        }
    }

    // Handle respawning
    public override void HandleRespawn()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // Signal to our game manager to respawn our player
            GameManager.instance.RespawnPlayer(this);
        }
    }

    // Function that updates our score
    public override void AddToScore(float scoreAmount)
    {
        base.AddToScore(scoreAmount);
        UpdateScoreText(score);
    }

    public override void RemoveFromScore(float scoreAmount)
    {
        base.RemoveFromScore(scoreAmount);
        UpdateScoreText(score);
    }

    public void UpdateScoreText(float newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    // Function that updates our lives
    public override void AddToLives(int livesAmount)
    {
        base.AddToLives(livesAmount);
        UpdateLivesText(lives);
    }

    public override void RemoveFromLives(int livesAmount)
    {
        base.RemoveFromLives(livesAmount);
        UpdateLivesText(lives);
    }

    public void UpdateLivesText(int newLives)
    {
        livesText.text = "Lives: " + newLives;
    }
}
