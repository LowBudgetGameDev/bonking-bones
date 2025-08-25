using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        ScoreManager.Instance.OnScoreIncrease += (object sender, EventArgs e) =>
        {
            UpdateText();
        };

        ScoreManager.Instance.OnScoreDecrease += (object sender, EventArgs e) =>
        {
            UpdateText();
        };

        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.SetText("$" + ScoreManager.Instance.GetScore().ToString("n0"));
        animator.Play("IncreaseScore", -1, 0f);
    }
}
