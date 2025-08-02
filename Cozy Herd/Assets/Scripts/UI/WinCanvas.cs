using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : MonoBehaviour
{
    public SceneManagementCozyHerd _sceneManager;

    private void Start()
    {
        _sceneManager = FindAnyObjectByType<SceneManagementCozyHerd>();
    }

    public void NextLevel()
    {
        _sceneManager.NextLevel();
    }
    public void QuitGame()
    {
        _sceneManager.QuitGame();
    }
}
