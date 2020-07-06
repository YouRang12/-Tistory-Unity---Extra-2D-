using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;


    private float initHealth = 100;
    private float initMana = 50;

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Start()
    {
        // 체력과 마나 초기화(현재 값, 최대 값)
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);

        // base는 상속받은 클래스의 기능을 가리킴
        base.Start();
    }

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Update()
    {
        GetInput();

        // base는 상속받은 클래스의 기능을 가리킴
        base.Update();
    }

    // 키보드 입력값을 받음
    private void GetInput()
    {
        // 체력과 마나를 조절(단축키 I, O)
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        Vector2 moveVector;

        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        direction = moveVector;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAttacking) 
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }

    }

    // 플레이어 공격 애니메이션
    private IEnumerator Attack()
    {
        isAttacking = true;
        myAnimator.SetBool("attack", isAttacking);

        yield return new WaitForSeconds(5);
        StopAttack();

    }
}


