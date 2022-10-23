using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        // victor3 is more importans in 3D game and victor2 2D games.
        // victor3 :(x, y, z) and victor2 :(x, y)
        if (Input.GetButtonDown("Jump"))// this code for jump
        {

            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
        UpdateAnimationUpdate();
    }
    private void UpdateAnimationUpdate()
    {
        if (dirX > 0f) // if player walk to the rhit
        {
            anim.SetBool("running", true);
        }
        else if (dirX < 0f) // if player walk to the left
        {

            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
}
