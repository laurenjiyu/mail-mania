
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        // Reset game state before switching scenes
        Time.timeScale = 1f;
        Debug.Log($"Loading scene {sceneName} - Time.timeScale reset to 1f");
        
        SceneManager.LoadScene(sceneName);
    }
}




