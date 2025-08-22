using System;
using UnityEngine;
using UnityEngine.UI;

public class ResetUI : MonoBehaviour
{
    [SerializeField] private Button resetButton;

    private void Start()
    {
        resetButton.onClick.AddListener(() =>
        {
            RagdollSpawner.Instance.SpawnRagdoll();
            Hide();
        });

        RagdollSpawner.Instance.OnRagdollInAirTooLong += (object sender, EventArgs e) =>
        {
            Show();
        };

        RagdollSpawner.Instance.OnRagdollSpawned += (object sender, EventArgs e) =>
        {
            Hide();
        };

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
