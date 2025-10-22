using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Jump_Test : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D call;
    private SpriteRenderer sprite;
    private Animator anim;
    float dirx = 0;
    private bool doubleJump = true;

    [Header("Wall Mechanics")]
    [SerializeField] private float wallSlidingSpeed = 2f;
    private bool isWallSliding;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 8f);

    [Header("Movement Mechanics")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float DoubleJumpPower = 12f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { idle, running, jumping, falling, DJumping }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        call = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? DoubleJumpPower : jumpForce);

                doubleJump = !doubleJump;
            }
        }

        WallSlide();
        WallJump();
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
            rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);
    }
    private void UpdateAnimationState()
    {

        MovementState state;
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(call.bounds.center, call.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && dirx != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallSliding = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0;

            if (transform.localScale.x != wallJumpingDirection)
            {
                sprite.flipX = false;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }
}
