using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerZoner : AIController
{
    public enum CurrentAIState { ChooseTarget, Scan, Alert, Chase, Attack, Withdraw }

    public CurrentAIState currentAIControllerState;

    // Start is called before the first frame update
    public override void Start()
    {
        ChangeCurrentState(currentAIControllerState);

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
                    ChangeCurrentState(CurrentAIState.Scan);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case CurrentAIState.Scan:
                // Debug.Log("Do Scan");
                // Do the behaviors associated with our Scan state
                DoScanState();
                // Check for transitions OUT of our Scan state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Chase);
                }
                if (CanHear(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }

                // If true, we transition OUT of the Scan state and into another state
                break;
            case CurrentAIState.Alert:
                // Debug.Log("Do Alert");
                // Do the behaviors associated with our Alert state
                DoAlertState();
                // Check for transitions OUT of our Alert state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Chase);
                }
                if (!CanHear(target))
                {
                    ChangeCurrentState(CurrentAIState.Scan);
                }
                // If true, we transition OUT of the Alert state and into another state
                break;
            case CurrentAIState.Chase:
                // Debug.Log("Do Chase");
                // Do the behaviors associated with our Chase state
                DoChaseState();
                // Check for transitions OUT of our Chase state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }
                if (IsDistanceLessThan(target, 15))
                {
                    ChangeCurrentState(CurrentAIState.Attack);
                }

                // If true, we transition OUT of the Chase state and into another state
                break;
            case CurrentAIState.Attack:
                // Debug.Log("Do Attack");
                // Do the behaviors associated with our Attack state
                DoAttackState();
                // Check for transitions OUT of our Attack state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }
                if (!IsDistanceLessThan(target, 15))
                {
                    ChangeCurrentState(CurrentAIState.Chase);
                }
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeCurrentState(CurrentAIState.Withdraw);
                }

                // If true, we transition OUT of the Attack state and into another state
                break;
            case CurrentAIState.Withdraw:
                // Debug.Log("Do Withdraw");
                // Do the behaviors associated with our Withdraw state
                DoWithdrawState();
                // Check for transitions OUT of our Withdraw state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Alert);
                }
                if (!IsDistanceLessThan(target, 12))
                {
                    ChangeCurrentState(CurrentAIState.Attack);
                }

                // If true, we transition OUT of the Withdraw state and into another state
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
