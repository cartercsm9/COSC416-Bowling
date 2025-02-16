using UnityEngine;
using Unity.Cinemachine;

public class LaunchIndicator : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCamera;
    
    // You can expose this to adjust the offset if needed.
    [SerializeField] private float yOffset = -20f;

    // Update is called once per frame
    void Update()
    {
        // Match the camera's forward vector
        transform.forward = freeLookCamera.transform.forward;
        // Then reset pitch and roll while applying a -20 degree offset to yaw
        float adjustedY = transform.rotation.eulerAngles.y + yOffset;
        transform.rotation = Quaternion.Euler(0, adjustedY, 0);
    }
}
