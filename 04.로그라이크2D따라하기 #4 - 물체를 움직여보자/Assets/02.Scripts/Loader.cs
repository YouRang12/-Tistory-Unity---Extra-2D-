using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;          // gameManager 프리팹 저장

    void Awake()
    {
        // gameManager 값이 없으면 gamaManager 생성
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
