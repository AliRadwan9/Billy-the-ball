using UnityEngine;

public class BounceAndDribble : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float acceleration = 7f;
    public float bounceForce = 10f;

    public float fallGravityScale = 4.7f;
    public float riseGravityScale = 3f;
    public float dribbleGravityScale = 6f; 
    public float dribblingBounceForce = 20f;
    public Vector3 respawnPoint = new Vector3(-83, 2, 0);

    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private bool isDribbling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        horizontalInput = Input.GetAxisRaw("Horizontal");

        float targetSpeed = horizontalInput * moveSpeed;
        float newSpeedX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newSpeedX, rb.linearVelocity.y);


    }
    // Update is called once per frame
    void Update(){
    
        verticalInput = Input.GetAxis("Vertical");

        if(verticalInput < 0){
            rb.gravityScale = dribbleGravityScale;
            isDribbling = true;
        }
        else{
            isDribbling = false;
            if(rb.linearVelocity.y < 0 ){
            rb.gravityScale = fallGravityScale;
            }
            else{
            rb.gravityScale = riseGravityScale;
            }
        }   
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            Bounce();
        }
    }
    public void Bounce(){
        // Reset vertical velocity before applying bounce force to make bounce consistent regardless of current vertical speed
        if(isDribbling){
            rb.AddForce(Vector2.up * dribblingBounceForce, ForceMode2D.Impulse); 
        }
        else{
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); 
        }
    }
    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Spikes")){
            rb.linearVelocity = Vector2.zero;
            transform.position = respawnPoint;
        }
    }
}
