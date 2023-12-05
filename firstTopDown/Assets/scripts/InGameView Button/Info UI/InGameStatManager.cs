using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameStatManager : MonoBehaviour
{
    // 플레이어 스탯 정보 표기할 변수
    public static TMP_Text AtkText;
    public static TMP_Text AtkSpdText;
    public static TMP_Text HealthText;
    public static TMP_Text CriticalText;


    void Start() {
        AtkText = GameObject.Find("UI Canvas").transform.Find("UI Panel 1").transform.Find("AtkObject").GetComponentInChildren<TMP_Text>();
        if(AtkText == null) {
            Debug.Log("not found AtkText");
        }

        AtkSpdText = GameObject.Find("UI Canvas").transform.Find("UI Panel 1").transform.Find("AtkSpdObject").GetComponentInChildren<TMP_Text>();
        if (AtkSpdText == null) {
            Debug.Log("not found AtkText");
        }

        HealthText = GameObject.Find("UI Canvas").transform.Find("UI Panel 1").transform.Find("HealthObject").GetComponentInChildren<TMP_Text>();
        if (HealthText == null) {
            Debug.Log("not found AtkText");
        }

        CriticalText = GameObject.Find("UI Canvas").transform.Find("UI Panel 1").transform.Find("CriticalObject").GetComponentInChildren<TMP_Text>();
        if (CriticalText == null) {
            Debug.Log("not found AtkText");
        }
    }

    void Update() {
        AtkText.text = "공격력 : "+StatManager.playerAtk.ToString();
        AtkSpdText.text = "공속 : "+StatManager.playerHealth.ToString();
        HealthText.text = "체력 : "+StatManager.playerAtkSpeed.ToString();
        CriticalText.text = "치명타 : "+StatManager.playerCritical.ToString();

    }
}
