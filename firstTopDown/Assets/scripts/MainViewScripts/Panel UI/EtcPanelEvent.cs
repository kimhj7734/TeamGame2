using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtcPanelEvent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    // private GameObject target2;
    private bool targetActive = false;

    void Start() {
        // target = GameObject.Find("BtnCanvas").transform.Find("ShopBtn").transform.Find("ShopCanvas").GetComponentInChildren<GameObject>();
        // if(target != null) {
        //     Debug.Log(target);
        // }

        // target2 = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").GetComponentInChildren<GameObject>();
        // if(target2 != null) {
        //     Debug.Log(target2);
        // }

        // 처음엔 비활성화 상태
        target.SetActive(targetActive);
    }

    // 버튼을 클릭할 때 패널을 토글
    public void TogglePanel() {
        targetActive = !targetActive;
        target.SetActive(targetActive);
        if (targetActive) {}
        else {}
    }
}
