using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;


    //Control Speed
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private float dashTimer;
    private float dashCooldownTimer;


    //Input Value
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;
    private bool doJump;


    //Animator Control
    private Animator anim;


    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded = true;



    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�Է� ����
        CheckInput();
        //�ִϸ��̼�
        AnimatorControllers();
        FlipController();

        //�浹 ����
        CollisionChecks();


        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        
    }

    void FixedUpdate()
    {
        //��,�� �̵�
        Movement();
        //����
        Jump();
    }
    private void Movement()
    {
        if(dashTimer > 0)
            //���߿��� �뽬�� ���������� ���Բ� y�ӵ� 0 ����
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        else
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        //Press Jump Button
        if (UnityEngine.Input.GetButtonDown("Jump") && isGrounded)
        {
            doJump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0)
        {
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
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
        anim.SetFloat("yVelocity", rb.velocity.y);

        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);

        anim.SetBool("isDashing", dashTimer>0);
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

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    //// �������� �׶��� üũ
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));    
    //}


}
