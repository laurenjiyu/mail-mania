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
    public string name; // sender name
    public EmailCategory actualCategory; // what it ACTUALLY is
    
    public EmailData(string subject, string blurb, string name, EmailCategory actualCategory)
    {
        this.subject = subject;
        this.blurb = blurb;
        this.name = name;
        this.actualCategory = actualCategory;
    }
}
