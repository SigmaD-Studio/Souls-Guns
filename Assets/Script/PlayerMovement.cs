using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f; // Speed multiplier for dashing
    public float dashDistance = 5f; // Distance to dash forward
    public float dashCooldown = 1f; // Cooldown time between dashes
    public float dashDuration = 0.3f; // Duration of the dash in seconds
    public bool isImmuneToDamage = false; // Flag for immunity to damage during dash

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canDash = true;
    private bool isDashing = false;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Collider2D playerCollider; // Reference to the player's collider
    private Animator ani;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        playerCollider = GetComponent<Collider2D>(); // Assuming the collider is on the same GameObject
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Dash();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        // Trigger dash on Space key press
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void Move()
    {
        // Regular movement
        rb.velocity = movement * moveSpeed;
        float speed = rb.velocity.magnitude;
        ani.SetFloat("Running", speed);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        ani.SetTrigger("Dash");

        // Disable trigger collider during dash
        playerCollider.isTrigger = true;

        // Calculate target position for dash
        Vector2 targetPosition = rb.position + movement.normalized * dashDistance;

        // Move towards the target position over the dash duration
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            rb.position = Vector2.MoveTowards(rb.position, targetPosition, dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable trigger collider after dash ends
        playerCollider.isTrigger = false;

        // Disable immunity and reset velocity after dash ends
        isImmuneToDamage = false;
        rb.velocity = Vector2.zero;

        // Wait for dash cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        isDashing = false;
        

    }
    void endDash() 
    {
        ani.SetTrigger("EndDash");
    }
}
