using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This component supplies a public method that can be
/// called externally, such as through a UI button press.
/// 
/// Allows the player to switch to a pre-defined scene.
/// </summary>
public class UI_SwitchScene : MonoBehaviour
{
    
    public void SwitchSceneTo(string sceneName)
    {
        // Reset game state before switching scenes
        Time.timeScale = 1f;
        Debug.Log($"Switching to {sceneName} - Time.timeScale reset to 1f");
        
        SceneManager.LoadScene(sceneName);
    }

}
