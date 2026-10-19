using UnityEngine;

public class CamRotationLock : MonoBehaviour
{
    private Quaternion initialRotation;
    public Transform target; // Reference to the ball's transform
    public float yPos = 50f; // Set this to your desired X position
    public float zPos = -100f; // Set this to your desired Z position
    public float xOffset = 0f; // Optional X offset from the target
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = Quaternion.Euler(0, 0, 0); // Lock rotation to this value
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = initialRotation; // Keep the camera's rotation fixed
        transform.position = new Vector3(target.position.x + xOffset, yPos, zPos); // Ensure the camera stays at the correct Z position
    }

}
