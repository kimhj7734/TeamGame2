using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatManager : MonoBehaviour
{
    // 스탯 구매 변수 (GoodsManager의 인자값)
    public static int buyAtkCoinInt = 1;
    public static int buyHealthCoinInt = 1;
    public static int buyAtkSpdCoinInt = 1;
    public static int buyCriticalCoinInt = 1;


    public static int buyGem = 5;

    // 필요 코인, 보석 텍스트
    // public static TMP_Text buyCoinText;
    // public static TMP_Text buyGemText;

    // 캐릭터 기본 스탯
    private float playerAtk = 0.05f;
    private float playerHealth = 0.05f;
    private float playerAtkSpeed = 0.05f;
    private float playerCritical = 0f;

    /*
        공격력 스탯 구매
        GoodsManager 클래스 참조(코인잃는함수), 필요 코인 없으면, 구매 안됨
    */
    public void buyAtk() {
        if(GoodsManager.coinInt >= buyAtkCoinInt) {
            GoodsManager.LostAtkCoin(buyAtkCoinInt);
            playerAtk *= 1.1f;  // 스탯 증가량
            PlayerPrefs.SetFloat("ATK", playerAtk);
            buyAtkCoinInt *= 2;   // 스탯 구매 할수록 필요코인 증가
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }

    // 체력 스탯 구매
    public void buyHealth() {
        if(GoodsManager.coinInt >= buyHealthCoinInt) {
            GoodsManager.LostHealthCoin(buyHealthCoinInt);
            playerHealth += 1.0f;   // 체력 스탯 증가량
            PlayerPrefs.SetFloat("Health", playerHealth);
            buyHealthCoinInt *= 2;   // 스탯 구매 시, 코인증가
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }

    // 공속 스탯 구매
    public void buyAtkSpd() {
        if (GoodsManager.coinInt >= buyAtkSpdCoinInt) {
            GoodsManager.LostAtkSpdCoin(buyAtkSpdCoinInt);
            playerAtkSpeed += 0.5f;
            PlayerPrefs.SetFloat("AtkSpd", playerAtkSpeed);
            buyAtkSpdCoinInt *= 2;
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }

    // 치명타확률 스탯 구매
    public void buyCritical() {
        if (GoodsManager.coinInt >= buyCriticalCoinInt) {
            GoodsManager.LostCriticalCoin(buyCriticalCoinInt);
            playerCritical += 0.5f;
            PlayerPrefs.SetFloat("Critical", playerCritical);
            buyCriticalCoinInt *= 2;
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }
}
