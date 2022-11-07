using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Values For Player")]
    //the most appropriate values follow 8 for velocity and 13 for force with 2.5 gravity scale in rb2D
    [SerializeField] private float velocity;
    [SerializeField] private float force;

    [Header("References")]
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollide;

    //specify which direction player will turn
    private float directMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        boxCollide = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        jump();
        
    }

    private void FixedUpdate()
    {
        directMove = Input.GetAxisRaw("Horizontal");

        //Movement of player
        rb.velocity = new Vector2 (directMove * velocity, rb.velocity.y);
        animator.SetBool("isWalk", directMove != 0);

        //Player jump 
        /* There are some errors
         * when i try to use rigidbody2D in FixedUpdate so im still trying to find the way solving it
         */
        //jump();
        
        flipObject();


    }

    private void jump()
    {
        animator.SetBool("isJump", !isGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = force * Vector2.up;
            animator.SetTrigger("jump");
        }
    }


    //adjust the exact direction when we turn player 
    private void flipObject()
    {
        if (directMove > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directMove < 0)
        {
            transform.localScale = new Vector3(-1.5f, transform.localScale.y, transform.localScale.z);
        }
    }

    //Use to create a box collide for player
    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollide.bounds.center, boxCollide.bounds.size, 0, Vector2.down, .1f, groundMask);
        return hit.collider != null;
    }
}
