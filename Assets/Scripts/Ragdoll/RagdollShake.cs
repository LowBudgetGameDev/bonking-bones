using UnityEngine;

public class RagdollShake : MonoBehaviour
{
    [SerializeField] private float intensityMultiplier = 1f;
    [SerializeField] private float shakeChance = 0.2f;

    private float speedForMinShake = 5f;
    private float speedForMaxShake = 250f;

    private float minShake = 2.5f;
    private float maxShake = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = collision.relativeVelocity.magnitude;

        if (speed < speedForMinShake) return;

        if (Random.Range(0f, 1f) < shakeChance) CinemachineShake.Instance.ShakeCamera(CalculateShakeAmount(speed) * intensityMultiplier, 0.1f);
    }

    private int CalculateShakeAmount(float speed)
    {
        return Mathf.FloorToInt(Mathf.Lerp(minShake, maxShake, (speed - speedForMinShake) / (speedForMaxShake - speedForMinShake)));
    }
}
