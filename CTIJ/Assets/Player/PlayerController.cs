using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float delayBeforeRun = 2f;
    [SerializeField] private float jumpForce = 9f;

    Rigidbody2D rb;
    Animator anim;
    bool canRun = false;
    bool isGrounded = false;

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
        // Running flag for Idle/Run
        anim.SetBool("isRunning", canRun);

        // Jump input – only once when grounded
        if (canRun && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            anim.SetBool("isGrounded", false);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            anim.SetTrigger("jump");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }
}
