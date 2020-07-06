using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    private Transform target;

    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("target").transform;
    }

    private void FixedUpdate()
    {
        // 타겟의 방향을 계산(타겟의 위치 - 나의 위치)
        Vector2 direction = target.position - transform.position;
        myRigidbody.velocity = direction.normalized * speed;

        // Mathf.Atan2(높이, 밑변) => 각도 산출
        // Mathf.Rad2Deg => 라디안을 각도로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Quaternion.AngleAxis(회전할 각도, 기준이 되는 각도)
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
