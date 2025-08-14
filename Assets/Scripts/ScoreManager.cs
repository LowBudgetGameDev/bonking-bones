using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnScoreIncrease;
    public event EventHandler OnScoreDecrease;

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        OnScoreIncrease?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        OnScoreDecrease?.Invoke(this, EventArgs.Empty);
    }

    public int GetScore()
    {
        return score;
    }
}
