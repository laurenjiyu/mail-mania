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
        "Hey! I wanted to check in and see how you've been doing lately. It feels like forever since we last talked...",
        "Can't believe it's been so long! I was thinking about you the other day and remembered that funny thing we did...",
        "Found this amazing place we should visit together sometime soon. I think you'd absolutely love it based on your interests...",
        "Just wanted to let you know I'm thinking of you and hope everything is going well in your world right now...",
        "Remember when we went to that concert? I was just reminiscing about it and had to smile at the memories we made...",
        "Hope you're doing well! I've been meaning to reach out but life has been crazy. Let's definitely catch up soon..."
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
        "Don't miss out on this incredible opportunity! Click here immediately to claim your exclusive prize before it's too late...",
        "We've detected unusual activity on your account. Please verify your information right away by clicking this link immediately...",
        "Congratulations! You're our lucky winner! To claim your prize, you need to verify your personal details right away...",
        "This amazing product changed my life completely! You won't believe the incredible results I've been getting every single day...",
        "Millions of people are making serious money from home with this simple system. Work from anywhere, no experience needed...",
        "Banks HATE this one weird trick! Learn the secret that financial institutions don't want you to know about today..."
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
        "This issue needs to be resolved immediately as it's blocking critical functionality. We need your input ASAP...",
        "We have a critical situation that requires your urgent attention and expertise. Please respond immediately when you see this...",
        "Please respond ASAP as the server is experiencing serious performance issues that are affecting all users right now...",
        "Your immediate attention is required for this sensitive matter. The situation has escalated and needs your urgent response...",
        "This is time-sensitive and needs to be addressed within the next hour to avoid serious consequences and downtime...",
        "Emergency: We need your help right away to resolve this critical issue before it impacts everyone in the department..."
    };

    private static readonly string[] senderNames = new string[]
    {
        "Alice",
        "Bob",
        "Charlie",
        "Diana",
        "Emma",
        "Frank",
        "Grace",
        "Henry",
        "Iris",
        "Jack",
        "Karen",
        "Leo",
        "Mia"
    };

    private static readonly string[] importantNames = new string[]
    {
        "The President",
        "CEO Jones",
        "Director Smith",
        "The Board",
        "VP Richardson",
        "Chief Executive",
        "Head Honcho",
        "The Big Boss",
        "Senior Partner",
        "Founder & CEO"
    };

    private static readonly string[] spamNames = new string[]
    {
        "Annoying Intern",
        "Random Bot",
        "Totally Legit Inc",
        "Definitely Real",
        "Trust Me LLC",
        "Suspicious Seller",
        "Anonymous Tipster",
        "Weird Email Guy",
        "Sketchy Services",
        "Too Good To Be True",
        "Click Here Now",
        "Prize Committee"
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

        string name;
        
        // Use different name pools based on category
        switch (category)
        {
            case EmailCategory.Personal:
                name = senderNames[Random.Range(0, senderNames.Length)];
                break;
            
            case EmailCategory.Urgent:
                name = importantNames[Random.Range(0, importantNames.Length)];
                break;
            
            case EmailCategory.Spam:
                name = spamNames[Random.Range(0, spamNames.Length)];
                break;
            
            default:
                name = "Unknown";
                break;
        }
        
        return new EmailData(subject, blurb, name, category);
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
