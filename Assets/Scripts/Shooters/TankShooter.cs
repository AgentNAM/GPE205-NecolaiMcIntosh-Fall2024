using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        // Instantiate the bullet
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get DamageOnHit component
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();
        // If it has one...
        if (doh != null)
        {
            // ... set the damageDone
            doh.damageDone = damageDone;

            // ... set the owner to the pawn that fired the bullet
            doh.owner = GetComponent<Pawn>();
        }

        // Get the Rigidbody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        // If it has one...
        if (rb != null)
        {
            // ... AddForce method to make it move forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }

        // Destroy it after a set time
        Destroy(newShell, lifespan);
    }
}
