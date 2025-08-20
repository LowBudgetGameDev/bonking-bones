using UnityEngine;

public class RagdollBlood : MonoBehaviour
{
    [SerializeField] private Transform bloodPrefab;
    [SerializeField] private float bloodScale = 0.75f;
    [SerializeField] private float bloodChance = 1f;

    private float speedForMinBlood = 5f;
    private float speedForMaxBlood = 200f;

    private int minBloodAmount = 20;
    private int maxBloodAmount = 75;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = collision.relativeVelocity.magnitude;

        if (speed < speedForMinBlood) return;

        speed = Mathf.Clamp(speed, speedForMinBlood, speedForMaxBlood);

        if (Random.Range(0f, 1f) < bloodChance) SpawnBlood(speed, transform.position);
    }

    private void SpawnBlood(float speed, Vector3 position)
    {
        Transform blood = Instantiate(bloodPrefab, position, Quaternion.identity);

        blood.localScale = Vector3.one * bloodScale;

        ParticleSystem bloodParticles = blood.GetComponent<ParticleSystem>();

        int bloodAmount = CalculateBloodAmount(speed);

        bloodParticles.emission.SetBurst(0, GetBloodBurst(bloodAmount));

        ParticleSystem.MainModule main = bloodParticles.main;

        main.startSpeed = GetBloodSpeeds(bloodAmount);
    }

    private int CalculateBloodAmount(float speed)
    {
        return Mathf.FloorToInt(Mathf.Lerp(minBloodAmount, maxBloodAmount, (speed - speedForMinBlood) / (speedForMaxBlood - speedForMinBlood)));
    }

    private ParticleSystem.Burst GetBloodBurst(int bloodAmount)
    {
        ParticleSystem.Burst burst = new ParticleSystem.Burst();

        burst.time = 0f;
        burst.count = bloodAmount;
        burst.cycleCount = 1;
        burst.repeatInterval = 0.01f;
        burst.probability = 1f;

        return burst;
    }

    private ParticleSystem.MinMaxCurve GetBloodSpeeds(int bloodAmount)
    {
        float defaultMinSpeed = 3f;
        float defaultMaxSpeed = 10f;

        float minSpeed = defaultMinSpeed * bloodAmount / speedForMinBlood;
        float maxSpeed = defaultMaxSpeed * bloodAmount / speedForMinBlood;

        // scale down speeds so they don't explode out as much
        float scaleFactor = 2f;

        minSpeed = Mathf.Clamp(minSpeed / scaleFactor, defaultMinSpeed, Mathf.Infinity);
        maxSpeed = Mathf.Clamp(maxSpeed / scaleFactor, defaultMaxSpeed, Mathf.Infinity);

        return new ParticleSystem.MinMaxCurve(minSpeed, maxSpeed);
    }
}
