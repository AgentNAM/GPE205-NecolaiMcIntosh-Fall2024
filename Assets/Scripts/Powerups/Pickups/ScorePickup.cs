using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    public ScorePowerup powerup;

    // Variable for Audio Player prefab
    public GameObject sfxCollectPrefab;

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
        // Variable for PowerupManager
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If other object has it
        if (powerupManager != null)
        {
            // Play collection sound effect
            Instantiate(sfxCollectPrefab);

            // Add the powerup
            powerupManager.Add(powerup);

            // Destroy this pickup
            Destroy(gameObject);
        }
    }
}
