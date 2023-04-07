using UnityEngine;

public class MoveLRRH : MonoBehaviour
{
        public float moveSpeed;
        public float jumpForce;

        private bool isJumping;
        private bool isGrounded;

        public Transform groundCheckleft;
        public Transform groundCheckRight;

        public Rigidbody2D rb;
        public Animator animator;
        public SpriteRenderer spriteRenderer;

        private Vector3 velocity = Vector3.zero;
        private float horizontalMovement;

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckleft.position, groundCheckRight.position);

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
         //   isJumping = true;
        //}

        MovePlayer(horizontalMovement);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
        {
            if (_velocity > 0.1f)
            {
                spriteRenderer.flipX = false;
            }else if(_velocity < -0.1f)
            {
                spriteRenderer.flipX = true;
            }
        }
}
