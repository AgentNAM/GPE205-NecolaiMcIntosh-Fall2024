using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControllerSleepy : AIController
{
    public enum CurrentAIState { ChooseTarget, Sleep, Panic, Chase, Attack, Tiptoe }

    public CurrentAIState currentAIControllerState;

    public float tiptoePercent = 0.1f;

    private Health tankHealth;
    public float currentHealth;
    public float lastHealth;

    // Start is called before the first frame update
    public override void Start()
    {
        tankHealth = pawn.GetComponent<Health>();
        currentHealth = pawn.GetComponent<Health>().currentHealth;
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
                    ChangeCurrentState(CurrentAIState.Sleep);
                }
                // If true, we transition OUT of the ChooseTarget state and into another state
                break;
            case CurrentAIState.Sleep:
                // Debug.Log("Do Sleep");
                // Do the behaviors associated with our Sleep state
                DoSleepState();
                // Check for transitions OUT of our Sleep state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (TookDamage())
                {
                    ChangeCurrentState(CurrentAIState.Panic);
                }

                // If true, we transition OUT of the Sleep state and into another state
                break;
            case CurrentAIState.Panic:
                // Debug.Log("Do Panic");
                // Do the behaviors associated with our Panic state
                DoPanicState();
                // Check for transitions OUT of our Panic state
                if (!IsHasTarget())
                {
                    ChangeCurrentState(CurrentAIState.ChooseTarget);
                }
                if (CanSee(target))
                {
                    ChangeCurrentState(CurrentAIState.Chase);
                }
                if (HasTimePassed(2))
                {
                    ChangeCurrentState(CurrentAIState.Tiptoe);
                }

                // If true, we transition OUT of the Panic state and into another state
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
                    ChangeCurrentState(CurrentAIState.Tiptoe);
                }
                if (IsDistanceLessThan(target, 10))
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
                    ChangeCurrentState(CurrentAIState.Tiptoe);
                }
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeCurrentState(CurrentAIState.Chase);
                }

                // If true, we transition OUT of the Attack state and into another state
                break;
            case CurrentAIState.Tiptoe:
                // Debug.Log("Do Tiptoe");
                // Do the behaviors associated with our Tiptoe state
                DoTiptoeState();
                // Check for transitions OUT of our Tiptoe state
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
                    ChangeCurrentState(CurrentAIState.Panic);
                }
                if (HasTimePassed(5))
                {
                    ChangeCurrentState(CurrentAIState.Sleep);
                }

                // If true, we transition OUT of the Tiptoe state and into another state
                break;
        }
    }

    // States
    protected void DoSleepState()
    {
        UpdateHealth();
    }
    protected void DoPanicState()
    {
        Panic();
    }

    protected void DoTiptoeState()
    {
        if (target != null)
        {
            Tiptoe();
        }
    }

    // Behaviors
    protected void Panic()
    {
        pawn.RotateClockwise(1.25f);
        pawn.Shoot();
    }

    protected void Tiptoe()
    {
        pawn.MoveForward(tiptoePercent);
        pawn.RotateTowards(target.transform.position);
    }

    protected void UpdateHealth()
    {
        lastHealth = currentHealth;
        currentHealth = pawn.GetComponent<Health>().currentHealth;

        // Debug.Log(lastHealth + " -> " + currentHealth);
    }

    // Helper Methods and Transition Methods
    public virtual void ChangeCurrentState(CurrentAIState newState)
    {
        // Change the current state
        currentAIControllerState = newState;
        // Save the time when we change states
        lastStateChangeTime = Time.time;
    }

    protected bool TookDamage()
    {
        return (currentHealth < lastHealth);
    }
}
