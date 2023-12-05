using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PixGoodsScripts : MonoBehaviour
{
    // 인게임씬 내에서 코인 , 보석을 표시할 텍스트
    public static TMP_Text inGameCoinText;
    public static TMP_Text inGameGemText;
    
    // 일일제한 코인,보석 표시 텍스트
    public static TMP_Text freeCoinText;
    public static TMP_Text freeGemText;

    // 일일제한 코인, 보석 변수
    public static int countCoin = 0;
    public static int countGem = 0;

    // 현재 날짜 저장하기 위한 변수 (현재시간은 제외)
    public static DateTime dtNow = DateTime.Now;
    public string currentDate = dtNow.ToString("yyyy-MM-dd");
    public string lastRewardDate;
    
    void Start() {
        // 재화 표시 텍스트
        inGameCoinText = GameObject.Find("GoodsPanelCanvas").transform.Find("GoodsPanelFix")
        .transform.Find("InGameCoinRawImage").GetComponentInChildren<TMP_Text>();
        if (inGameCoinText == null) {
            Debug.Log("coinText not found !");
        }

        inGameGemText = GameObject.Find("GoodsPanelCanvas").transform.Find("GoodsPanelFix")
        .transform.Find("InGameGemRawImage").GetComponentInChildren<TMP_Text>();
        if (inGameGemText == null) {
            Debug.Log("gemText not found !");
        }

        // 일일보상 텍스트
        freeCoinText = GameObject.Find("UI Canvas").transform.Find("UI Btn Panel 2").transform.Find("SHOP Btn").transform.Find("ShopBtnCanvas")
        .transform.Find("ShopIMG").transform.Find("FreeCoinBtn").GetComponentInChildren<TMP_Text>();
        if (freeCoinText == null) {
            Debug.Log("coinText not found !");
        }

        freeGemText = GameObject.Find("UI Canvas").transform.Find("UI Btn Panel 2").transform.Find("SHOP Btn").transform.Find("ShopBtnCanvas")
        .transform.Find("ShopIMG").transform.Find("FreeGemBtn").GetComponentInChildren<TMP_Text>();
        if (freeGemText == null) {
            Debug.Log("coinText not found !");
        }
    }

    void Update() {
        inGameCoinText.text = GoodsManager.coinInt.ToString();
        inGameGemText.text = GoodsManager.gemInt.ToString();

        freeCoinText.text = "무료코인 : " + countCoin + " / 5";
        freeGemText.text = "무료보석 : " + countGem + " / 5";

    }

    // 인게임씬 상점 광고보상형 코인 얻는 함수
    public double GetCoin(double rewardCoin) {
        rewardCoin += PlayerPrefs.GetInt("Coin", 0);
        GoodsManager.coinInt = (int)rewardCoin;
        Debug.Log("코인 얻었다");

        return GoodsManager.coinInt;
    }

    // 인게임씬 상점 광고보상형 보석 얻는 함수
    public double GetGem(double rewardGem) {
        rewardGem += PlayerPrefs.GetInt("Gem", 0); ;
        GoodsManager.gemInt = (int)rewardGem;
        Debug.Log("보석 얻었다");

        return GoodsManager.gemInt;
    }

    // 인게임씬 무료보상 코인, 하루 5번까지
    public void FreeGetCoin() {
        // 이전 보상을 받은 날짜를 PlayerPrefs에서 가져옴 (시간제외)
        string lastRewardDate = PlayerPrefs.GetString("LastRewardDate", "").ToString();
        // 현재 날짜를 문자열로 저장 (시간제외)
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        // 보상 코인
        int freeCoin = 10;

        // 여기서 날짜 비교 조건문, 날짜 중 일자가 현재 일자와 같지 않으면 아래의 IF문 안탐.
        if (lastRewardDate != currentDate) {
            countCoin = 0; // 날짜가 바뀌었으므로 리셋

            // 현재 날짜를 LastRewardDate로 저장
            PlayerPrefs.SetString("LastRewardDate", currentDate);
            PlayerPrefs.Save();
        }

        if (countCoin < 5) {
            countCoin++;
            GoodsManager.coinInt += freeCoin;
            PlayerPrefs.SetInt("Coin", GoodsManager.coinInt);
            PlayerPrefs.Save();
        }

        else {
            Debug.Log("일일 제한을 초과하셨습니다.");
        }
    }

    // 무료보상 보석, 하루 5번까지
    public void FreeGetGem() {
        // 이전 보상을 받은 날짜를 PlayerPrefs에서 가져옴 (시간제외)
        string lastRewardDate = PlayerPrefs.GetString("LastRewardDate", "").ToString();
        // 현재 날짜를 문자열로 저장 (시간제외)
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

        // 무료 보상 보석
        int freeGem = 3;

        // 여기서 날짜 비교 조건문, 날짜 중 일자가 현재 일자와 같지 않으면 아래의 IF문 안탐.
        if (lastRewardDate != currentDate) {
            countGem = 0; // 날짜가 바뀌었으므로 리셋

            // 현재 날짜를 LastRewardDate로 저장
            PlayerPrefs.SetString("LastRewardDate", currentDate);
            PlayerPrefs.Save();
        }

        if (countGem < 5) {
            countGem++;
            GoodsManager.gemInt += freeGem;
            PlayerPrefs.SetInt("Gem", GoodsManager.gemInt);
            PlayerPrefs.Save();
        }

        else {
            Debug.Log("일일 제한을 초과하셨습니다.");
        }

    }
}
