using System;
using Unity.Cinemachine;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    public static RagdollSpawner Instance { get; private set; }

    public event EventHandler OnRagdollSpawned;

    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private CinemachineCamera followCamera;

    private Vector2 spawnPoint = new Vector2(0f, 1260f);

    private Transform currentRagdoll;

    private void Awake()
    {
        Instance = this;

        SpawnRagdoll();
    }

    public void SpawnRagdoll()
    {
        if (currentRagdoll != null) Destroy(currentRagdoll.gameObject);

        currentRagdoll = Instantiate(ragdollPrefab, spawnPoint, Quaternion.identity);

        currentRagdoll.GetComponent<RagdollCamera>().SetCameraTarget(followCamera);

        OnRagdollSpawned?.Invoke(this, EventArgs.Empty);
    }
}
