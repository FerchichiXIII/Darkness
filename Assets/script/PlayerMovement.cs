using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    // if use serializeField i can update the jump force and speed in the unity .
    // Start is called before the first frame update
    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkSoundEffect;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") && IsGrounded())
        {
            walkSoundEffect.Play();
        }
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        // victor3 is more importans in 3D game and victor2 2D games.
        // victor3 :(x, y, z) and victor2 :(x, y)
        if (Input.GetButtonDown("Jump") && IsGrounded())// this code for jump
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationUpdate();
        
    }
    private void UpdateAnimationUpdate()
    {
         MovementState state;
        if (dirX > 0f) // if player walk to the rhit
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // if player walk to the left
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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround );
    }

}
