using UnityEngine;

public class Cattlegoal : MonoBehaviour
{
    public CattleType Type;
    public int WantedAmount = 0;
    public int CurrentAmount = 0;

    public void CheckIfAmountReached(int amount)
    {
        if(CurrentAmount + amount >= WantedAmount)
        {
            Debug.Log($"Goal reached for {Type} with {CurrentAmount} out of {WantedAmount}.");
        }
        else
        {
            CurrentAmount += amount;
            Debug.Log($"Current amount for {Type}: {CurrentAmount}. Still need {WantedAmount - CurrentAmount} more.");
        }
    }
}

public enum CattleType
{
    Cow,
    Sheep
}
