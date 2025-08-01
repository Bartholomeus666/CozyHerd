using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ForceDefaultAgents : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FixAllAgentTypes());
    }

    private IEnumerator FixAllAgentTypes()
    {
        yield return new WaitForSeconds(2f); // Wait for NavMesh

        NavMeshAgent[] allAgents = Object.FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        Debug.Log($"Found {allAgents.Length} agents to fix");

        foreach (NavMeshAgent agent in allAgents)
        {
            Debug.Log($"Fixing agent: {agent.name}, current ID: {agent.agentTypeID}");

            // Disable agent
            agent.enabled = false;
            yield return null;

            // Set to default agent type (ID: 0)
            agent.agentTypeID = 0;

            // Re-enable
            yield return null;
            agent.enabled = true;

            yield return new WaitForSeconds(0.1f);

            Debug.Log($"{agent.name} - New ID: {agent.agentTypeID}, On NavMesh: {agent.isOnNavMesh}");
        }

        Debug.Log("All agents fixed!");
    }
}