using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    Animator anim;                  // Animator
    Transform chkPoint;             // Check Point

    float moveSpeed = 8f;           // 이동 속도
    float jumpSpeed = 12f;          // 점프 속도
    float gravity = 19f;            // 중력

    Vector3 moveDir;                // 이동 방향
    bool isDead = false;            // 사망?

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        chkPoint = transform.Find("CheckPoint");
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        CheckBranch();      // 나뭇가지 조사
        MoveOwl();          // 올빼미 이동
    }

    // 올빼미 이동
    void MoveOwl()
    {
        // 올빼미가 화면 아래를 벗어났나?
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.y < -100)
        {
            isDead = true;
            return;
        }
        // 키 입력
        float keyValue = Input.GetAxis("Horizontal");

        // 화면의 가장자리인지 조사
        if ((keyValue < 0 && pos.x < 40) ||
            (keyValue > 0 && pos.x > Screen.width - 40))
        {
            keyValue = 0;
        }
        moveDir.x = keyValue * moveSpeed;

        // 중력
        moveDir.y -= gravity * Time.deltaTime;

        // 이동
        transform.Translate(moveDir * Time.deltaTime);

        // 올빼미 애니메이션
        anim.SetFloat("velocity", moveDir.y);
    }

    // 나뭇가지 판정
    void CheckBranch()
    {
        // CheckPoint에서 아래쪽으로 0.2m 이내 조사
        RaycastHit2D hit = Physics2D.Raycast(chkPoint.position, Vector2.down, 0.1f);

        // 디버그 출력
        Debug.DrawRay(chkPoint.position, Vector2.down * 1f, Color.blue);

        // 조사한 물체가 나뭇가지이면 점프 속도 설정
        if (hit.collider != null && hit.collider.tag == "Branch")
        {
            moveDir.y = jumpSpeed;
        }
    }

    // 충돌 판정 및 처리
    void OnTriggerEnter2D(Collider2D coll)
    {
        Transform other = coll.transform;

        switch (other.tag)
        {
            case "Bird":
                other.SendMessage("DropBird");
                break;
            case "Gift":
                other.SendMessage("GetGift");
                break;
        }
    }
}
