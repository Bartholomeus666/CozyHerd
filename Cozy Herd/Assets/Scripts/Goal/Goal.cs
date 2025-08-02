using System;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public List<Cattlegoal> Goals;
    public GameObject VictoryCanvas;
    public bool GoalsFulfilled = false;


    private void Start()
    {
        GoalsFulfilled = false;
        // Initialize goals if needed
        foreach (Cattlegoal goal in Goals)
        {
            goal.CurrentAmount = 0;
            goal.Finished = false;
        }
    }

    private void Update()
    {
        if (GoalsFulfilled)
        {
            if (CheckGateState())
            {
                VictoryStarts();
                GoalsFulfilled = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        foreach (Cattlegoal goal in Goals)
        {
            if (other.tag == goal.Type.ToString())
            {
                goal.CheckIfAmountReached(1);
                if (goal.Finished && CheckIfAllGoalsReached())
                {
                    GoalsFulfilled = true;
                }
            }
        }
    }



    private void VictoryStarts()
    {
        Instantiate(VictoryCanvas, null);
        Time.timeScale = 0f; // Pause the game
    }

    private bool CheckGateState()
    {
        return GetComponentInChildren<GateWork>().isGateClosed;
    }
    private bool CheckIfAllGoalsReached()
    {
        foreach (Cattlegoal goal in Goals)
        {
            if (!goal.Finished)
            {
                return false; // If any goal is not finished, exit
            }
            else 
                continue;
        }
        return true;
    }




}



public enum CattleType
{
    Cow,
    Sheep
}
