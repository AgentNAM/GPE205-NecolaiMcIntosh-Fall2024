using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerDefault : AIController
{
    public enum CurrentAIState { ChooseTarget, Guard, Chase, Attack }

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
                    ChangeCurrentState(CurrentAIState.Guard);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case CurrentAIState.Guard:
                Debug.Log("Do Guard");
                // Do the behaviors associated with our Guard state
                DoGuardState();
                // Check for transitions OUT of our Guard state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }

                // If true, we transition OUT of the Guard state and into another state
                break;
            case CurrentAIState.Chase:
                Debug.Log("Do Chase");
                // Do the behaviors associated with our Chase state
                DoChaseState();
                // Check for transitions OUT of our Chase state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }

                // If true, we transition OUT of the Chase state and into another state
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

                // If true, we transition OUT of the Attack state and into another state
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
