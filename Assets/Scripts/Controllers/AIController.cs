using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Scan, Chase, Attack, BackToPost, Flee, Patrol };

    public AIState currentState;

    protected float lastStateChangeTime;

    public GameObject target;

    // Start is called before the first frame update
    public override void Start()
    {
        ChangeState(currentState);

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Make Decisions
        ProcessInputs();

        base.Update();
    }

    public override void ProcessInputs()
    {
        Debug.Log("Making Decisions");
        switch (currentState)
        {
            case AIState.Guard:
                // Do the behaviors associated with our Guard state
                Debug.Log("Do Guard");
                // Check for transitions OUT of our Guard state
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Chase);
                }
                // If true, we transition OUT of the Guard state and into another state
                break;
            case AIState.Scan:
                Debug.Log("Do Scan");
                // Do the behaviors associated with our Scan state

                // Check for transitions OUT of our Scan state

                // If true, we transition OUT of the Scan state and into another state
                break;
            case AIState.Chase:
                Debug.Log("Do Chase");
                // Do the behaviors associated with our Chase state
                DoChaseState();
                // Check for transitions OUT of our Chase state
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Guard);
                }
                // If true, we transition OUT of the Chase state and into another state
                break;
            case AIState.Attack:
                Debug.Log("Do Attack");
                // Do the behaviors associated with our Attack state
                DoAttackState();
                // Check for transitions OUT of our Attack state

                // If true, we transition OUT of the Attack state and into another state
                break;
            case AIState.BackToPost:
                Debug.Log("Do BackToPost");
                // Do the behaviors associated with our BackToPost state

                // Check for transitions OUT of our BackToPost state

                // If true, we transition OUT of the BackToPost state and into another state
                break;
            case AIState.Flee:
                Debug.Log("Do Flee");
                // Do the behaviors associated with our Flee state

                // Check for transitions OUT of our Flee state

                // If true, we transition OUT of the Flee state and into another state
                break;
            case AIState.Patrol:
                Debug.Log("Do Patrol");
                // Do the behaviors associated with our Patrol state

                // Check for transitions OUT of our Patrol state

                // If true, we transition OUT of the Patrol state and into another state
                break;
        }
    }

    // States
    protected void DoGuardState()
    {
        // Do Nothing
    }

    protected void DoChaseState()
    {
        // Seek in the Seek State
        Seek(target);
    }

    protected void DoAttackState()
    {
        // Chase after the target
        Seek(target.transform);
        // Tell our pawn to Shoot
        Shoot();
    }

    // Behaviors
    protected void Seek(GameObject target)
    {
        // Rotate towards the target
        pawn.RotateTowards(target.transform.position);
        // Move Forward
        pawn.MoveForward();
    }

    protected void Seek(Transform targetTransform)
    {
        Seek(targetTransform.gameObject);
    }

    protected void Shoot()
    {
        // Tell our pawn to shoot
        pawn.Shoot();
    }

    // Helper Methods and Transition Methods
    public virtual void ChangeState(AIState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
