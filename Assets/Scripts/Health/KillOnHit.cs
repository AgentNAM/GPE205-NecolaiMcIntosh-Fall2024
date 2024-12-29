using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the object we are colliding with
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // Only damage if the object has a Health component
        if (otherHealth != null)
        {
            // Inflict damage
            otherHealth.Die();
        }
    }
}