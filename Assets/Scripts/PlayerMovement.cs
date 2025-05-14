using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isMoving;
    private bool isJumping;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Set velocity based on input
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Update animator
        animator.SetFloat("MoveX", horizontalInput);
        isMoving = horizontalInput != 0;
        animator.SetBool("isMoving", isMoving);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            animator.SetBool("isJumping", isJumping);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Reset isJumping when landing
        if (isGrounded && isJumping)
        {
            isJumping = false;
            //might want to set the bool to isWalking(?) instead
            animator.SetBool("isJumping", isJumping);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            // Set a parameter to differentiate attacks
            if (mousePos.x < Screen.width / 2)
            {
                animator.SetInteger("AttackType", 0); // Left attack
            }
            else
            {
                animator.SetInteger("AttackType", 1); // Right attack
            }

            animator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

