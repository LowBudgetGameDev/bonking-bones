using System;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private bool needsToBeShown;

    private void Start()
    {
        WinManager.Instance.OnReachGoal += (object sender, EventArgs e) =>
        {
            needsToBeShown = true;
        };

        RagdollSpawner.Instance.OnRagdollSpawned += (object sender, EventArgs e) =>
        {
            if (!needsToBeShown) return;

            Show();
            needsToBeShown = false;
        };

        continueButton.onClick.AddListener(() =>
        {
            Hide();
        });

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject?.SetActive(false);
    }
}
