using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a single email item in the inbox UI
/// </summary>
public class EmailItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI subjectText;
    [SerializeField] private TextMeshProUGUI blurbText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = new Color(0.9f, 0.9f, 0.9f, 1f); // Light gray

    private EmailData emailData;
    private int emailIndex;
    private Button button;
    private bool isSelected = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("EmailItem: Button component not found! Adding one now...");
            button = gameObject.AddComponent<Button>();
        }
        
        // Don't use button.onClick for now - we'll use OnPointerClick instead
        
        // Ensure background image is visible
        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
        }
        
        if (backgroundImage == null)
        {
            Debug.LogError("EmailItem: Image component not found! Adding one now...");
            backgroundImage = gameObject.AddComponent<Image>();
        }

        // Set image to use a simple solid color (no sprite needed)
        backgroundImage.sprite = null;
        backgroundImage.type = Image.Type.Simple;
        backgroundImage.color = normalColor;
    }

    /// <summary>
    /// Sets the email data to display
    /// </summary>
    public void SetData(EmailData data, int index)
    {
        emailData = data;
        emailIndex = index;

        // Update UI text
        if (subjectText != null) subjectText.text = data.subject;
        if (blurbText != null) blurbText.text = data.blurb;
        if (timeText != null) timeText.text = data.time;

        isSelected = false;
        
        // Ensure background image is visible
        if (backgroundImage != null)
        {
            backgroundImage.color = normalColor;
        }
        
        UpdateAppearance();
    }

    /// <summary>
    /// Called when this email is clicked (via OnClick method)
    /// </summary>
    private void OnClick()
    {
        if (EmailManager.Instance == null)
        {
            Debug.LogError("EmailManager instance not found!");
            return;
        }
        Debug.Log($"Email clicked: {emailData.subject}");
        EmailManager.Instance.SelectEmail(this);
    }

    /// <summary>
    /// Implements IPointerClickHandler for click detection
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"OnPointerClick detected on: {emailData?.subject ?? "Unknown"}");
        OnClick();
    }

    /// <summary>
    /// Sets the selected state of this email
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateAppearance();
    }

    /// <summary>
    /// Updates the visual appearance based on selected state
    /// </summary>
    private void UpdateAppearance()
    {
        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
        }
        
        if (backgroundImage != null)
        {
            backgroundImage.color = isSelected ? selectedColor : normalColor;
        }
    }

    /// <summary>
    /// Returns the email data for this item
    /// </summary>
    public EmailData GetEmailData()
    {
        return emailData;
    }

    /// <summary>
    /// Returns the index of this email in the inbox
    /// </summary>
    public int GetEmailIndex()
    {
        return emailIndex;
    }
}
