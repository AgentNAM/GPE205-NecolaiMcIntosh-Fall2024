using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public float pointsAwarded;

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth -= amount;
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    public void Heal(float amount, Pawn source)
    {
        currentHealth += amount;
        Debug.Log(source.name + " did " + amount + " healing to " + gameObject.name);

        // Prevent overhealing
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Die(Pawn source)
    {
        source.controller.AddToScore(pointsAwarded);

        Controller controller = GetComponent<Pawn>().controller;

        if (controller != null)
        {
            controller.RemoveFromLives();

            // Signal to our game manager to respawn our player, possibly
        }

        Destroy(gameObject);
    }
}
