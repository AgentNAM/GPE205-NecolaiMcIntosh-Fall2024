using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerEscort : AIController
{
    public enum CurrentAIState { ChooseFriend, Guard, Patrol, ChooseEnemy, Defend }

    public CurrentAIState currentAIControllerState;

    public GameObject friend;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void ProcessInputs()
    {
        switch (currentAIControllerState)
        {
            case CurrentAIState.ChooseFriend:
                // Debug.Log("Do ChooseFriend");
                // Do the behaviors associated with our ChooseFriend state
                DoChooseFriendState();
                // Check for transitions OUT of our ChooseFriend state
                if (IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.Guard);
                }
                // If true, we transition OUT of the ChooseFriend state and into another state
                break;
            case CurrentAIState.Guard:
                // Debug.Log("Do Guard");
                // Do the behaviors associated with our Guard state
                DoGuardState();
                // Check for transitions OUT of our Guard state
                if (!IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.ChooseFriend);
                }
                if (IsDistanceLessThan(friend, 5))
                {
                    ChangeCurrentState(CurrentAIState.Patrol);
                }
                if (HasTimePassed(1))
                {
                    ChangeCurrentState(CurrentAIState.ChooseEnemy);
                }

                // If true, we transition OUT of the Guard state and into another state
                break;
            case CurrentAIState.Patrol:
                // Debug.Log("Do Patrol");
                // Do the behaviors associated with our Patrol state
                DoPatrolState();
                // Check for transitions OUT of our Patrol state
                if (!IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.ChooseFriend);
                }
                if (!IsDistanceLessThan(friend, 5))
                {
                    ChangeCurrentState(CurrentAIState.Guard);
                }
                if (HasTimePassed(1))
                {
                    ChangeCurrentState(CurrentAIState.ChooseEnemy);
                }

                // If true, we transition OUT of the Patrol state and into another state
                break;
            case CurrentAIState.ChooseEnemy:
                // Debug.Log("Do ChooseEnemy");
                // Do the behaviors associated with our ChooseEnemy state
                DoChooseEnemyState();
                // Check for transitions OUT of our ChooseEnemy state
                if (!IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.ChooseFriend);
                }
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.Guard);
                }
                if (IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.Defend);
                }
                // If true, we transition OUT of the ChooseEnemy state and into another state
                break;
            case CurrentAIState.Defend:
                // Debug.Log("Do Defend");
                // Do the behaviors associated with our Defend state
                DoDefendState();
                // Check for transitions OUT of our Defend state
                if (!IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.ChooseFriend);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.ChooseEnemy);
                }

                // If true, we transition OUT of the Attack state and into another state
                break;
        }
        
    }

    // States
    protected void DoChooseFriendState()
    {
        BefriendNearestPlayer();
    }

    protected void DoChooseEnemyState()
    {
        TargetNearestVisibleEnemy();
    }

    protected void DoDefendState()
    {
        if (target != null)
        {
            LookAt(target.transform);
            Shoot();
        }
    }

    // Behaviors
    protected void LookAt(Vector3 targetPosition)
    {
        // Rotate towards the targetPosition
        pawn.RotateTowards(targetPosition);
    }

    protected void LookAt(Transform targetTransform)
    {
        // Look at the target transform's position
        LookAt(targetTransform.position);
    }
    protected void LookAt(GameObject target)
    {
        // Look at the target game object's transform
        LookAt(target.transform);
    }

    // Helper Methods and Transition Methods
    public virtual void ChangeCurrentState(CurrentAIState newState)
    {
        // Change the current state
        currentAIControllerState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }

    public void BefriendNearestPlayer()
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

                        // Befriend the closest tank
                        friend = closestTank.gameObject;
                    }
                }
            }
        }
    }

    public void TargetNearestVisibleEnemy()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.enemies != null)
            {
                // And there are players in it
                if (GameManager.instance.enemies.Count > 0)
                {
                    // Assume that we cannot see any enemy tanks
                    Pawn closestTank = null;
                    float closestTankDistance = 0f;

                    // Iterate through the enemy controllers one at a time
                    foreach (AIController enemy in GameManager.instance.enemies)
                    {
                        // If the current enemy is not us
                        if (enemy != this)
                        {
                            // If the current enemy has a tank pawn
                            if (enemy.pawn != null)
                            {
                                // If we can see the current enemy's tank pawn
                                if (CanSee(enemy.pawn.gameObject))
                                {
                                    // If the closest visible enemy has not yet been set
                                    if (closestTank == null)
                                    {
                                        // Then this one is the new closest
                                        closestTank = enemy.pawn;
                                        closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                                    }
                                    // If this one is closer than the closest
                                    else if (Vector3.Distance(pawn.transform.position, enemy.pawn.transform.position) <= closestTankDistance)
                                    {
                                        // Then this one is the new closest
                                        closestTank = enemy.pawn;
                                        closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
                                    }
                                }
                            }
                        }
                    }

                    // If we saw any enemy tanks
                    if (closestTank != null)
                    {
                        // Target the closest enemy's tank
                        target = closestTank.gameObject;
                    }
                    // If we did not see any enemy tanks
                    else
                    {
                        // Do not target any tanks
                        target = null;
                    }
                }
            }
        }
    }

    protected bool IsHasFriend()
    {
        return (friend != null);
    }
}
