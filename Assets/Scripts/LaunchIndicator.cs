using UnityEngine;
using Unity.Cinemachine;

public class LaunchIndicator : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCamera;
    
    // Multiplier to increase (or decrease) the yaw rotation compared to the camera.
    [SerializeField] private float yawMultiplier = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Get the camera's yaw (horizontal rotation)
        float cameraYaw = freeLookCamera.transform.eulerAngles.y;
        
        // Multiply the yaw by our multiplier
        float adjustedYaw = cameraYaw * yawMultiplier;
        
        // Set the indicator's rotation using the adjusted yaw, while zeroing pitch and roll.
        transform.rotation = Quaternion.Euler(0, adjustedYaw, 0);
    }
}
