using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]
public class DragAndShoot : MonoBehaviour
{
    [Header("Settings")]
    public float power = 10f;
    public float maxDragDistance = 5f;
    public float stopThreshold = 0.2f; 
    public float stopDelay = 0.8f;    
    public Vector3 respawnPoint = new Vector3(-57, -25, -8); 
    public float outOfBoundsY = -40f; 

    [Header("Gravity Tweaks")]
    public float fallGravityScale = 4.7f;
    public float riseGravityScale = 3f;

    [Header("Visuals")]
    private LineRenderer lineVisual;

    private Rigidbody2D rb;
    private Vector2 dragStartPos;
    private bool isGrounded = false;
    private float stopTimer = 0f;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineVisual = GetComponent<LineRenderer>();
        
        rb.angularDamping = 2f; 
        
        // Ensure the line is hidden at the start
        lineVisual.enabled = false;
        // Make sure the line uses world space so it doesn't move weirdly with rotation
        lineVisual.useWorldSpace = true;
    }

    void Update()
    {
        HandleInput();
        HandlePhysicsTweaks();
        HandleReset();
    }

    void HandleInput()
    {
        // 1. START DRAG: Click anywhere on screen
        if (Input.GetMouseButtonDown(0))
        {
            if (rb.linearVelocity.magnitude < stopThreshold)
            {
                dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
                lineVisual.enabled = true;
            }
        }

        // 2. DURING DRAG: Draw the visual line and handle release
        if (isDragging)
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Calculate direction based on slingshot logic (Start - Current)
            Vector2 launchDirection = dragStartPos - currentMousePos;
            launchDirection = Vector2.ClampMagnitude(launchDirection, maxDragDistance);

            // Update Line Positions
            // Point 0 is the ball, Point 1 is the aim direction
            lineVisual.SetPosition(0, transform.position);
            lineVisual.SetPosition(1, (Vector2)transform.position + launchDirection);

            // 3. RELEASE: Shoot the ball
            if (Input.GetMouseButtonUp(0))
            {
                if (launchDirection.magnitude > 0.3f)
                {
                    Shoot(launchDirection);
                }

                isDragging = false;
                lineVisual.enabled = false;
            }
        }
    }

    void HandlePhysicsTweaks()
    {
        if (isGrounded && rb.linearVelocity.magnitude < 1.0f)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopDelay)
            {
                StopBall();
            }
        }
        else
        {
            stopTimer = 0f;
        }

        rb.gravityScale = (rb.linearVelocity.y < -0.1f) ? fallGravityScale : riseGravityScale;
    }

    void HandleReset()
    {
        if (transform.position.y < outOfBoundsY)
        {
            transform.position = respawnPoint;
            StopBall();
        }
    }

    void Shoot(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero; 
        rb.AddForce(direction * power, ForceMode2D.Impulse);
        stopTimer = 0f;
    }

    void StopBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        stopTimer = 0f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}