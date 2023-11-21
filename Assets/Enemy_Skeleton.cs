using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Entity
{

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;


    protected override void Start()
    {
        base.Start();
        facingDir = 1;
        facingRight = true;
    }

    protected override void Update()
    {
        base.Update();

        //바닥이 없을때 Flip
        if (!isGrounded || isWallDetected )
        {
            Flip();
        }

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }
}
