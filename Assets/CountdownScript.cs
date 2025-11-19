using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float countdownDuration = 1f; // seconds per number
    [SerializeField] private string gameSceneName = "02_Game"; // Name of the game scene to load
    [SerializeField] private Color textColor = new Color(0.996f, 1f, 0.933f, 1f); // FEFFEE in RGB
    private bool isCountingDown = false;

    private void Start()
    {
        // Auto-start countdown when scene loads
        StartCountdown();
    }

    /// <summary>
    /// Starts the countdown sequence
    /// </summary>
    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    /// <summary>
    /// Coroutine that handles the countdown animation
    /// </summary>
    private IEnumerator CountdownCoroutine()
    {
        isCountingDown = true;

        // Set text color
        if (countdownText != null)
        {
            countdownText.color = textColor;
        }

        // Count from 3 to 1
        for (int i = 3; i >= 1; i--)
        {
            if (countdownText != null)
            {
                countdownText.text = i.ToString();
            }
            yield return new WaitForSeconds(countdownDuration);
        }

        // Show GO!
        if (countdownText != null)
        {
            countdownText.text = "GO!";
        }
        yield return new WaitForSeconds(countdownDuration);

        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }
}
