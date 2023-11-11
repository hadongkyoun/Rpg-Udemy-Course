using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Rigidbody2D�� �������� ���� ��⸦ �غ��ϴ� ��
    //Unity Inspector���� ����, Ȥ�� �ڵ�� ����
    //public Rigidbody2D rb = this.GetComponent<Rigidbody2D>(); 
    Rigidbody2D rb;

    //Control Speed
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    private float xInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement


        xInput = Input.GetAxisRaw("Horizontal");

        // y���� �ӵ� �״�� ( Nothing Change )
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
