using UnityEngine;

public class RagdollReset : MonoBehaviour
{
    [SerializeField] private Rigidbody2D torsoRigidbody2D;

    private RagdollLaunch ragdollLaunch;

    private float restTimer;
    private float restTimerMax = 3f;

    private float maxRestSpeed;
    private float minMaxRestSpeed = 0.05f;

    private void Awake()
    {
        ragdollLaunch = GetComponent<RagdollLaunch>();

        restTimer = restTimerMax;
        maxRestSpeed = 0f;
    }

    private void Update()
    {
        if (!ragdollLaunch.GetHasLaunched()) return;

        if (maxRestSpeed == 0f)
        {
            maxRestSpeed = ragdollLaunch.GetThrowStrength() / 100f; // Takes 1% of the max launch strength so the rest speed will be the same no matter how hard you throw

            maxRestSpeed = Mathf.Clamp(maxRestSpeed, minMaxRestSpeed, Mathf.Infinity);
        }

        restTimer -= Time.deltaTime;

        if (torsoRigidbody2D.linearVelocity.magnitude > maxRestSpeed)
        {
            restTimer = restTimerMax;
        }

        if (restTimer < 0f)
        {
            RagdollSpawner.Instance.SpawnRagdoll();
        }
    }
}
