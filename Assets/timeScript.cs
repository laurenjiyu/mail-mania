using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeScript : MonoBehaviour
{
    public TextMeshProUGUI TimeText;     
    public float workdayDuration = 300f;  // 5 mins * 60 sec
    public GameObject victoryPopup;       
    public GameObject tryAgainPopup;        
    public progressBar progressBar;  

    private float elapsed = 0f;
    private bool finished = false;

    void Update()
    {
        if (finished) return;

        elapsed += Time.deltaTime;

        if (elapsed >= workdayDuration) 
        {
            elapsed = workdayDuration;
            finished = true;
            EndOfDay();
        }
        
        CheckIsNegative();
        CheckIsFull();
        Debug.Log($"Progress: {progressBar.progress}");
        UpdateClock();

    }

    void UpdateClock()
    {
        float percentGoneBy = elapsed / workdayDuration; 

        // how many hours is the percent, add to 9 am
        float hoursPassed = percentGoneBy * 8f; 
        float currentHour = 9f + hoursPassed;  

        int hour = Mathf.FloorToInt(currentHour);
        int minute = Mathf.FloorToInt((currentHour - hour) * 60f);

        TimeText.text = $"{hour:00}:{minute:00}";
    }

    void EndOfDay()
    {
        if (progressBar.IsFull())
        {
            victoryPopup.SetActive(true);
        }
        else
        {
            tryAgainPopup.SetActive(true);
        }

        Time.timeScale = 0f;
    }
    void CheckIsFull()
    {
        
        if (progressBar.IsFull())
        {
            finished = true;
            victoryPopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void CheckIsNegative()
    {
        if (progressBar.progress < 0f)
        {
            finished = true;
            tryAgainPopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
