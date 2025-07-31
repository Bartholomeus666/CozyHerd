using System;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public List<Cattlegoal> Goals;

    private void Start()
    {
        GeneratGoals();
    }

    private void GeneratGoals()
    {
        Cattlegoal cattlegoal = new Cattlegoal
        {
            Type = CattleType.Cow,
            WantedAmount = 5,
            CurrentAmount = 0
        };
        Goals.Add(cattlegoal);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (Cattlegoal goal in Goals)
        {
            if(other.tag == goal.Type.ToString())
            {
                goal.CheckIfAmountReached(1);
            }
        }
    }
}
