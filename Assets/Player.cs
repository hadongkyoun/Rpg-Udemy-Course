using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // �� �Է��� ���� ����Ƽ �� Inspector�� ���� ������ ��� ( ���� ��ȭ �� )
    public float xInput;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start was called.");
    }

    // Update is called once per frame
    void Update()
    {

        // Check How many times play Update Method in Unity.
        Debug.Log("Update is called!");


        // Input GetKeyCode ���
        if (Input.GetKey(KeyCode.Space))
        {
            // GetKeyDown,GetKeyUp�� 1���� ���� �� ���̴�
            Debug.Log("You holding the Space button!");
        }


            //������ KeyDown �ص�, GetKey�� ������ ���� �ȴ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("You pressed Space button!");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("You released Space button!");
        }

        // Input GetButton ���
        if (Input.GetButtonDown("Jump"))
        {
            //Unity Edit Input Manager�� Jump Button�� ���� �� �ִ�
            Debug.Log("Jump");
        }


        //���򿡼��� Ű �Է� ���� A,D or Left, Right
        //-1, 0, 1 ��ȯ ( �ÿ��ϰ� �ﰢ���� ��Ʈ�� ���� )

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        xInput = Input.GetAxisRaw("Horizontal");

        //-1 ~ 0 ~ 1 ��ȯ
        //Debug.Log(Input.GetAxis("Horizontal"));
    }
}
