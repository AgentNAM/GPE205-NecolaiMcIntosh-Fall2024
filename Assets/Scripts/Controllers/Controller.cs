using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    // Variable to hold our Pawn
    public Pawn pawn;

    // Score variable
    public float score;

    // Lives variable
    public int lives;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void ProcessInputs();

    // Function that updates our score
    public virtual void AddToScore(float scoreAmount)
    {
        score += scoreAmount;
    }

    public virtual void RemoveFromScore(float scoreAmount)
    {
        score -= scoreAmount;
    }

    // Function that updates our lives
    public virtual void AddToLives(int livesAmount)
    {
        lives += livesAmount;
    }

    public virtual void AddToLives()
    {
        AddToLives(1);
    }

    public virtual void RemoveFromLives(int livesAmount)
    {
        lives -= livesAmount;
    }

    public virtual void RemoveFromLives()
    {
        RemoveFromLives(1);
    }
}
