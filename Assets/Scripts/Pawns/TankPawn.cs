using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float nextEventTime;

    // Variables for SFX
    public AudioClip sfxTankFire;

    // Start is called before the first frame update
    public override void Start()
    {
        nextEventTime = Time.time + 1 / fireRate;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveBackward(float moveSpeedPercent)
    {
        if (mover != null)
        {
            mover.Move(transform.forward, -moveSpeed * moveSpeedPercent);
        }
        else
        {
            Debug.LogWarning("Warning: Mover component is not initialized!");
        }
    }

    public override void MoveBackward()
    {
        MoveBackward(1);
    }

    public override void MoveForward(float moveSpeedPercent)
    {
        if (mover != null)
        {
            mover.Move(transform.forward, moveSpeed * moveSpeedPercent);
        }
        else
        {
            Debug.LogWarning("Warning: Mover component is not initialized!");
        }
    }

    public override void MoveForward()
    {
        MoveForward(1);
    }

    public override void RotateClockwise(float turnSpeedPercent)
    {
        if (mover != null)
        {
            mover.Rotate(turnSpeed * turnSpeedPercent);
        }
        else
        {
            Debug.LogWarning("Warning: Mover component is not initialized!");
        }
    }

    public override void RotateClockwise()
    {
        RotateClockwise(1);
    }

    public override void RotateCounterClockwise(float turnSpeedPercent)
    {
        if (mover != null)
        {
            mover.Rotate(-turnSpeed * turnSpeedPercent);
        }
        else
        {
            Debug.LogWarning("Warning: Mover component is not initialized!");
        }
    }

    public override void RotateCounterClockwise()
    {
        RotateCounterClockwise(1);
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        if (shooter != null)
        {
            if (Time.time >= nextEventTime)
            {
                // Tell shooter to shoot
                shooter.Shoot(shellPrefab, fireForce, damageDone * damageMultiplier, shellLifespan);
                // Reset cooldown timer
                nextEventTime = Time.time + 1 / fireRate;

                // Play shooting sound effect
                audioSource.PlayOneShot(sfxTankFire);
                // AudioSource.PlayClipAtPoint(sfxTankFire, shooter.transform.position);
            }
        }
    }

    public override void AddToDamageMultiplier(float dmAmount)
    {
        damageMultiplier += dmAmount;
    }

    public override void RemoveFromDamageMultiplier(float dmAmount)
    {
        damageMultiplier -= dmAmount;
    }
}
