using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Branch : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitBranch();
	}

    // Update is called once per frame
    void Update () {
        // 화면을 벗어난 나뭇가지 제거
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.y < -30) {
            Destroy(gameObject);
        }
    }

    // 나뭇가지 초기화
    void InitBranch () {
        // 나뭇가지 크기
        float sx = Random.Range(0.5f, 1);

        // 나뭇가지 방향
        int x = (Random.Range(0, 2) == 0) ? -1 : 1;     // -1 or 1
        int y = (Random.Range(0, 2) == 0) ? -1 : 1;     // -1 or 1

        // 방향과 크기 설정
        transform.localScale = new Vector3(sx * x, y, 1);
    }
}
