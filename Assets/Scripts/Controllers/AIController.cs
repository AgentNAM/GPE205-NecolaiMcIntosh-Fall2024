using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { ChooseTarget, Guard, Scan, Chase, Attack, Flee, Patrol, BackToPost };

    public AIState currentState;

    protected float lastStateChangeTime;

    public GameObject target;

    public float hearingDistance;

    public float fieldOfView;

    public float targetFieldOfView;

    public float fleeDistance;

    public Transform[] waypoints;
    public float waypointStopDistance;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it is tracking our enemies in a list
            if (GameManager.instance.enemies != null)
            {
                // Register ourselves with the GameManager
                GameManager.instance.enemies.Add(this);
            }
        }

        ChangeState(currentState);

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Make Decisions
        if (pawn != null)
        {
            ProcessInputs();
        }

        base.Update();
    }

    public override void ProcessInputs()
    {
        // Debug.Log("Making Decisions");
        switch (currentState)
        {
            case AIState.ChooseTarget:
                // Debug.Log("Do ChooseTarget");
                // Do the behaviors associated with our ChooseTarget state
                DoChooseTargetState();
                // Check for transitions OUT of our ChooseTarget state
                if (IsHasTarget())
                {
                    ChangeState(AIState.Patrol);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case AIState.Guard:
                // Debug.Log("Do Guard");
                // Do the behaviors associated with our Guard state

                // Check for transitions OUT of our Guard state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {
                    /* if (IsDistanceLessThan(target, 10))
                    {
                        ChangeState(AIState.Chase);
                    }*/
                    if (CanSee(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                // If true, we transition OUT of the Guard state and into another state
                break;
            case AIState.Scan:
                // Debug.Log("Do Scan");
                // Do the behaviors associated with our Scan state

                // Check for transitions OUT of our Scan state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {

                }
                // If true, we transition OUT of the Scan state and into another state
                break;
            case AIState.Chase:
                // Debug.Log("Do Chase");
                // Do the behaviors associated with our Chase state
                DoChaseState();
                // Check for transitions OUT of our Chase state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {
                    /*if (!IsDistanceLessThan(target, 10))
                    {
                        ChangeState(AIState.Guard);
                    }*/
                    if (!CanSee(target))
                    {
                        ChangeState(AIState.Patrol);
                    }
                    /*
                    if (IsDistanceLessThan(target, 10))
                    {
                        ChangeState(AIState.Flee);
                    }
                    */
                }
                // If true, we transition OUT of the Chase state and into another state
                break;
            case AIState.Attack:
                // Debug.Log("Do Attack");
                // Do the behaviors associated with our Attack state
                DoAttackState();
                // Check for transitions OUT of our Attack state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {

                }
                // If true, we transition OUT of the Attack state and into another state
                break;
            case AIState.Flee:
                // Debug.Log("Do Flee");
                // Do the behaviors associated with our Flee state
                DoFleeState();
                // Check for transitions OUT of our Flee state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {
                    if (!IsDistanceLessThan(target, fleeDistance))
                    {
                        ChangeState(AIState.Guard);
                    }
                }
                // If true, we transition OUT of the Flee state and into another state
                break;
            case AIState.Patrol:
                // Debug.Log("Do Patrol");
                // Do the behaviors associated with our Patrol state
                DoPatrolState();
                // Check for transitions OUT of our Patrol state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {
                    if (CanSee(target))
                    {
                        ChangeState(AIState.Attack);
                    }
                }
                // If true, we transition OUT of the Patrol state and into another state
                break;
            case AIState.BackToPost:
                // Debug.Log("Do BackToPost");
                // Do the behaviors associated with our BackToPost state

                // Check for transitions OUT of our BackToPost state
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {

                }
                // If true, we transition OUT of the BackToPost state and into another state
                break;
        }
    }

    // States
    protected void DoChooseTargetState()
    {
        // Target the first player's pawn
        TargetNearestPlayer();
    }

    protected void DoGuardState()
    {
        // Do Nothing
    }

    protected void DoChaseState()
    {
        if (target != null)
        {
            // Seek in the Seek State
            Seek(target);
        }
    }

    protected void DoAttackState()
    {
        if (target != null)
        {
            // Chase after the target
            Seek(target.transform);
            // Tell our pawn to Shoot
            Shoot();
        }
    }

    protected void DoFleeState()
    {
        if (target != null)
        {
            // Flee from target
            Flee();
        }
    }

    protected void DoPatrolState()
    {
        Patrol();
    }

    // Behaviors
    protected void Seek(Vector3 targetPosition)
    {
        // Rotate towards the targetPosition
        pawn.RotateTowards(targetPosition);
        // Move Forward
        pawn.MoveForward();
    }

    protected void Seek(Transform targetTransform)
    {
        // Seek the target transform's position
        Seek(targetTransform.position);
    }
    protected void Seek(GameObject target)
    {
        // Seek the target game object's transform
        Seek(target.transform);
    }

    protected void Shoot()
    {
        // Tell our pawn to shoot
        pawn.Shoot();
    }

    protected void Flee()
    {
        // Find the Vector to our target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the Vector away from our target by multiplying by -1
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        // Find the distance between the target and the player
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        // Convert targetDistance into a percent
        float percentOfFleeDistance = targetDistance / fleeDistance;
        // Clamp percentage between 0 (0% of fleeDistance away) and 1 (100% of fleeDistance away) to make sure we don't flee more than fleeDistance away or backwards towards the player
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        // Invert percentage
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance * flippedPercentOfFleeDistance;

        // Seek the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector);
    }

    protected void Patrol()
    {
        // If we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            // If we are close enough, then increment to next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
        /*
        if (waypoints.Length > 0)
        {
            // If we have enough waypoints in our list to move to a current waypoint
            if (waypoints.Length > currentWaypoint)
            {
                // Then seek that waypoint
                Seek(waypoints[currentWaypoint]);
                // If we are close enough, then increment to next waypoint
                if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                RestartPatrol();
            }
        }
        */
    }

    protected void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }

    // Helper Methods and Transition Methods
    public virtual void ChangeState(AIState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    // Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    public void TargetNearestTank()
    {
        // Get an array of all the tanks (pawns)
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        // Assume that the first tank is the closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        // Iterate through the tank pawns one at a time
        foreach (Pawn tank in allTanks)
        {
            // If the current tank pawn is not this tank's pawn
            if (tank != pawn)
            {
                // If this one is closer than the closest
                if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
                {
                    // Then this one is the new closest
                    closestTank = tank;
                    closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                }
            }
        }

        // Target the closest tank
        target = closestTank.gameObject;
    }

    public void TargetNearestPlayer()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    // And the first player's tank exists
                    if (GameManager.instance.players[0].pawn != null)
                    {
                        // Assume that the first tank is the closest
                        Pawn closestTank = GameManager.instance.players[0].pawn;
                        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

                        // Iterate through the player controllers one at a time
                        foreach (PlayerController player in GameManager.instance.players)
                        {

                            // If this one is closer than the closest
                            if (Vector3.Distance(pawn.transform.position, player.pawn.transform.position) <= closestTankDistance)
                            {
                                // Then this one is the new closest
                                closestTank = player.pawn;
                                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                            }
                        }

                        // Target the closest tank
                        target = closestTank.gameObject;
                    }
                }
            }
        }
        
    }

    protected bool IsHasTarget()
    {
        // Return true if we have a target, false if we don't
        return (target != null);
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (target != null)
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
        else
        {
            return false;
        }
    }

    protected bool CanHear(GameObject target)
    {
        if (target != null)
        {
            // Get the target's NoiseMaker
            NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
            // If they don't have one, they can't make noise, so return false
            if (noiseMaker == null)
            {
                return false;
            }

            // If they are making no noise, return false
            if (noiseMaker.volumeDistance <= 0)
            {
                return false;
            }

            // If the target is making noise, then a the volumeDistance of the noisemaker to the hearing distance of this AI agent
            float totalDistance = noiseMaker.volumeDistance + hearingDistance;

            // If the distance between our pawn and target is closer than the total distance
            // AKA r1 + r2
            if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
            {
                // then we hear the target
                return true;
            }
            else
            {
                // Otherwise, we cannot hear the target
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        if (target != null)
        {
            // Find the vector from the agent to the target
            Vector3 agentToTargetVector = target.transform.position - pawn.transform.position;

            // Find the angle between the forward facing vector and the vector to our target
            float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

            if (angleToTarget < fieldOfView)
            {

                RaycastHit hit;
                Debug.DrawRay(pawn.transform.position + Vector3.up / 3.0f, agentToTargetVector, Color.green);
                // Do the additional check of whether anything is blocking the view of the target
                if (Physics.Raycast(pawn.transform.position + Vector3.up / 3.0f, agentToTargetVector, out hit))
                {
                    if (hit.collider.gameObject == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsSeen(GameObject target)
    {
        if (target != null)
        {
            // Find the vector from the agent to the target
            Vector3 targetToAgentVector = pawn.transform.position - target.transform.position;

            // Find the angle between the forward facing vector and the vector to our target
            float angleToAgent = Vector3.Angle(targetToAgentVector, target.transform.forward);

            if (angleToAgent < targetFieldOfView)
            {

                RaycastHit hit;
                Debug.DrawRay(target.transform.position + Vector3.up / 3.0f, targetToAgentVector, Color.red);
                // Do the additional check of whether anything is blocking the view of the target
                if (Physics.Raycast(target.transform.position + Vector3.up / 3.0f, targetToAgentVector, out hit))
                {
                    if (hit.collider.gameObject == pawn.gameObject)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool HasTimePassed(float timeInSeconds)
    {
        // If the amount of time since the last state change is greater than the ...
        if (Time.time - lastStateChangeTime >= timeInSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
