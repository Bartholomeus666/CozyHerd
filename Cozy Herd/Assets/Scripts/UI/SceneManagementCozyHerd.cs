using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagementCozyHerd: MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt("CurrentLevel", 1);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed

        int level = PlayerPrefs.GetInt("CurrentLevel", 1);
        string levelName = "Level" + level;

        PlayerPrefs.SetInt("CurrentLevel", level + 1);

        StartCoroutine(LoadLevelForWebGL(levelName));
    }

    private IEnumerator LoadLevelForWebGL(string sceneName)
    {
        // Load the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        yield return asyncLoad;

        // WebGL needs extra time for initialization
        yield return new WaitForSeconds(1.5f);

        // Set active scene
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}