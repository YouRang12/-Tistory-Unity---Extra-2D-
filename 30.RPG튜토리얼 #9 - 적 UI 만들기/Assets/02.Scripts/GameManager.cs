using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    private NPC currentTarget;

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
                if (currentTarget != null)
                {
                    // 타겟 해제
                    currentTarget.DeSelect();
                }

                // 변경된 타겟의 정보 적용
                currentTarget = hit.collider.GetComponent<NPC>();

                player.myTarget = currentTarget.Select();

                // 적을 클릭하면 적 캐릭터 UI 활성화
                UIManager.MyInstance.ShowTargetFrame(currentTarget);
            }
            else
            {
                // 적이 아닌 다른 곳을 클릭하면 적 캐릭터 UI 비활성화
                UIManager.MyInstance.HideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = null;
                player.myTarget = null;
            }
        }
    }
}
