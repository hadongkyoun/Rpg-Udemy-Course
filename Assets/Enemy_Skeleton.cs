using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Entity
{

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D isPlayerDetected;

    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        facingDir = -1;
        facingRight = false;
        isAttacking = false;
    }

    protected override void Update()
    {
        base.Update();

        //바닥이 없을때 Flip
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //플레이어 감지했을때
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                //거리가 1 이상이면 걷기
                rb.velocity = new Vector2(moveSpeed * 2.0f * facingDir, rb.velocity.y);

                Debug.Log("I see the player");
                isAttacking = false;
            }
            else
            {
                isAttacking = true;
            }
        }
        else
            Movement();
    }

    private void Movement()
    {

         //평시 상태
         rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    
    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));

    //}
}
