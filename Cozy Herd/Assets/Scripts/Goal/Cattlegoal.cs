using UnityEngine;

[CreateAssetMenu(fileName = "New Cattle Goal", menuName = "Goals/Cattle Goal")]
public class Cattlegoal : ScriptableObject
{
    public CattleType Type;
    public int WantedAmount = 0;
    public int CurrentAmount = 0;

    public bool Finished = false;

    public void Initialize(CattleType type, int wantedAm)
    {
        Type = type;
        WantedAmount = wantedAm;
    }

    public void CheckIfAmountReached(int amount)
    {
        if(CurrentAmount + amount >= WantedAmount)
        {
            CurrentAmount += amount;
            Finished = true;
            Debug.Log($"Goal reached for {Type} with {CurrentAmount} out of {WantedAmount}.");
        }
        else
        {
            CurrentAmount += amount;
            Debug.Log($"Current amount for {Type}: {CurrentAmount}. Still need {WantedAmount - CurrentAmount} more.");
        }
    }
}

