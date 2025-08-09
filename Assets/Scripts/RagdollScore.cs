using UnityEngine;

public class RagdollScore : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier = 1;

    private int minForceForScore = 5;
    private int maxForceForScore = 50;

    private int maxForceMultiplier = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude < minForceForScore) return;

        float relativeForce = collision.relativeVelocity.magnitude;
        relativeForce = Mathf.Clamp(relativeForce, minForceForScore, maxForceForScore);

        // This equation was found to linearly increase score until max force
        float score = (maxForceMultiplier - 1) * relativeForce / (maxForceForScore - minForceForScore);

        ScoreManager.Instance.IncreaseScore((int) score * scoreMultiplier);
    }
}
