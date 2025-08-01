using UnityEngine;
using UnityEngine.AI;

public class VerifyAllAgentTypes : MonoBehaviour
{
    private void Start()
    {
        Invoke("CheckAllAgents", 3f);
    }

    private void CheckAllAgents()
    {
        NavMeshAgent[] agents = Object.FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        Debug.Log("=== ALL AGENT TYPES ===");
        foreach (NavMeshAgent agent in agents)
        {
            Debug.Log($"{agent.name}: Agent Type ID = {agent.agentTypeID}, On NavMesh = {agent.isOnNavMesh}");
        }
    }
}
