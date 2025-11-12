using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Represents a single email item in the inbox UI
/// </summary>
public class EmailItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subjectText;
    [SerializeField] private TextMeshProUGUI blurbText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;

    private EmailData emailData;
    private int emailIndex;
    private Button button;
    private bool isSelected = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
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
        UpdateAppearance();
    }

    /// <summary>
    /// Called when this email is clicked
    /// </summary>
    private void OnClick()
    {
        EmailManager.Instance.SelectEmail(this);
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
