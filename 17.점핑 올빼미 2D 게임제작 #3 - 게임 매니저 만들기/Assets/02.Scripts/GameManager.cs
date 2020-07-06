using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    AudioSource music;      // 배경 및 게임오버 음악
    Transform spPoint;      // SpawnPoint
    Vector3 wrdSize;        // 화면의 크기 (월드좌표)

    void Awake () {
        InitGame();
    }

    // Update is called once per frame
    void Update () {
        MakeBranch();
        MakeBird();
        MakeGift();
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
}
