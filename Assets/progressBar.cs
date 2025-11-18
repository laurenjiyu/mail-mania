using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
    public Image Filling;   // Image with fill type = Filled
    public float progress = 0f;  // 0 to 1

    public void AddProgress(float amount)
    {
        progress = Mathf.Clamp01(progress + amount);
        Filling.fillAmount = progress;
    }

    public bool IsFull()
    {
        return progress >= 1f;
    }
}


