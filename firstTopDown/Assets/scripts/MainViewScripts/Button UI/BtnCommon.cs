using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCommon : MonoBehaviour
{
    /*
        버튼 클릭(탭) 딜레이
    */

    [SerializeField] private GameObject target1;

    public float clickDelay = 0.5f; // 클릭 딜레이 시간
    private float lastClickTime = 0f;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // 현재 시간을 기록
            float currentTime = Time.time;
            // 이전 클릭 이후의 시간 간격을 계산
            float timeSinceLastClick = currentTime - lastClickTime;

            // 딜레이 시간 이후에만 클릭을 처리
            if (timeSinceLastClick >= clickDelay) {
                // 클릭 시간 업데이트
                lastClickTime = currentTime;
            }
        }
    }
}
