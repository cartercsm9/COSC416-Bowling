using System.Runtime.CompilerServices;
using UnityEngine;

public class Gutter : MonoBehaviour
{
    // Choose the axis on which the ball should be centered.
    // Only one of these should be true.
    public bool lockX = true;
    public bool lockZ = false;
    
    // The direction along which the ball will be pushed once centered.
    // For example, if the gutter runs along the Z-axis, you might use Vector3.forward.
    public Vector3 desiredDirection = Vector3.forward;
    
    // Minimum speed to ensure the ball keeps moving.
    public float minSpeed = 10f;
    
    // How far the ball can be off-center before snapping to the gutter's center.
    public float alignmentThreshold = 0.2f;
    
    private void OnTriggerEnter(Collider triggeredBody)
    {
        Debug.Log("Trigger activated by: " + triggeredBody.gameObject.name);
        
        Rigidbody ballRigidBody = triggeredBody.GetComponent<Rigidbody>();
        if (ballRigidBody == null)
        {
            Debug.LogWarning("No Rigidbody attached to " + triggeredBody.gameObject.name);
            return;
        }
        
        // Get the current ball position and the gutter's center (from this object's position)
        Vector3 ballPos = ballRigidBody.transform.position;
        Vector3 gutterCenter = transform.position;
        
        // Check alignment on the chosen axis and snap to center if off by more than threshold.
        if (lockX)
        {
            float errorX = ballPos.x - gutterCenter.x;
            if (Mathf.Abs(errorX) > alignmentThreshold)
            {
                ballPos.x = gutterCenter.x;
            }
        }
        if (lockZ)
        {
            float errorZ = ballPos.z - gutterCenter.z;
            if (Mathf.Abs(errorZ) > alignmentThreshold)
            {
                ballPos.z = gutterCenter.z;
            }
        }
        
        ballRigidBody.transform.position = ballPos;
        Debug.Log("Ball repositioned to: " + ballRigidBody.transform.position);
        
        // Capture the ball's speed before zeroing its velocity.
        float velocityMagnitude = ballRigidBody.linearVelocity.magnitude;
        // Ensure a minimum speed if needed.
        velocityMagnitude = Mathf.Max(velocityMagnitude, minSpeed);
        
        // Zero out the current velocities.
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        
        // Apply force along the desired direction with the captured speed.
        Debug.Log("Applying force in direction: " + desiredDirection.normalized + " with speed: " + velocityMagnitude);
        ballRigidBody.AddForce(desiredDirection.normalized * velocityMagnitude, ForceMode.VelocityChange);
    }
}
