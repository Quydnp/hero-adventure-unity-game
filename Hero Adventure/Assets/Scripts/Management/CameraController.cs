using Unity.Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera = Object.FindFirstObjectByType<CinemachineCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
