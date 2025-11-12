using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Database of pre-prepared email contents for random generation
/// </summary>
public class EmailContentDatabase : MonoBehaviour
{
    // Personal email subjects and blurbs
    private static readonly string[] personalSubjects = new string[]
    {
        "Happy Birthday!",
        "How's your week going?",
        "Let's catch up soon",
        "Check out this recipe",
        "Thanks for the birthday gift",
        "Coffee this weekend?",
        "Miss you!",
        "Memories from last trip"
    };

    private static readonly string[] personalBlurbs = new string[]
    {
        "Hey! I wanted to check in and see how you've been...",
        "Can't believe it's been so long! I was thinking...",
        "Found this amazing place we should visit together...",
        "Just wanted to let you know I'm thinking of you...",
        "Remember when we... I was just reminiscing about...",
        "Hope you're doing well! I've been meaning to..."
    };

    // Spam email subjects and blurbs
    private static readonly string[] spamSubjects = new string[]
    {
        "Click here to claim your prize!",
        "URGENT: Act now for 80% off!",
        "You've won! Congratulations!!!",
        "Limited time offer expires TODAY",
        "Enlarge your... naturally",
        "Nigerian Prince needs your help",
        "Work from home! Make $5000/week",
        "FREE MONEY - No catch!"
    };

    private static readonly string[] spamBlurbs = new string[]
    {
        "Don't miss out! Click here immediately to...",
        "We've detected unusual activity. Verify your...",
        "Congratulations! You're our lucky winner. To...",
        "This amazing product changed my life! You won't...",
        "Millions of people are making money from home...",
        "Banks HATE this one weird trick! Click to..."
    };

    // Urgent email subjects and blurbs
    private static readonly string[] urgentSubjects = new string[]
    {
        "ACTION REQUIRED: Project deadline tomorrow",
        "ASAP: Critical bug in production",
        "Emergency meeting scheduled for 3 PM",
        "Your account will be deleted",
        "URGENT: Security alert",
        "Immediate response needed",
        "Boss needs this ASAP",
        "System maintenance - action required"
    };

    private static readonly string[] urgentBlurbs = new string[]
    {
        "This issue needs to be resolved immediately. The...",
        "We have a critical situation that requires your...",
        "Please respond ASAP. The server is experiencing...",
        "Your immediate attention is required for this...",
        "This is time-sensitive and needs to be addressed...",
        "Emergency: We need your help right away to..."
    };

    private static readonly string[] timeSlots = new string[]
    {
        "9:15 AM",
        "9:32 AM",
        "10:47 AM",
        "11:05 AM",
        "11:45 AM",
        "12:30 PM",
        "1:15 PM",
        "2:00 PM",
        "2:45 PM",
        "3:30 PM",
        "4:12 PM",
        "5:00 PM",
        "5:30 PM"
    };

    /// <summary>
    /// Generates a random email of the specified category
    /// </summary>
    public static EmailData GenerateEmail(EmailCategory category)
    {
        string subject, blurb;
        
        switch (category)
        {
            case EmailCategory.Personal:
                subject = personalSubjects[Random.Range(0, personalSubjects.Length)];
                blurb = personalBlurbs[Random.Range(0, personalBlurbs.Length)];
                break;
            
            case EmailCategory.Spam:
                subject = spamSubjects[Random.Range(0, spamSubjects.Length)];
                blurb = spamBlurbs[Random.Range(0, spamBlurbs.Length)];
                break;
            
            case EmailCategory.Urgent:
                subject = urgentSubjects[Random.Range(0, urgentSubjects.Length)];
                blurb = urgentBlurbs[Random.Range(0, urgentBlurbs.Length)];
                break;
            
            default:
                subject = "Unknown";
                blurb = "Unknown email type";
                break;
        }

        string time = timeSlots[Random.Range(0, timeSlots.Length)];
        return new EmailData(subject, blurb, time, category);
    }

    /// <summary>
    /// Generates a mix of emails from all categories
    /// </summary>
    public static List<EmailData> GenerateMixedEmails(int count)
    {
        List<EmailData> emails = new List<EmailData>();
        EmailCategory[] categories = { EmailCategory.Personal, EmailCategory.Spam, EmailCategory.Urgent };
        
        for (int i = 0; i < count; i++)
        {
            EmailCategory randomCategory = categories[Random.Range(0, categories.Length)];
            emails.Add(GenerateEmail(randomCategory));
        }
        
        return emails;
    }
}
