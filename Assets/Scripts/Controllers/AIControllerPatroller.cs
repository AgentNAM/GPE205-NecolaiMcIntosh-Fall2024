using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerPatroller : AIController
{
    public enum CurrentAIState { ChooseTarget, Patrol, Alert, Attack, Withdraw, BackToPost }

    public CurrentAIState currentAIControllerState;

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
            case CurrentAIState.ChooseTarget:
                // Debug.Log("Do ChooseTarget");
                // Do the behaviors associated with our ChooseTarget state
                DoChooseTargetState();
                // Check for transitions OUT of our ChooseTarget state
                if (IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.Patrol);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case CurrentAIState.Patrol:
                Debug.Log("Do Patrol");
                // Do the behaviors associated with our Patrol state
                DoPatrolState();
                // Check for transitions OUT of our Patrol state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }
                // If true, we transition OUT of the Patrol state and into another state
                break;
            case CurrentAIState.Alert:
                Debug.Log("Do Alert");
                // Do the behaviors associated with our Alert state
                DoAlertState();
                // Check for transitions OUT of our Alert state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Patrol);
                }
                if (HasTimePassed(2))
                {
                    ChangeCurrentState(CurrentAIState.Attack);
                }
                // If true, we transition OUT of the Alert state and into another state
                break;
            case CurrentAIState.Attack:
                Debug.Log("Do Attack");
                // Do the behaviors associated with our Attack state
                DoAttackState();
                // Check for transitions OUT of our Attack state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.BackToPost);
                }
                if (!IsDistanceLessThan(post, postWanderDistance))
                {
                    ChangeCurrentState(CurrentAIState.Withdraw);
                }
                // If true, we transition OUT of the Attack state and into another state
                break;
            case CurrentAIState.Withdraw:
                Debug.Log("Do Withdraw");
                // Do the behaviors associated with our Withdraw state
                DoWithdrawState();
                // Check for transitions OUT of our Withdraw state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.BackToPost);
                }
                if (IsDistanceLessThan(post, postWanderDistance))
                {
                    ChangeCurrentState(CurrentAIState.Attack);
                }
                // If true, we transition OUT of the Withdraw state and into another state
                break;
            case CurrentAIState.BackToPost:
                Debug.Log("Do BackToPost");
                // Do the behaviors associated with our BackToPost state
                DoBackToPostState();
                // Check for transitions OUT of our BackToPost state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }
                if (IsDistanceLessThan(post, waypointStopDistance))
                {
                    ChangeCurrentState(CurrentAIState.Patrol);
                }
                // If true, we transition OUT of the BackToPost state and into another state
                break;
        }

    }

    // States


    // Behaviors


    // Helper Methods and Transition Methods
    public virtual void ChangeCurrentState(CurrentAIState newState)
    {
        // Change the current state
        currentAIControllerState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }
}
