using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public float pointsAwarded;

    public Image healthIcon;

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
        // Play damage sound
        Instantiate(GetComponent<Pawn>().sfxDamagePrefab);

        // Decrease current health
        currentHealth -= amount;
        if (source != null)
        {
            Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        }

        // Prevent overkill
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update health icon
        UpdateHealthIcon();

        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    public void Heal(float amount, Pawn source)
    {
        // Increase current health
        currentHealth += amount;
        if (source != null)
        {
            Debug.Log(source.name + " did " + amount + " healing to " + gameObject.name);
        }

        // Prevent overhealing
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update health icon
        UpdateHealthIcon();
    }

    public void Die()
    {
        // Play death sound
        Instantiate(GetComponent<Pawn>().sfxDeathPrefab);
        // Get controller
        Controller controller = GetComponent<Pawn>().controller;

        if (controller != null)
        {
            controller.RemoveFromLives();

            controller.HandleRespawn();
            /*
            // Signal to our game manager to respawn our player, possibly
            GameManager.instance.RespawnPlayer(controller);
            */
        }

        Destroy(gameObject);
    }

    public void Die(Pawn source)
    {
        source.controller.AddToScore(pointsAwarded);

        Die();
    }

    public void UpdateHealthIcon()
    {
        healthIcon.fillAmount = currentHealth / maxHealth;
    }
}
