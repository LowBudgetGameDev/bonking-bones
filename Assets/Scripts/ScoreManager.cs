using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void DecreaseScore(int amount)
    {
        score -= amount; 
    }

    public int GetScore()
    {
        return score;
    }
}
