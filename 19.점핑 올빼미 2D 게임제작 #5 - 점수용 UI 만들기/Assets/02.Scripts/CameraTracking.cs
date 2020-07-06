using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {

    Transform target;   // Tracking 대상
    float height;       // Target의 최대 높이

	// Use this for initialization
	void Start () {
        // 올빼미와 최대 높이
        target = GameObject.Find("Owl").transform;
        height = target.position.y;
	}
	
	// Late Update
	void LateUpdate () {
        // Target의 높이
        float ty = target.position.y;
        if (ty <= height) return;

        // 목표값까지 보간
        float cy = transform.position.y;
        cy = Mathf.Lerp(cy, ty, 5 * Time.deltaTime);

        // 카메라 높이 조정
        Vector3 pos = transform.position;
        pos.y = cy - 0.1f;
        transform.position = pos;

        // 최대 높이 갱신
        height = ty;
    }
}
