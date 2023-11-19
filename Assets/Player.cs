using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;


    //Control
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private float dashTimer;
    private float dashCooldownTimer;

    [Header("Attack Info")]
    [SerializeField]private float comboTime;
    [SerializeField] private float attackMoveSpeed;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;


    //Input Value
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;
    private bool doJump;


    //Animator Control
    private Animator anim;

    //렌더링
    private SpriteRenderer playerRender;


    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded = true;



    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //입력 감지
        CheckInput();
        //애니메이션
        AnimatorControllers();
        FlipController();

        //충돌 감지
        CollisionChecks();


        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;


        
    }

    void FixedUpdate()
    {
        //이동 확인
        Movement();
        //점프
        Jump();
    }

    private void Movement()
    {
        if (isAttacking)
        {
            if (dashTimer > 0)
            {
                //공격 캔슬 가능한 대쉬 구현
                isAttacking = false;
                rb.velocity = new Vector2(facingDir * dashSpeed, 0);
            }
            else
                //임의로 공격시 이동하는 속도 추가
                rb.velocity = new Vector2(facingDir * attackMoveSpeed, 0);
        }
        else if (dashTimer > 0)
        {
            //공중에서 대쉬가 일직선으로 가게끔 y속도 0 설정
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        }
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

        //Press Dash Button
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        //Press Attack Button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();

        }

    }

    private void StartAttackEvent()
    {
       
        if (!isGrounded)
            return;
        
        isAttacking = true;
        if (comboTimeWindow < 0)
            comboCounter = 0;
        comboTimeWindow = comboTime;
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
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);

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

    public void AttackOver()
    {
        comboCounter++;
        if (comboCounter > 2)
            comboCounter = 0;

        isAttacking = false;
    }


    //// 라인으로 그라운드 체크
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));    
    }


}
