using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(BuildNavMeshForWebGL());
    }

    private IEnumerator BuildNavMeshForWebGL()
    {
        // Wait for scene to fully load
        yield return new WaitForSeconds(1f);

        // Find all NavMesh surfaces and rebuild them
        NavMeshSurface[] surfaces = Object.FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);

        Debug.Log($"Found {surfaces.Length} NavMesh surfaces for WebGL build");

        foreach (NavMeshSurface surface in surfaces)
        {
            if (surface != null && surface.gameObject.activeInHierarchy)
            {
                Debug.Log($"Building NavMesh for: {surface.name}");
                surface.BuildNavMesh();

                // Wait a frame between builds
                yield return null;
            }
        }

        // Verify NavMesh exists
        yield return new WaitForSeconds(0.5f);
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        if (triangulation.vertices.Length > 0)
        {
            Debug.Log($"WebGL NavMesh built successfully: {triangulation.vertices.Length} vertices");
        }
        else
        {
            Debug.LogError("WebGL NavMesh build failed!");
        }
    }
}