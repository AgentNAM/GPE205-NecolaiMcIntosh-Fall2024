using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveBackward()
    {
        Debug.Log("Moving backwards");
    }

    public override void MoveForward()
    {
        Debug.Log("Moving forwards");
    }

    public override void RotateClockwise()
    {
        Debug.Log("Rotating clockwise");
    }

    public override void RotateCounterClockwise()
    {
        Debug.Log("Rotating counterclockwise");
    }
}
