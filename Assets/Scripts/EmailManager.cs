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
    [SerializeField] private int initialEmailCount = 5; // Number of emails to start with
    [SerializeField] private float emailSpacing = 120f; // Vertical spacing between emails

    private List<EmailData> currentEmails = new List<EmailData>();
    private EmailItem selectedEmailItem = null;
    private int correctSortCount = 0;
    private int totalSortCount = 0;

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
            
            // Ensure RectTransform exists
            RectTransform rectTransform = emailGO.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError($"Email prefab {i} doesn't have a RectTransform! The prefab must be a UI element.");
                Debug.LogError("Make sure your Email prefab is created as a UI element (Right-click Canvas > UI > Button - TextMeshPro)");
                Destroy(emailGO);
                continue;
            }
            
            // Log position for debugging
            Debug.Log($"Email {i} position: {rectTransform.anchoredPosition}, size: {rectTransform.sizeDelta}");
            
            // Reset RectTransform to defaults
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
            
            // Force layout rebuild
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);

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
                Debug.Log($"Email {i} setup complete, clickable: {emailGO.GetComponent<Button>() != null}");
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
        }
        else
        {
            Debug.Log($"Incorrect! It was {selectedEmail.actualCategory}, not {category}");
        }
        
        totalSortCount++;

        // Remove the email from the list
        currentEmails.Remove(selectedEmail);
        selectedEmailItem = null;

        // Refresh display
        DisplayEmails();

        // Add a new email to the bottom
        if (currentEmails.Count > 0)
        {
            EmailCategory[] categories = { EmailCategory.Personal, EmailCategory.Spam, EmailCategory.Urgent };
            EmailCategory newCategory = categories[Random.Range(0, categories.Length)];
            currentEmails.Add(EmailContentDatabase.GenerateEmail(newCategory));
            DisplayEmails();
        }

        // Log accuracy
        float accuracy = (float)correctSortCount / totalSortCount * 100f;
        Debug.Log($"Accuracy: {correctSortCount}/{totalSortCount} ({accuracy:F1}%)");
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
