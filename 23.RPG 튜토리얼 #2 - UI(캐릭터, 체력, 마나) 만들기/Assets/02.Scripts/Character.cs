using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract를 통한 추상 클래스 사용
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    protected Vector2 direction;
    private Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // protected는 상속받은 클래스에서만 접근이 가능
    // virtual을 통해서 상속 가능
    protected virtual void Update()
    {
        Move();
    }

    // 캐릭터를 이동시킴(방향, 스피드, Time.deltaTime)
    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement();  //WALK
        }
        else
        {
            animator.SetLayerWeight(1, 0);  // IDLE
        }
    }

    // 파라미터 값에 따른 애니메이션 변환
    public void AnimateMovement()
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

}
