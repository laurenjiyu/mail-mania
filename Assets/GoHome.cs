using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popupPanel;

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0f;       
    }

    public void ReturnHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }
}




