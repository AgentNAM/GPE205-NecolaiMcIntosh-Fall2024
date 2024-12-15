using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageBoostPowerup : Powerup
{
    public float damageBoostAmount;

    public override void Apply(PowerupManager target)
    {
        // Apply Damage Multiplier Changes
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.AddToDamageMultiplier(damageBoostAmount);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Apply Damage Multiplier Changes
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.RemoveFromDamageMultiplier(damageBoostAmount);
        }
    }
}
