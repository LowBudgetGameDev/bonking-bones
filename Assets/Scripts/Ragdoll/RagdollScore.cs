using UnityEngine;

public class RagdollScore : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier = 1;

    private int minSpeedForScore = 5;
    private int referenceSpeed = 50;

    private int referenceSpeedScore = 25;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = collision.relativeVelocity.magnitude;

        if (speed < minSpeedForScore) return;

        ScoreManager.Instance.IncreaseScore(CalculateScore(speed) * scoreMultiplier * GetScoreMultiplier());
    }

    // Not gonna lie this equation came from ChatGPT
    // However the main meaning behind this is to get the score to increase non linearly for especially larger speeds
    private float CalculateScore(float speed)
    {
        float power = 2.5f; // Faster than quadratic growth

        // Get powered versions of the speeds to get a faster increase in score for faster speeds
        float poweredSpeed = Mathf.Pow(speed, power);
        float poweredMinSpeed = Mathf.Pow(minSpeedForScore, power);

        // Using the reference speed, get a scaling constant which makes the equation more stable for smaller values rather than growing out of control right away
        float poweredReferenceSpeed = Mathf.Pow(referenceSpeed, power);
        float scoreScale = (referenceSpeedScore - 1) / (poweredReferenceSpeed - poweredMinSpeed);

        // Add 1 to give a minimum score of 1 for any hit above the minimum
        return 1 + scoreScale * (poweredSpeed - poweredMinSpeed);
    }

    private float GetScoreMultiplier()
    {
        return UpgradeManager.Instance.GetUpgradeValue(Upgrade.ScoreMultiplier);
    }
}
