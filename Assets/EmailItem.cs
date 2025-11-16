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
