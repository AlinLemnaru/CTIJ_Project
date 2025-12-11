using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float delayBeforeRun = 2f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private float maxSlideTime = 3f;

    Rigidbody2D rb;
    Animator anim;
    Vector2 pos;

    bool canRun = false;
    bool isGrounded = false;
    bool isSliding = false;
    float slideTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Invoke(nameof(StartRunning), delayBeforeRun);
    }

    void StartRunning()
    {
        canRun = true;
        anim.SetBool("isIdle", false);
    }

    void Update()
    {
        // Keep player x position constant 
        pos = rb.position;
        pos.x = -5.0f;
        rb.position = pos;

        // 1. Ground info from animator (optional, we also track isGrounded bool)
        bool grounded = isGrounded;

        // 2. Slide input
        // Start slide
        if (!isSliding && Input.GetKey(KeyCode.LeftControl))
        {
            isSliding = true;
            slideTimer = 0f;
            anim.SetBool("isSliding", true);
        }

        // While sliding
        if (isSliding)
        {
            slideTimer += Time.deltaTime;

            // End slide after max time or if key released
            if (!Input.GetKey(KeyCode.LeftControl) || slideTimer >= maxSlideTime)
            {
                isSliding = false;
                anim.SetBool("isSliding", false);
            }
        }

        // 3. Running flag for Idle/Run (do not run while sliding)
        bool shouldRun = canRun && grounded && !isSliding;
        anim.SetBool("isRunning", shouldRun);

        // 4. Jump input – only once when grounded and not sliding
        if (shouldRun && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            anim.SetBool("isGrounded", false);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            anim.SetTrigger("jump");
        }
    }

    // Detect ground collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }
}
