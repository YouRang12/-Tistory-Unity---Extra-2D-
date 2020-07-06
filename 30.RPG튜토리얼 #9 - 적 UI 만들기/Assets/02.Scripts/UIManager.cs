using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // 싱글턴 패턴 => 각 클래스마다 인스턴스화 하지 않아서 효율적
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Button[] actionButtons;
    private KeyCode action1, action2, action3;

    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;

    [SerializeField]
    private Image portraitFrame;

    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();

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

    // 적 캐릭터 UI 활성화
    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
    }

    // 적 캐릭터 UI 비활성화
    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    // 체력이 변경되면 호출되어 HP 값 변경
    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }
}
