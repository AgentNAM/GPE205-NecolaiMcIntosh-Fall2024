using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePowerup : Powerup
{
    public float pointsAwarded;

    public override void Apply(PowerupManager target)
    {
        // Apply Score Changes
        Controller targetController = target.GetComponent<Pawn>().controller;
        if (targetController != null)
        {
            targetController.AddToScore(pointsAwarded);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Apply Score Changes
        Controller targetController = target.GetComponent<Pawn>().controller;
        if (targetController != null)
        {
            targetController.RemoveFromScore(pointsAwarded);
        }
    }
}
