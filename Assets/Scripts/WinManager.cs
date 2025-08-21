using System;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    public event EventHandler OnReachGoal;

    private long winAmount = 20000000;

    private bool hasWon;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ScoreManager.Instance.OnScoreIncrease += (object sender, EventArgs e) =>
        {
            if (hasWon) return;

            if (ScoreManager.Instance.GetScore() >= winAmount)
            {
                hasWon = true;
                OnReachGoal?.Invoke(this, EventArgs.Empty);
            }
        };
    }
}
