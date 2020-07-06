using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;  // 다른 클래스에서도 GameManager에 접근 가능
    public  BoardManager boardScript;           // 레벨을 설정할 BoardManager에 대한 참조를 저장

    private int level = 3;                      // Day1, 게임에서의 현재 레벨 번호

    // Awake는 항상 Start 함수 전에 호출됩니다.
    void Awake()
    {
        // instance에 값이 없다면 이 스크립트를 넣어주자.
        if (instance == null)
            instance = this;
        // 만약 instance가 이 스크립트가 아니라면 gameObject를 삭제하자. 중복 생성 방지.
        else if (instance != this)
            Destroy(gameObject);

        // 다른 씬으로 넘어갈 때 gameObject가 사라지지 않는다.
        DontDestroyOnLoad(gameObject);
        // Call by reference 
        // 연결된 BoardManager 스크립트에 대한 구성 요소 참조 가져 오기
        boardScript = GetComponent<BoardManager>();

        // 첫 번째 레벨을 초기화
        InitGame();
    }

    // 각 레벨에 맞게 게임을 초기화
    void InitGame()
    {
        // 현재 레벨 번호 전달(적이 얼마나 나올지 결정)
        boardScript.SetupScene(level);
    }
}


