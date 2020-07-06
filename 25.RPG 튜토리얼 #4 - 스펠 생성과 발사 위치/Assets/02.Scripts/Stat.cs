using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

    private Image content;

    [SerializeField]
    private Text statText;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;
    public float MyMaxValue { get; set; }


    // 체력과 마나의 현재 값 설정
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue) currentValue = MyMaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;

            currentFill = currentValue / MyMaxValue;
            statText.text = currentValue + " / " + MyMaxValue;
        }
    }

    private float currentValue;

    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // 체력 or 마나의 값이 변경될 경우
        if (currentFill != content.fillAmount)
        {
            // Mathf.Lerp(시작값, 끝값, 기준) => 부드럽게 값을 변경 가능
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    // 체력과 마나 값을 셋팅(현재 값, 최대 값)
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
