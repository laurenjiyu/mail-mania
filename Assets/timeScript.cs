using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeScript : MonoBehaviour
{
    public TextMeshProUGUI TimeText;     
    public float workdayDuration = 60f;  // 5 mins * 60 sec
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

        if (9f + hoursPassed > 12f)
        {
            currentHour = (9f + hoursPassed) % 12f;
        }

        int hour = Mathf.FloorToInt(currentHour);
        int minute = Mathf.FloorToInt((currentHour - hour) * 60f);
        List<int> am = new List<int>{9, 10, 11};
        // int pm = [12, 1, 2, 3, 4, 5];

        if (am.Contains(hour)){
            TimeText.text = $"{hour:00}:{minute:00} am";
        } else
        {
            TimeText.text = $"{hour:00}:{minute:00} pm ";
        }

    }

    void EndOfDay()
    {
        if (progressBar.IsFull())
        {
            tryAgainPopup.SetActive(true);
        }
        else
        {
            victoryPopup.SetActive(true);
        }

        Time.timeScale = 0f;
    }
    void CheckIsFull()
    {
        
        if (progressBar.IsFull())
        {
            finished = true;
            tryAgainPopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // void CheckIsNegative()
    // {
    //     if (progressBar.progress < 0f)
    //     {
    //         finished = true;
    //         tryAgainPopup.SetActive(true);
    //         Time.timeScale = 0f;
    //     }
    // }
}
