using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
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
        scoreText.SetText("$" + ScoreManager.Instance.GetScore().ToString());
    }
}
