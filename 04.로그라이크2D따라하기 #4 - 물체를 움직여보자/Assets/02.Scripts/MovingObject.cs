using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract 추상클래스
public abstract class MovingObject : MonoBehaviour {

    public float moveTime = 0.1f;           // 오브젝트를 움직이게 할 시간단위
    public LayerMask blockingLayer;         // 충돌이 일어났는지 체크할 레이어


    private BoxCollider2D boxCollider;    
    private Rigidbody2D rb2D;              
    private float inverseMoveTime;          // 움직임을 효과적으로 만들 때 사용할 변수


    // 자식 클래스가 덮어써서 재정의 가능 (오버라이드)
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        // 나누기 대신에 계산에 효율적인 곱하기를 사용하려고 쓰임
        // 곱셈이 나누기보다 효율적이다.
        inverseMoveTime = 1f / moveTime;
    }

    // Move는 이동할 수 있으면 true를 반환하고 그렇지 않으면 false를 반환.
    // Move는 x 방향, y 방향, RaycastHit2D에 대해 충돌을 확인하기위한 매개 변수를 취함.
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        // 현재 위치를 저장
        Vector2 start = transform.position;

        // 끝나는 위치를 저장
        Vector2 end = start + new Vector2(xDir, yDir);

        // Ray를 사용할 때 자신과 부딪치기 않기 위해 boxCollider 비활성화
        boxCollider.enabled = false;

        // 시작지점에서 끝지점의 라인을 가져와 blockingLayer와 충돌검사
        hit = Physics2D.Linecast(start, end, blockingLayer);

        // boxCollider 활성화
        boxCollider.enabled = true;

        // 아무것도 맞지 않았으면 이동
        if (hit.transform == null)
        {          
            StartCoroutine(SmoothMovement(end));

            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // 이동할 남은 거리를 계산(현재 위치 - end)
        // sqrMagintude: 벡트 길이 제곱
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        // Epsilon은 0에 가까운 엄청 작은 수
        while (sqrRemainingDistance > float.Epsilon)
        {
            // end에 가까운 위치를 찾음(moveTime 사용)
            // Vector3.MoveTowards: 현재 포인트를 직선상에 목표 포인트로 이동시킴.
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            // 계산된 위치로 이동
            rb2D.MovePosition(newPostion);

            // 이동 후 남은 거리를 다시 계산
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            // while 루프로 다시 돌아가기 전에 다음 프레임을 기다림.
            yield return null;
        }
    }

    // 가상 키워드는 override 키워드를 사용하여 클래스를 상속함으로써 AttemptMove를 재정의 할 수 있음을 의미.
    // 일반형 입력 T는 막혔을 때, 유닛이 반응할 컴포넌트 타입을 가리키기 위해 사용
    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;

        // Move가 성공하면 canMove를 true로 설정하고 실패하면 false를 설정.
        bool canMove = Move(xDir, yDir, out hit);

        // 부딪치지 않으면 이후 코드를 실행하지 않고 리턴
        if (hit.transform == null)
            return;

        // 부딪쳤다면, 충돌한 오브젝트의 컴포넌트의 레퍼런스를  T타입의 컴포넌트에 할당
        T hitComponent = hit.transform.GetComponent<T>();

        // 움직이던 오브젝트가 막혔고, 상호작용할 수 있는 오브젝트와 충돌
        if (!canMove && hitComponent != null)   
            OnCantMove(hitComponent);
    }

    // 일반형(Generic) 입력 T를 T형의 component라는 변수로 받아옴.
    // 추상화 함수
    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}

