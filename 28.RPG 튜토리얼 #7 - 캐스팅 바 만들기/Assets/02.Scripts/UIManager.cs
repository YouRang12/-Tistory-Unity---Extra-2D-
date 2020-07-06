using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    [SerializeField]
    private Button[] actionButtons;
    private KeyCode action1, action2, action3;

    void Start()
    {
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
    }

    void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
    }

    // 버튼의 OnClick 이벤트를 호출함
    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }
}
