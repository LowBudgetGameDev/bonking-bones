using Unity.Cinemachine;
using UnityEngine;

public class RagdollCamera : MonoBehaviour
{
    [SerializeField] private Transform torsoTransform;

    public void SetCameraTarget(CinemachineCamera camera)
    {
        camera.Target.TrackingTarget = torsoTransform;
    }
}
