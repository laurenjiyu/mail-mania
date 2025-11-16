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
    [SerializeField] private TextMeshProUGUI nameText;
    private Image backgroundImage;
    private Color selectedColor = new Color(1f, 1f, 0.624f, 1f); // FFF79F in RGB

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
        
        // Get the Image component for color changes
        backgroundImage = GetComponent<Image>();
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
        if (nameText != null) nameText.text = data.name;

        isSelected = false;
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
        
        // Change color when selected
        if (backgroundImage != null)
        {
            if (selected)
            {
                backgroundImage.color = selectedColor; // FFF79F color
            }
            else
            {
                backgroundImage.color = Color.white; // Default white
            }
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
