using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private SpriteRenderer sr;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 6f;

    private enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Falling
    }

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //player movement
        dirX = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(dirX * movementSpeed, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb2d.velocity = new Vector2(0, jumpForce);
        }
        AnimationUpdateController();
    }

    private void AnimationUpdateController()
    {
        MovementState currentMovementState;
        if (dirX > 0f)
        {
            currentMovementState = MovementState.Running;
            sr.flipX = false;
        }
        else if (dirX < 0f)
        {
            currentMovementState = MovementState.Running;
            sr.flipX = true;
        }
        else
        {
            currentMovementState = MovementState.Idle;
        }

        if (rb2d.velocity.y > 0.1f)
        {
            currentMovementState = MovementState.Jumping;
        }
        else if (rb2d.velocity.y < -0.1f)
        {
            currentMovementState = MovementState.Falling;
        }


        anim.SetInteger("MovementState", (int)currentMovementState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
