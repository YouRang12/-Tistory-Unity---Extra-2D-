using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent (typeof(AudioSource))]
public class GameManager : MonoBehaviour {

    AudioSource music;      // 배경 및 게임오버 음악
    Transform spPoint;      // SpawnPoint
    Vector3 wrdSize;        // 화면의 크기 (월드좌표)

    Transform owl;          // 올빼미

    Image pnButton;         // Button Panel
    Image pnOver;           // Game Over Panel

    Text txtHight;          // Text Widget
    Text txtGift;
    Text txtBird;
    Text txtScore;

    float owlHeight = 0;    // 점수 처리용
    int giftScore = 0;
    int giftCnt = 0;
    int birdCnt = 0;
    int score = 0;

    public bool isMobile;   // 모바일인가?
    public float btnAxis;   // 버튼 Value(-1.0 ~ 1.0)

    int dir;                // -1: 왼쪽 버튼, 1: 오른쪽 버튼
    bool isOver;            // 게임 오버인가?

    void Awake () {
        InitGame();
        InitWidget();
    }

    // Update is called once per frame
    void Update () {
        MakeBranch();
        MakeBird();
        MakeGift();

        if (!isOver) SetScore();
    }

    // Score 계산
    void SetScore()
    {
        // 올뺴미 최대 높이 계산
        if (owl.position.y > owlHeight)
            owlHeight = owl.position.y;

        score = Mathf.FloorToInt(owlHeight) * 100 + giftScore - birdCnt * 100;

        // 화면 표신
        txtHight.text = owlHeight.ToString("#,##0.0");
        txtGift.text = giftCnt.ToString();
        txtBird.text = birdCnt.ToString();
        txtScore.text = score.ToString("#,##0");
    }
    
    // 나뭇가지 만들기
    void MakeBranch () {
        // 나뭇가지의 개수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Branch").Length;
        if (cnt > 3) return;

        // SpawnPoint 높이에 지그재그로 배치
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);

        // 나뭇가지
        GameObject branch = Instantiate(Resources.Load("Branch")) as GameObject;
        branch.transform.position = pos;

        // SpawnPoint를 위로 이동
        spPoint.position += new Vector3(0, 3, 0);
    }

    // 참새 만들기
    void MakeBird() {
        // 참새 수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Bird").Length;
        if (cnt > 7 || Random.Range(0, 1000) < 980) return;

        Vector3 pos = spPoint.position;
        pos.y -= Random.Range(0, 2f);

        GameObject bird = Instantiate(Resources.Load("Bird")) as GameObject;
        bird.transform.position = pos;
    }

    // 선물상자 만들기
    void MakeGift () {
        // 선물상자 수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Gift").Length;
        if (cnt > 5 || Random.Range(0, 1000) < 980) return;

        // 위치
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);
        pos.y += Random.Range(0.5f, 1.5f);

        // 이름
        GameObject gift = Instantiate(Resources.Load("Gift0")) as GameObject;
        gift.name = "Gift" + Random.Range(0, 3);      // 0~2
        gift.transform.position = pos;
    }

    // 게임 초기화
    void InitGame () {
        // 배경음악
        music = GetComponent<AudioSource>();
        music.loop = true;

        if (music.clip != null) {
            music.Play();
        }

        // SpawnPoint
        spPoint = GameObject.Find("SpawnPoint").transform;

        // World의 크기
        Vector3 scrSize = new Vector3(Screen.width, Screen.height);
        scrSize.z = 10;
        wrdSize = Camera.main.ScreenToWorldPoint(scrSize);

        Cursor.visible = false;
    }

    // 위젯 초기화
    void InitWidget()
    {
        // 모바일 디바이스인가?
        isMobile = Application.platform == RuntimePlatform.Android ||
                    Application.platform == RuntimePlatform.IPhonePlayer;

        Cursor.visible = isMobile;

        // 모바일 디바이스에서만 Button Panel 보이기
        pnButton = GameObject.Find("PanelButton").GetComponent<Image>();
        pnButton.gameObject.SetActive(isMobile);

        // Quit Panel
        pnOver = GameObject.Find("PanelOver").GetComponent<Image>();
        pnOver.gameObject.SetActive(false);

        // score Text
        txtHight = GameObject.Find("TxtHeight").GetComponent<Text>();
        txtGift = GameObject.Find("TxtGift").GetComponent<Text>();
        txtBird = GameObject.Find("TxtBird").GetComponent<Text>();
        txtScore = GameObject.Find("TxtScore").GetComponent<Text>();

        // 올빼미
        owl = GameObject.Find("Owl").transform;
    }

    // 선물획득 - 외부 호출
    void GetGift (int kind)
    {
        giftCnt++;
        giftScore += (kind * 100) + 100;
    }

    // 참새와 충돌 - 외부 호출
    void BirdStrike()
    {
        birdCnt++;
    }

    // 게임오버 - 외부 호출
    void SetGameOver()
    {
        isOver = true;
        pnOver.gameObject.SetActive(true);
        Cursor.visible = true;

        // 배경음악 바꾸기
        music.clip = Resources.Load("gameover", typeof(AudioClip)) as AudioClip;
        music.loop = false;
        music.Play();
    }
}
