using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // 값 입력을 통해 유니티 속 Inspector에 접근 가능한 모습 ( 값이 변화 함 )
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


        // Input GetKeyCode 방법
        if (Input.GetKey(KeyCode.Space))
        {
            // GetKeyDown,GetKeyUp이 1번씩 실행 될 것이다
            Debug.Log("You holding the Space button!");
        }


            //빠르게 KeyDown 해도, GetKey는 여러번 실행 된다
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("You pressed Space button!");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("You released Space button!");
        }

        // Input GetButton 방법
        if (Input.GetButtonDown("Jump"))
        {
            //Unity Edit Input Manager에 Jump Button이 정의 돼 있다
            Debug.Log("Jump");
        }


        //수평에서의 키 입력 감지 A,D or Left, Right
        //-1, 0, 1 반환 ( 시원하고 즉각적인 컨트롤 가능 )

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        xInput = Input.GetAxisRaw("Horizontal");

        //-1 ~ 0 ~ 1 반환
        //Debug.Log(Input.GetAxis("Horizontal"));
    }
}
