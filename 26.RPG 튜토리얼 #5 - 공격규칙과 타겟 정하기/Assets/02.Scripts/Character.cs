using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract를 통한 추상 클래스 사용
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;
    protected Vector2 direction;

    protected Animator myAnimator;
    private Rigidbody2D myRigid2D;

    protected bool isAttacking = false; // 공격 가능여부
    protected Coroutine attackRoutine;

    // 대기, 이동, 공격
    public enum LayerName
    {
        IdleLayer = 0,
        WalkLayer = 1,
        AttackLayer = 2
    }

    // 이동 가능 여부
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // protected는 상속받은 클래스에서만 접근이 가능
    // virtual을 통해서 상속 가능
    protected virtual void Start()
    {
        myRigid2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    // 물리 효과가 적용된(Rigidbody) 오브젝트 조정에 쓰임
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // 객체의 속도 = 방향 * 속도
        myRigid2D.velocity = direction.normalized * speed;
    }

    // 이동, 공격에 따른 Layer변경(WalkLayer, AttackLayer, IdleLayer)
    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer(LayerName.WalkLayer);
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
        }
        else if(isAttacking)
        {
            ActivateLayer(LayerName.AttackLayer); 
        }
        else
        {
            ActivateLayer(LayerName.IdleLayer);
        }
    }

    public void ActivateLayer(LayerName layerName)
    {
        // 모든 레이어의 무게값을 0 으로 만들어 줍니다.
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(1, 0);
        }

        myAnimator.SetLayerWeight((int)layerName, 1);
    }

    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            myAnimator.SetBool("attack", isAttacking);
        }
    }
}
