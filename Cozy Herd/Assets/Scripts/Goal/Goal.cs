using System;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public List<Cattlegoal> Goals;

    private void OnTriggerEnter(Collider other)
    {
        foreach (Cattlegoal goal in Goals)
        {
            if (other.tag == goal.Type.ToString())
            {
                goal.CheckIfAmountReached(1);
            }
        }
    }

    private void Start()
    {
        // Initialize goals if needed
        foreach (Cattlegoal goal in Goals)
        {
            goal.CurrentAmount = 0;
        }
    }
}

public enum CattleType
{
    Cow,
    Sheep
}
