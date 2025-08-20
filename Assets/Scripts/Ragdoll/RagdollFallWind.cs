using UnityEngine;

public class RagdollFallWind : MonoBehaviour
{
    [SerializeField] private Rigidbody2D torsoRigidbody2D;

    private AudioSource fallWindAudioSource;

    private float speedForMinSound = 5f;
    private float speedForMaxSound = 100f;

    private float minVolume = 0.5f;
    private float maxVolume = 1.0f;

    private void Awake()
    {
        fallWindAudioSource = GetComponent<AudioSource>();

        fallWindAudioSource.volume = 0f;
    }

    private void FixedUpdate()
    {
        float speed = torsoRigidbody2D.linearVelocity.magnitude;

        if (speed < speedForMinSound)
        {
            fallWindAudioSource.volume = 0f;
            return;
        }

        speed = Mathf.Clamp(speed, speedForMinSound, speedForMaxSound);

        // Simple lerp for volume as ragdoll goes faster. Might change it later on to sound better or not.
        fallWindAudioSource.volume = Mathf.Lerp(minVolume, maxVolume, (speed - speedForMinSound) / (speedForMaxSound - speedForMinSound));
    }
}
