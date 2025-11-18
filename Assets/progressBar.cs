using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
    public Image Filling;   
    public float progress = 0f;  

    public void AddProgress(float amount)
    {
        progress = progress + amount; 
        Filling.fillAmount = progress;
    }

    public void RemoveProgress(float amount)
    {
        progress = progress - amount; 
        Filling.fillAmount = progress;
    }

    public bool IsFull()
    {
        return progress >= 1f;
    }
}


