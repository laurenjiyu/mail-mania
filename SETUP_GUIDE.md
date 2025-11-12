# Email Sorting Game - Setup Guide

## Overview
This system creates an interactive email sorting game where players must categorize emails into Personal, Spam, or Urgent buckets.

## Scripts Created

### 1. **EmailData.cs**
- Defines the `EmailData` class: stores subject, blurb, time, and actual category
- Defines the `EmailCategory` enum: Personal, Spam, Urgent

### 2. **EmailContentDatabase.cs**
- Contains pre-written email content organized by category
- Provides `GenerateEmail(category)` to create random emails of a specific type
- Provides `GenerateMixedEmails(count)` to create a mix of email types

### 3. **EmailManager.cs**
- Manages the inbox and sorting logic
- Tracks which email is selected
- Handles sorting and accuracy tracking
- Singleton pattern (use `EmailManager.Instance`)

### 4. **EmailItem.cs** (Updated)
- Represents individual email UI elements
- Displays subject, blurb, and time
- Handles click selection
- Visual feedback for selected emails

### 5. **BucketButton.cs**
- Attached to bucket buttons (Personal, Spam, Urgent)
- Calls sorting when clicked

## Unity Setup Steps

### Step 1: Set Up the Game Scene
1. Create or open your game scene (02_Game.unity)
2. Create a Canvas for the UI

### Step 2: Create Email Display Area
1. Create a new Panel for the inbox (name it "InboxPanel")
2. Add a Vertical Layout Group component
3. Make it tall enough to display multiple emails (set height to 600+)

### Step 3: Create Email Prefab
1. Select your existing Email.prefab in the Prefabs folder
2. Add these components if not already present:
   - **Button** component (for click detection)
   - **Image** component (for background/selection highlighting)
   - **Layout Element** component (set preferred height to ~100)
3. Create child UI elements:
   - **Subject** - TextMeshProUGUI for the email subject
   - **Blurb** - TextMeshProUGUI for the email excerpt
   - **Time** - TextMeshProUGUI for the time stamp
4. Organize in a vertical layout
5. Add the **EmailItem** script to the root of the prefab
6. In the Inspector, assign the text elements to the EmailItem script fields:
   - Drag Subject TextMesh to "Subject Text" field
   - Drag Blurb TextMesh to "Blurb Text" field
   - Drag Time TextMesh to "Time Text" field
   - Assign the Image component to "Background Image" field

### Step 4: Set Up EmailManager
1. Create an empty GameObject named "EmailManager"
2. Add the **EmailManager** script to it
3. In the Inspector, assign:
   - **Email Container**: The InboxPanel transform
   - **Email Prefab**: Your prepared Email prefab
   - **Initial Email Count**: 5 (or desired starting count)
   - **Email Spacing**: 120 (adjust for your layout)

### Step 5: Create Bucket Buttons
1. Create three buttons in your scene (or in a separate panel):
   - Name them: "PersonalButton", "SpamButton", "UrgentButton"
   - Position them horizontally below the inbox
   - Make them visually distinct (different colors recommended)
2. Add the **BucketButton** script to each button
3. In the Inspector for each button's BucketButton script:
   - **Personal Button**: Set Category to "Personal"
   - **Spam Button**: Set Category to "Spam"
   - **Urgent Button**: Set Category to "Urgent"

### Step 6: Test
1. Play the scene
2. Click on emails to select them (they should highlight)
3. Click a bucket button to sort the selected email
4. Watch the remaining emails move up
5. Check the Console for accuracy tracking

## Gameplay Flow

1. **Player sees inbox** with mixed emails (Personal, Spam, Urgent)
2. **Player clicks an email** to select it (highlights in yellow)
3. **Player clicks a bucket button** to sort it into that category
4. **Email disappears**, emails below move up
5. **New email appears** at the bottom
6. **Accuracy tracked** in console output

## Console Output Example
```
Correct! ✓
Accuracy: 1/1 (100.0%)

Incorrect! It was Urgent, not Personal
Accuracy: 1/2 (50.0%)
```

## Customization

### Change Email Content
Edit `EmailContentDatabase.cs` to add your own email subjects and blurbs:
```csharp
private static readonly string[] personalSubjects = new string[]
{
    "Your custom subject here",
    // Add more...
};
```

### Change Spacing
In EmailManager Inspector, adjust **Email Spacing** value

### Change Colors
In EmailItem Inspector, adjust **Selected Color** and **Normal Color**

### Change Initial Email Count
In EmailManager Inspector, adjust **Initial Email Count**

## Tips
- Use distinct colors for each bucket button to make them clear
- Consider adding sound effects or animations for feedback
- You could add a UI text display showing "Correct: X/Y" accuracy in real-time
- Consider adding difficulty levels that spawn emails faster or with harder-to-categorize content
