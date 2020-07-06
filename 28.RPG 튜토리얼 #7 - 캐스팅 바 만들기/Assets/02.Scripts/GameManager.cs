using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        // 마우스 좌클릭 && 마우스의 위치가 UI 위에 있는지 체크
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스의 현재 위치를 월드좌표(Vector3)로 반환해줌
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mathf.Infinity는 무한대를 뜻함
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero, Mathf.Infinity, 512);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    player.myTarget = hit.transform.GetChild(0);
                }
            }
            else
            {
                player.myTarget = null;
            }
        }
    }
}
