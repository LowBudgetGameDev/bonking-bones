using UnityEngine;

public class RagdollReset : MonoBehaviour
{
    [SerializeField] private Rigidbody2D torsoRigidbody2D;

    private RagdollLaunch ragdollLaunch;

    private float restTimer;
    private float restTimerMax = 0.5f;

    private float maxRestSpeed = 0.1f;

    private void Awake()
    {
        ragdollLaunch = GetComponent<RagdollLaunch>();

        restTimer = restTimerMax;
    }

    private void Update()
    {
        if (!ragdollLaunch.GetHasLaunched()) return;

        restTimer -= Time.deltaTime;

        if (torsoRigidbody2D.linearVelocity.magnitude > maxRestSpeed)
        {
            restTimer = restTimerMax;
        }

        if (restTimer < 0f)
        {
            RagdollSpawner.Instance.SpawnRagdoll();

            restTimer = restTimerMax;
        }
    }
}
