using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the email inbox, tracks selected email, and handles sorting
/// </summary>
public class EmailManager : MonoBehaviour
{
    [SerializeField] private Transform emailContainer; // Parent transform where emails are displayed
    [SerializeField] private GameObject emailPrefab; // Email prefab to instantiate
    [SerializeField] private int initialEmailCount = 7; // Number of emails to start with
    public GameObject tryAgainPopup;


    private List<EmailData> currentEmails = new List<EmailData>();
    private EmailItem selectedEmailItem = null;
    private int correctSortCount = 0;
    private int totalSortCount = 0;
    public progressBar progressBar;

    // Singleton pattern
    public static EmailManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Validate setup
        if (emailContainer == null)
        {
            Debug.LogError("EmailManager: emailContainer is not assigned! Please assign it in the Inspector.");
            return;
        }

        if (emailPrefab == null)
        {
            Debug.LogError("EmailManager: emailPrefab is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Check for EventSystem
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogError("ERROR: No EventSystem in scene! Create one: Right-click Hierarchy > UI > Event System");
        }

        // Check for GraphicRaycaster on Canvas
        Canvas canvas = emailContainer.GetComponentInParent<Canvas>();
        if (canvas != null && canvas.GetComponent<GraphicRaycaster>() == null)
        {
            Debug.LogError("ERROR: Canvas doesn't have GraphicRaycaster! Add Component > GraphicRaycaster");
        }

        // Configure the Vertical Layout Group for proper spacing
        VerticalLayoutGroup layoutGroup = emailContainer.GetComponent<VerticalLayoutGroup>();
        if (layoutGroup != null)
        {
            layoutGroup.padding = new RectOffset(10, 10, 10, 10); // left, right, top, bottom
            layoutGroup.spacing = 3; // minimal space between emails
            Debug.Log("Configured Vertical Layout Group spacing");
        }

        // Generate initial emails
        currentEmails = EmailContentDatabase.GenerateMixedEmails(initialEmailCount);
        Debug.Log($"Generated {currentEmails.Count} emails");
        DisplayEmails();
    }

    /// <summary>
    /// Displays all current emails in the inbox
    /// </summary>
    private void DisplayEmails()
    {
        Debug.Log($"DisplayEmails called with {currentEmails.Count} emails");
        
        // Clear existing email displays
        foreach (Transform child in emailContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new email displays
        for (int i = 0; i < currentEmails.Count; i++)
        {
            GameObject emailGO = Instantiate(emailPrefab, emailContainer);
            Debug.Log($"Instantiated email {i}: {currentEmails[i].subject}");

            // FIX: Correct the RectTransform to have positive size
            RectTransform rect = emailGO.GetComponent<RectTransform>();
            if (rect != null)
            {
                // Set to a reasonable default size if it's negative or zero
                if (rect.sizeDelta.x <= 0 || rect.sizeDelta.y <= 0)
                {
                    rect.sizeDelta = new Vector2(1058, 80); // Smaller height to fit more emails
                    Debug.Log($"Email {i} - Fixed RectTransform size to: {rect.sizeDelta}");
                }
            }

            // Set up the email display
            EmailItem emailItem = emailGO.GetComponent<EmailItem>();
            if (emailItem == null)
            {
                Debug.LogError($"Email prefab {i} doesn't have an EmailItem script!");
                Destroy(emailGO);
                continue;
            }
            else
            {
                emailItem.SetData(currentEmails[i], i);
                Debug.Log($"Email {i} setup complete");
            }
        }
    }

    /// <summary>
    /// Called when an email is clicked
    /// </summary>
    public void SelectEmail(EmailItem emailItem)
    {
        // Deselect previous email if any
        if (selectedEmailItem != null)
        {
            selectedEmailItem.SetSelected(false);
        }

        selectedEmailItem = emailItem;
        selectedEmailItem.SetSelected(true);
    }

    /// <summary>
    /// Called when a bucket button is clicked
    /// </summary>
    public void SortEmail(EmailCategory category)
    {
        if (selectedEmailItem == null)
        {
            Debug.LogWarning("No email selected!");
            return;
        }

        EmailData selectedEmail = selectedEmailItem.GetEmailData();
        
        // Check if sort was correct
        if (selectedEmail.actualCategory == category)
        {
            correctSortCount++;
            Debug.Log("Correct! ✓");
            progressBar.AddProgress(0.1f);
        }
        else
        {
            progressBar.RemoveProgress(0.1f);
            Debug.Log($"Incorrect! It was {selectedEmail.actualCategory}, not {category}");
        }
        
        totalSortCount++;

        // Remove the email from the list
        currentEmails.Remove(selectedEmail);
        selectedEmailItem = null;

        // Refresh display
        DisplayEmails();

        // Add a new email to the bottom
        // if (currentEmails.Count > 0)
        // {
        //     EmailCategory[] categories = { EmailCategory.Personal, EmailCategory.Spam, EmailCategory.Urgent };
        //     EmailCategory newCategory = categories[Random.Range(0, categories.Length)];
        //     currentEmails.Add(EmailContentDatabase.GenerateEmail(newCategory));
        //     DisplayEmails();
        // }

        // Log accuracy
        float accuracy = (float)correctSortCount / totalSortCount * 100f;
        Debug.Log($"Accuracy: {correctSortCount}/{totalSortCount} ({accuracy:F1}%)");
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEmailRoutine());
    }

    private IEnumerator SpawnEmailRoutine()
    {
        while (true)
        {
            float interval = Random.Range(5f * 0.2f, 5f * 0.8f);
            yield return new WaitForSeconds(interval);

            //yield return new WaitForSeconds(5f); // wait 5 seconds
            Debug.Log($"num emails {currentEmails.Count}");

            if (currentEmails.Count < 7)
            {
                EmailCategory[] categories = { EmailCategory.Personal, EmailCategory.Spam, EmailCategory.Urgent };
                EmailCategory newCategory = categories[Random.Range(0, categories.Length)];
                EmailData newEmail = EmailContentDatabase.GenerateEmail(newCategory);

                currentEmails.Insert(0, newEmail);

                DisplayEmails();
            }
            else
            {
                tryAgainPopup.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// Gets current sort accuracy
    /// </summary>
    public float GetAccuracy()
    {
        if (totalSortCount == 0) return 0f;
        return (float)correctSortCount / totalSortCount;
    }

    /// <summary>
    /// Gets current stats
    /// </summary>
    public (int correct, int total) GetStats()
    {
        return (correctSortCount, totalSortCount);
    }
}
