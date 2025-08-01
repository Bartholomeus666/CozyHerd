using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MinimalWebGLFix : MonoBehaviour
{
    private void Start()
    {
        // Only run fixes in WebGL builds
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(SimpleWebGLFix());
#endif
    }

    private IEnumerator SimpleWebGLFix()
    {
        // Wait for scene to load
        yield return new WaitForSeconds(2f);

        NavMeshAgent[] agents = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);

        foreach (NavMeshAgent agent in agents)
        {
            if (!agent.isOnNavMesh)
            {
                // Simple position fix
                if (NavMesh.SamplePosition(agent.transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                {
                    agent.transform.position = hit.position;
                    Debug.Log($"WebGL: Adjusted {agent.name} position");
                }
            }
        }
    }
}