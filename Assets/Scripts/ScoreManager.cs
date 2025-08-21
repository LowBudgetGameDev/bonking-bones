using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnScoreIncrease;
    public event EventHandler OnScoreDecrease;

    private long score;
    private long scaledUpScore = 90000000000000; // This score will be 100x the normal score allowing for some decimals but will be rounded down when getting the final score

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore(float amount)
    {
        scaledUpScore += (int) (amount * 100);
        score = scaledUpScore / 100;
        OnScoreIncrease?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseScore(long amount)
    {
        scaledUpScore -= amount * 100;
        score = scaledUpScore / 100;
        OnScoreDecrease?.Invoke(this, EventArgs.Empty);
    }

    public long GetScore()
    {
        return score;
    }
}
