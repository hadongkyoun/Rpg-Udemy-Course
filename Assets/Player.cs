using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : Entity
{

    [Header("Move Info")]
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;
    private float xInput;
    private bool doJump;

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



    protected override void Start()
    {
        base.Start();
        
        //플레이어는 Ground 위에서 게임 시작
        isGrounded = true;
        //오른쪽 정면으로 보는 것으로 시작
        facingDir = 1;
        facingRight = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //입력 감지
        CheckInput();
        //애니메이션
        AnimatorControllers();
        FlipController();

        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;


        
    }

    void FixedUpdate()
    {
        //움직임 확인
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
        if (UnityEngine.Input.GetButtonDown("Jump") && isGrounded && !isAttacking)
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

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
            Flip();
        else if(rb.velocity.x < 0 && facingRight)
            Flip();
    }

    public void AttackOver()
    {
        comboCounter++;
        if (comboCounter > 2)
            comboCounter = 0;

        isAttacking = false;
    }



}
