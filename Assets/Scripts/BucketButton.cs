using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles bucket button interactions for sorting emails
/// </summary>
public class BucketButton : MonoBehaviour
{
    [SerializeField] private EmailCategory category;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (EmailManager.Instance == null)
        {
            Debug.LogError("EmailManager instance not found! Make sure EmailManager is set up in the scene.");
            return;
        }
        EmailManager.Instance.SortEmail(category);
    }
}
