using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerEscort : AIController
{
    public enum CurrentAIState { ChooseFriend, Guard, Patrol, ChooseEnemy, Attack }

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
                    ChangeCurrentState(CurrentAIState.Attack);
                }
                // If true, we transition OUT of the ChooseEnemy state and into another state
                break;
            case CurrentAIState.Attack:
                // Debug.Log("Do Attack");
                // Do the behaviors associated with our Attack state
                DoAttackState();
                // Check for transitions OUT of our Attack state
                if (!IsHasFriend())
                {
                    ChangeCurrentState(CurrentAIState.ChooseFriend);
                }
                if (!IsHasTarget())
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

    // Behaviors


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
        AIController[] allEnemies = FindObjectsOfType<AIController>();

        // Assume that we cannot see any enemy tanks
        AIController closestEnemy = null;
        float closestEnemyTankDistance = 0f;

        // Iterate through the enemy AIControllers one at a time
        foreach (AIController enemy in allEnemies)
        {
            // If the current enemy has a tank pawn
            if (enemy.pawn != null)
            {
                // If the current enemy's tank pawn is not this tank's pawn
                if (enemy.pawn != pawn)
                {
                    // If we can see the current enemy's tank pawn
                    if (CanSee(enemy.pawn.gameObject))
                    {
                        // If the closest visible enemy has not yet been set
                        if (closestEnemy == null)
                        {
                            // Then this one is the new closest
                            closestEnemy = enemy;
                            closestEnemyTankDistance = Vector3.Distance(pawn.transform.position, closestEnemy.pawn.transform.position);
                        }
                        // If this one is closer than the closest
                        else if (Vector3.Distance(pawn.transform.position, enemy.pawn.transform.position) <= closestEnemyTankDistance)
                        {
                            // Then this one is the new closest
                            closestEnemy = enemy;
                            closestEnemyTankDistance = Vector3.Distance(pawn.transform.position, closestEnemy.pawn.transform.position);
                        }
                    }
                }
            }
        }

        // If we saw any enemy tanks
        if (closestEnemy != null)
        {
            // Target the closest enemy's tank
            target = closestEnemy.pawn.gameObject;
        }
        // If we did not see any enemy tanks
        else
        {
            // Do not target any tanks
            target = null;
        }
    }

    protected bool IsHasFriend()
    {
        return (friend != null);
    }
}
