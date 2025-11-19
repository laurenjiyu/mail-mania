using UnityEngine;

/// <summary>
/// Enforces a fixed resolution of 1440x1080 for the game
/// This script should be attached to a GameObject in the first scene
/// </summary>
public class ResolutionManager : MonoBehaviour
{
    [Header("Target Resolution")]
    public int targetWidth = 1440;
    public int targetHeight = 1080;
    public bool fullscreen = false;
    
    [Header("Debug")]
    public bool showDebugInfo = true;
    
    private void Awake()
    {
        // Make this object persist across scenes
        DontDestroyOnLoad(gameObject);
        
        // Set the target resolution immediately
        SetTargetResolution();
    }
    
    private void Start()
    {
        // Double-check resolution is set correctly
        SetTargetResolution();
    }
    
    /// <summary>
    /// Sets the game to the target resolution
    /// </summary>
    public void SetTargetResolution()
    {
        // Get current resolution
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;
        bool currentFullscreen = Screen.fullScreen;
        
        if (showDebugInfo)
        {
            Debug.Log($"Current Resolution: {currentWidth}x{currentHeight}, Fullscreen: {currentFullscreen}");
            Debug.Log($"Target Resolution: {targetWidth}x{targetHeight}, Fullscreen: {fullscreen}");
        }
        
        // Only change if different from target
        if (currentWidth != targetWidth || currentHeight != targetHeight || currentFullscreen != fullscreen)
        {
            Screen.SetResolution(targetWidth, targetHeight, fullscreen);
            
            if (showDebugInfo)
            {
                Debug.Log($"Resolution changed to: {targetWidth}x{targetHeight}, Fullscreen: {fullscreen}");
            }
        }
        else if (showDebugInfo)
        {
            Debug.Log("Resolution already matches target - no change needed");
        }
    }
    
    /// <summary>
    /// Call this if you want to manually refresh the resolution
    /// </summary>
    public void RefreshResolution()
    {
        SetTargetResolution();
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        // Reapply resolution when the application gains focus
        // This helps if the user alt-tabs or the resolution gets changed externally
        if (hasFocus)
        {
            SetTargetResolution();
        }
    }
}
