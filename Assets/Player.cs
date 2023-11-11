using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Rigidbody2D�� �������� ���� ��⸦ �غ��ϴ� ��
    //Unity Inspector���� ����, Ȥ�� �ڵ�� ����
    //public Rigidbody2D rb = this.GetComponent<Rigidbody2D>(); 
    private Rigidbody2D rb;

    //Control Speed
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;
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
