using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;


    //Control Speed
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;
    private bool doJump;

    //Input Value
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;

    //Animator Control
    private Animator anim;
 

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //입력 감지
        CheckInput();
        //애니메이션
        AnimatorControllers();
        FlipController();
    }

    void FixedUpdate()
    {
        //좌,우 이동
        Movement();
        //점프
        Jump();
    }
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        //Press Jump Button
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            doJump = true;
        }
    }

    private void Jump()
    {
        if (doJump)
        {
            doJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }

    private void Flip()
    {
        //Switcher
        facingDir = facingDir * -1;
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
            Flip();
        else if(rb.velocity.x < 0 && facingRight)
            Flip();
    }
}
