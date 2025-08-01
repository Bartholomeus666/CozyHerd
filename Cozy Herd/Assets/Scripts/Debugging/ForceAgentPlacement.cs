using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ForceAgentPlacement : MonoBehaviour
{
    [SerializeField] private bool debugMode = true;

    private void Start()
    {
        StartCoroutine(ForceAllAgentsOnNavMesh());
    }

    private IEnumerator ForceAllAgentsOnNavMesh()
    {
        // Wait for NavMesh to be fully ready
        yield return new WaitForSeconds(3f);

        // Verify NavMesh exists
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        if (triangulation.vertices.Length == 0)
        {
            Debug.LogError("No NavMesh found! Cannot place agents.");
            yield break;
        }

        if (debugMode) Debug.Log($"NavMesh ready with {triangulation.vertices.Length} vertices");

        // With this:
        NavMeshAgent[] allAgents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        if (debugMode) Debug.Log($"Found {allAgents.Length} agents to place");

        foreach (NavMeshAgent agent in allAgents)
        {
            yield return StartCoroutine(PlaceAgentSafely(agent));
        }

        if (debugMode) Debug.Log("All agents placement completed");
    }

    private IEnumerator PlaceAgentSafely(NavMeshAgent agent)
    {
        if (agent == null) yield break;

        string agentName = agent.name;
        Vector3 originalPosition = agent.transform.position;

        if (debugMode) Debug.Log($"Processing {agentName} at {originalPosition}");

        // Step 1: Disable agent completely
        agent.enabled = false;
        yield return new WaitForEndOfFrame();

        // Step 2: Fix agent type
        agent.agentTypeID = 0;

        // Step 3: Find valid NavMesh position
        NavMeshHit hit;
        bool foundPosition = false;
        Vector3 validPosition = originalPosition;

        // Try multiple search radii
        float[] searchRadii = { 0.5f, 2f, 5f, 10f, 20f, 50f };

        foreach (float radius in searchRadii)
        {
            if (NavMesh.SamplePosition(originalPosition, out hit, radius, NavMesh.AllAreas))
            {
                validPosition = hit.position;
                foundPosition = true;
                if (debugMode) Debug.Log($"{agentName}: Found position at radius {radius}, position: {validPosition}");
                break;
            }
        }

        // If still no position found, try near center of NavMesh
        if (!foundPosition)
        {
            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
            if (triangulation.vertices.Length > 0)
            {
                Vector3 centerish = triangulation.vertices[triangulation.vertices.Length / 2];
                if (NavMesh.SamplePosition(centerish, out hit, 10f, NavMesh.AllAreas))
                {
                    validPosition = hit.position;
                    foundPosition = true;
                    if (debugMode) Debug.Log($"{agentName}: Placed near NavMesh center: {validPosition}");
                }
            }
        }

        if (!foundPosition)
        {
            Debug.LogError($"{agentName}: Could not find any valid NavMesh position!");
            yield break;
        }

        // Step 4: Move agent to valid position
        agent.transform.position = validPosition;
        yield return new WaitForEndOfFrame();

        // Step 5: Re-enable agent
        agent.enabled = true;
        yield return new WaitForSeconds(0.5f); // Give time to initialize

        // Step 6: Verify placement
        if (agent.isOnNavMesh)
        {
            if (debugMode) Debug.Log($"{agentName}: Successfully placed on NavMesh at {agent.transform.position}");
        }
        else
        {
            Debug.LogError($"{agentName}: Still not on NavMesh after placement attempt!");

            // Last resort: try warping
            if (NavMesh.SamplePosition(validPosition, out hit, 1f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                yield return new WaitForSeconds(0.2f);

                if (debugMode) Debug.Log($"{agentName}: Warp attempt result: {agent.isOnNavMesh}");
            }
        }
    }
}