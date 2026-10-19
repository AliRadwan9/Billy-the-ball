using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    
    [Header("Follow Settings")]
    public float smoothTime = 0.3f; // Approximate time to reach target
    public Vector2 deadZoneSize = new Vector2(2f, 2f); // The area where the ball can move freely
    public Vector3 offset = new Vector3(0, 2, -10);

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate the target's position relative to the camera
        Vector3 targetPos = target.position + offset;
        Vector3 currentPos = transform.position;

        // 2. Only move if the target has moved outside the "Dead Zone"
        Vector3 desiredPosition = currentPos;

        // Horizontal Check
        if (Mathf.Abs(targetPos.x - currentPos.x) > deadZoneSize.x)
        {
            desiredPosition.x = targetPos.x;
        }

        // Vertical Check (Your Y-Threshold)
        if (Mathf.Abs(targetPos.y - currentPos.y) > deadZoneSize.y)
        {
            desiredPosition.y = targetPos.y;
        }

        // 3. Smoothly move to that desired position
        
        transform.position = Vector3.SmoothDamp(currentPos, desiredPosition, ref velocity, smoothTime);
    }

    // This helps to see the Dead Zone in the Unity Editor Scene View
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - offset, new Vector3(deadZoneSize.x * 2, deadZoneSize.y * 2, 0));
    }
}