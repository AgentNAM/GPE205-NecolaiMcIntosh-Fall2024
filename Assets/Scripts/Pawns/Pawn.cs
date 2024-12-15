using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Variable for referencing our controller
    public Controller controller;

    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;

    // Variable for our Mover
    public Mover mover;
    // Variable for our  hooter
    public Shooter shooter;
    // Variable for our NoiseMaker
    public NoiseMaker noiseMaker;
    // Variable for our AudioSource
    public AudioSource audioSource;

    /*
    // Variable for our AudioManager
    public AudioManager audioManager;
    */

    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for damage done
    public float damageDone;
    // Variable for damage multiplier
    public float damageMultiplier;
    // Variable for bullet lifespan
    public float shellLifespan;
    // Variable for Rate of Fire
    public float fireRate;
    // Variable for Volume Distance when Moving
    public float movingVolumeDistance;

    /*
    // Variable to hold our audio source
    public AudioSource audioSource;
    */

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Initialize our Mover component
        mover = GetComponent<Mover>();
        // Initialize our Shooter component
        shooter = GetComponent<Shooter>();
        // Initialize our NoiseMaker component
        noiseMaker = GetComponent<NoiseMaker>();
        // Initialize our AudioSource component
        audioSource = GetComponent<AudioSource>();

        /*
        // Initialize our AudioManager component
        audioManager = GetComponent<AudioManager>();
        */
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveForward(float moveSpeedPercent);
    public abstract void MoveBackward();
    public abstract void MoveBackward(float moveSpeedPercent);
    public abstract void RotateClockwise();
    public abstract void RotateClockwise(float turnSpeedPercent);
    public abstract void RotateCounterClockwise();
    public abstract void RotateCounterClockwise(float turnSpeedPercent);
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void Shoot();
    public abstract void AddToDamageMultiplier(float dmAmount);
    public abstract void RemoveFromDamageMultiplier(float dmAmount);
}
