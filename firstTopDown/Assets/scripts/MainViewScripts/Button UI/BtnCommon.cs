using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCommon : MonoBehaviour
{
    private GameObject target;

    void Start() {
        // ui태그가 UIBtn 이라는 태그를 가진 게임오브젝트 찾아내기 (target : Button)
        target = GameObject.FindGameObjectWithTag("UIBtn");
    }
}
