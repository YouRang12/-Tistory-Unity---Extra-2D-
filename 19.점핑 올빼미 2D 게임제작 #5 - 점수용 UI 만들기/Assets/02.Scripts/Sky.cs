using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour {

    Material mat;           // 머티리얼 
    float speed = 0.05f;    // 스크롤 속도

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 ofs = mat.mainTextureOffset;
        ofs.x += speed * Time.deltaTime;

        mat.mainTextureOffset = ofs;
    }
}

