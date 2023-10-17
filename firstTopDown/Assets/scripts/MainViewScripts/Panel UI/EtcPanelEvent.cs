using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcPanelEvent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private bool targetActive = false;

    void Start() {
        // 처음엔 비활성화 상태
        target.SetActive(targetActive);
    }

    // 버튼을 클릭할 때 패널을 토글
    public void TogglePanel() {
        targetActive = !targetActive;
        target.SetActive(targetActive);
        if (targetActive) {
            Debug.Log("패널이 활성화되었습니다.");
        }

        else {
            Debug.Log("패널이 비활성화되었습니다.");
        }
    }
}
