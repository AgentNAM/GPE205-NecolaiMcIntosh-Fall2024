using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerCoward : AIController
{
    public enum CurrentAIState { ChooseTarget, Guard, Sneak, Withdraw, Flee }

    public CurrentAIState currentAIControllerState;

    public float sneakSpeedPercent;

    public float sneakDistance;
    public float withdrawDistance;

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
                    ChangeCurrentState(CurrentAIState.Guard);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case CurrentAIState.Guard:
                // Debug.Log("Do Guard");
                // Do the behaviors associated with our Guard state
                DoGuardState();
                // Check for transitions OUT of our Guard state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (IsSeen(target))
                {
                    ChangeCurrentState(CurrentAIState.Flee);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Sneak);
                }

                // If true, we transition OUT of the Guard state and into another state
                break;
            case CurrentAIState.Sneak:
                // Debug.Log("Do Sneak");
                // Do the behaviors associated with our Sneak state
                DoSneakState();
                // Check for transitions OUT of our Sneak state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (IsSeen(target))
                {
                    ChangeCurrentState(CurrentAIState.Flee);
                }
                if (IsDistanceLessThan(target, sneakDistance))
                {
                    ChangeCurrentState(CurrentAIState.Withdraw);
                }

                // If true, we transition OUT of the Sneak state and into another state
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
                if (!IsDistanceLessThan(target, withdrawDistance))
                {
                    ChangeCurrentState(CurrentAIState.Flee);
                }

                // If true, we transition OUT of the Withdraw state and into another state
                break;
            case CurrentAIState.Flee:
                // Debug.Log("Do Flee");
                // Do the behaviors associated with our Flee state
                DoFleeState();
                // Check for transitions OUT of our Flee state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (!IsSeen(target))
                {
                    ChangeCurrentState(CurrentAIState.Sneak);
                }
                if (!IsDistanceLessThan(target, fleeDistance))
                {
                    ChangeCurrentState(CurrentAIState.Guard);
                }

                // If true, we transition OUT of the Flee state and into another state
                break;
        }
    }

    // States
    protected void DoSneakState()
    {
        if (target != null)
        {
            Sneak();
        }
    }
    protected void DoWithdrawState()
    {
        if (target != null)
        {
            BackAway(target);
            Shoot();
        }
    }

    protected void DoCowerState()
    {
        // Do Nothing
    }

    protected void DoCryState()
    {
        if (target != null)
        {
            // Back away from the player in the Cry State
            BackAway(target);
        }
    }


    // Behaviors
    protected void Sneak()
    {
        pawn.MoveForward(sneakSpeedPercent);
        pawn.RotateTowards(target.transform.position);
    }

    protected void BackAway(Vector3 targetPosition)
    {
        // Rotate towards the targetPosition
        pawn.RotateTowards(targetPosition);
        // Move Backward
        pawn.MoveBackward();
    }

    protected void BackAway(Transform targetTransform)
    {
        BackAway(targetTransform.position);
    }

    protected void BackAway(GameObject target)
    {
        BackAway(target.transform);
    }


    // Helper Methods and Transition Methods
    public virtual void ChangeCurrentState(CurrentAIState newState)
    {
        // Change the current state
        currentAIControllerState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }
}
