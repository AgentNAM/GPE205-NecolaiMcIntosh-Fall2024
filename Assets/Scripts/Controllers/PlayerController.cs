using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

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
}
