using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Update()
    {
        //Executes the GetInput function
        GetInput();

        // base는 상속받은 클래스의 기능을 가리킴
        base.Update();
    }

    // 키보드 입력값을 받음
    private void GetInput()
    {
        Vector2 moveVector;

        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;
    }
}
