using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Debug script to test if clicks are working
/// </summary>
public class ClickDebug : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked at: " + Input.mousePosition);
            
            // Cast a ray to see what we're clicking on
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            
            if (results.Count > 0)
            {
                Debug.Log($"Clicked on: {results[0].gameObject.name}");
            }
            else
            {
                Debug.Log("No UI element clicked");
            }
        }
    }
}
