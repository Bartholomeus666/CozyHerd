using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DogDebug : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(DebugDogAgent());
    }

    private IEnumerator DebugDogAgent()
    {
        yield return new WaitForSeconds(2f); // Wait for NavMesh to be ready

        Debug.Log("=== DOG NAVMESH DEBUG ===");

        if (agent == null)
        {
            Debug.LogError("DOG: No NavMeshAgent component found!");
            yield break;
        }

        Debug.Log($"DOG Position: {transform.position}");
        Debug.Log($"DOG Agent Type ID: {agent.agentTypeID}");
        Debug.Log($"DOG Enabled: {agent.enabled}");
        Debug.Log($"DOG GameObject Active: {gameObject.activeInHierarchy}");
        Debug.Log($"DOG Height: {agent.height}");
        Debug.Log($"DOG Radius: {agent.radius}");
        Debug.Log($"DOG Base Offset: {agent.baseOffset}");

        // Check NavMesh at dog's position
        NavMeshHit hit;
        bool foundNavMesh = NavMesh.SamplePosition(transform.position, out hit, 50f, NavMesh.AllAreas);

        Debug.Log($"DOG: NavMesh found within 50 units: {foundNavMesh}");
        if (foundNavMesh)
        {
            Debug.Log($"DOG: Nearest NavMesh position: {hit.position}");
            Debug.Log($"DOG: Distance to NavMesh: {Vector3.Distance(transform.position, hit.position):F2}");
            Debug.Log($"DOG: NavMesh area: {hit.mask}");
        }

        // Compare with global NavMesh
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        Debug.Log($"DOG: Global NavMesh vertices: {triangulation.vertices.Length}");

        // Try to fix the dog
        if (!foundNavMesh || !agent.isOnNavMesh)
        {
            Debug.Log("DOG: Attempting to fix NavMesh position...");
            yield return StartCoroutine(FixDogPosition());
        }
    }

    private IEnumerator FixDogPosition()
    {
        // Disable agent
        agent.enabled = false;

        // Try different search radii
        float[] radii = { 1f, 5f, 10f, 25f, 50f, 100f };
        bool positioned = false;

        foreach (float radius in radii)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, radius, NavMesh.AllAreas))
            {
                transform.position = hit.position;
                Debug.Log($"DOG: Moved to NavMesh position: {hit.position} (radius: {radius})");
                positioned = true;
                break;
            }
        }

        if (!positioned)
        {
            // Try positions around other working agents
            NavMeshAgent[] otherAgents = FindObjectsOfType<NavMeshAgent>();
            foreach (NavMeshAgent otherAgent in otherAgents)
            {
                if (otherAgent != agent && otherAgent.isOnNavMesh)
                {
                    Vector3 nearOther = otherAgent.transform.position + Vector3.right * 2f;
                    if (NavMesh.SamplePosition(nearOther, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                    {
                        transform.position = hit.position;
                        Debug.Log($"DOG: Positioned near working agent: {hit.position}");
                        positioned = true;
                        break;
                    }
                }
            }
        }

        // Re-enable agent
        yield return null;
        agent.enabled = true;

        yield return new WaitForSeconds(0.2f);

        Debug.Log($"DOG: Final result - IsOnNavMesh: {agent.isOnNavMesh}");

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("DOG: Still failed to place on NavMesh!");
        }
    }
}