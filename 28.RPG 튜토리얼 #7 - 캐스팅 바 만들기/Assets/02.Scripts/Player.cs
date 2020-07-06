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

    [SerializeField]
    private Transform[] exitPoints; // 발사방향 (위쪽, 오른쪽, 아래, 왼쪽)
    private int exitIndex = 0;

    private SpellBook spellBook; // 스펠의 정보가 담겨있음

    [SerializeField]
    private Block[] blocks; // 벽 (아래, 왼쪽, 위쪽, 오른쪽)

    public Transform myTarget { get; set; }

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();

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
        base.Update(); // base는 상속받은 클래스의 기능을 가리킴
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

        if (moveVector.x != 0 || moveVector.y != 0)
        {
            if (moveVector.y > 0) exitIndex = 0;         // 위쪽
            else if (moveVector.y < 0) exitIndex = 2;    // 아래
            else if (moveVector.x > 0) exitIndex = 1;    // 오른족
            else exitIndex = 3;                          // 왼쪽
        }
    }

    // 플레이어 공격 애니메이션
    private IEnumerator Attack(int spellIndex)
    {
        Spell newSpell = spellBook.CastSpell(spellIndex);

        isAttacking = true;
        myAnimator.SetBool("attack", isAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        GameObject spell = newSpell.MySpellPrefab;
        Vector3 exitPosition = exitPoints[exitIndex].position;
        Quaternion exitQuaternion = Quaternion.identity;

        SpellScript s = Instantiate(spell, exitPosition, exitQuaternion).GetComponent<SpellScript>();
        s.MyTarget = myTarget;

        StopAttack();
    }

    // 스펠 생성(fire, ice, lightning)
    public void CastSpell(int spellIndex)
    {
        Block(); // 블럭 생성
        if (myTarget == null) return;
        // 공격중x, 이동중x, 발사가능하면 스펠 발사
        if (!isAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    // 스펠 발사가능여부 확인
    private bool InLineOfSight()
    {
        Vector3 targetDirection = (myTarget.transform.position - transform.position).normalized;

        // Physics2D.Raycast(Raycast 발사위치, Raycast 발사방향, Raycast 발사거리, 체크할 레이어 번호)
        RaycastHit2D hit =
            Physics2D.Raycast
            (
                transform.position,
                targetDirection,
                Vector2.Distance(transform.position, myTarget.position),
                256
            );
        if (hit.collider == null) return true;
  
        return false;
    }

    // 블럭(Triiger 체크용)
    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    }

    // 공격 중지
    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}


