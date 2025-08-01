using UnityEngine;
using UnityEngine.AI;

public class SimpleAgentReset : MonoBehaviour
{
    [ContextMenu("Reset All Agents")]
    public void ResetAllAgents()
    {
        NavMeshAgent[] agents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        foreach (NavMeshAgent agent in agents)
        {
            // Just reset to enabled state
            agent.enabled = true;

            Debug.Log($"Reset {agent.name}: Type ID = {agent.agentTypeID}, Enabled = {agent.enabled}");
        }
    }
}