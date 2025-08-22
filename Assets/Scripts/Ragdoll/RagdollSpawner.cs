using System;
using Unity.Cinemachine;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    public static RagdollSpawner Instance { get; private set; }

    public event EventHandler OnRagdollSpawned;
    public event EventHandler OnRagdollInAirTooLong;

    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private CinemachineCamera followCamera;

    private Vector2 spawnPoint = new Vector2(0f, 1260f);

    private Transform currentRagdoll;

    private float resetTimerMax = 60f;
    private float resetTimer;
    private bool hasLaunched;

    private void Awake()
    {
        Instance = this;

        SpawnRagdoll();
    }

    // This logic here is used to determine if the ragdoll has been alive for too long
    private void Update()
    {
        if (!hasLaunched) return;

        resetTimer -= Time.deltaTime;

        if (resetTimer < 0f)
        {
            OnRagdollInAirTooLong?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SpawnRagdoll()
    {
        if (currentRagdoll != null) Destroy(currentRagdoll.gameObject);

        currentRagdoll = Instantiate(ragdollPrefab, spawnPoint, Quaternion.identity);

        currentRagdoll.GetComponent<RagdollCamera>().SetCameraTarget(followCamera);

        currentRagdoll.GetComponent<RagdollLaunch>().OnLaunch += (object sender, EventArgs e) =>
        {
            resetTimer = resetTimerMax;
            hasLaunched = true;
        };

        hasLaunched = false;

        OnRagdollSpawned?.Invoke(this, EventArgs.Empty);
    }
}
