using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the category/bucket type for an email
/// </summary>
public enum EmailCategory
{
    Personal,
    Spam,
    Urgent
}

/// <summary>
/// Represents a single email with its display data and actual category
/// </summary>
[System.Serializable]
public class EmailData
{
    public string subject;
    public string blurb; // short excerpt from email body
    public string time; // e.g., "11:45 AM", "2:30 PM"
    public EmailCategory actualCategory; // what it ACTUALLY is
    
    public EmailData(string subject, string blurb, string time, EmailCategory actualCategory)
    {
        this.subject = subject;
        this.blurb = blurb;
        this.time = time;
        this.actualCategory = actualCategory;
    }
}
