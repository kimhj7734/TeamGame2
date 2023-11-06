using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsManager : MonoBehaviour
{
    // 코인 , 다이아 를 담을 변수
    public static int coinInt;
    public static int gemInt;

    // 코인 , 다이아 를 표시할 텍스트
    public static TMP_Text coinText;
    public static TMP_Text gemText;

    // 무료 재화를 표시할 텍스트
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
        /*
            CoinText 는 GoodsCanvas/GoodsPanel/CoinRawImage 있는 자식 컴포넌트인 coinText 입니다.
            GemText  는 GoodsCanvas/GoodsPanel/GemRawImage  있는 자식 컴포넌트인 gemText  입니다.
        */
        coinText = GameObject.Find("GoodsCanvas").transform.Find("GoodsPanel").transform.Find("CoinRawImage").GetComponentInChildren<TMP_Text>();
        if (coinText == null) {
            Debug.Log("coinText not found !");
        }

        gemText = GameObject.Find("GoodsCanvas").transform.Find("GoodsPanel").transform.Find("GemRawImage").GetComponentInChildren<TMP_Text>();
        if (gemText == null) {
            Debug.Log("gemText not found !");
        }

        /*
            freeCoinText 는 BtnCanvas/ShopBtn/ShopCanvas/ShopIMG 에 있는 자식 컴포넌트인 freeCoinText 입니다.
            freeGemText  는 BtnCanvas/ShopBtn/ShopCanvas/ShopIMG 에 있는 자식 컴포넌트인 freeGemText  입니다.
        */
        freeCoinText = GameObject.Find("BtnCanvas").transform.Find("ShopBtn").transform.Find("ShopCanvas").transform.Find("ShopIMG")
        .transform.Find("FreeCoinBtn").GetComponentInChildren<TMP_Text>();
        if (freeCoinText == null) {
            Debug.Log("coinText not found !");
        }

        freeGemText = GameObject.Find("BtnCanvas").transform.Find("ShopBtn").transform.Find("ShopCanvas").transform.Find("ShopIMG")
        .transform.Find("FreeGemBtn").GetComponentInChildren<TMP_Text>();
        if (freeGemText == null) {
            Debug.Log("coinText not found !");
        }
        /******************************************** 컴포넌트 / 오브젝트 참조 경로 End *****************************************************************/

        // PlayerPrefs 내에 저장되어있는 'Coin'을 불러옵니다. 만약에 저장된 정보가 없다면 0을 저장합니다.
        coinInt = PlayerPrefs.GetInt("Coin", 0);
        gemInt = PlayerPrefs.GetInt("Gem", 0);

        // 현재 시간을 제외하고 날짜만 저장
        PlayerPrefs.GetString("SavedDate", currentDate);

        // 현재까지 저장된 데이터 모두 삭제 (테스트용)
        // PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();
    }
    
    void Update() {
        // coinInt , gemInt , currentDate 를 PlayerPrefs 내에 저장되어있는 'Coin' , 'Gem' , 'SavedDate' 에 저장합니다.
        PlayerPrefs.SetInt("Coin", coinInt);
        PlayerPrefs.SetInt("Gem", gemInt);
        PlayerPrefs.SetString("SavedDate", currentDate);

        /*
            CoinText의 Text에 CoinInt , GemText 의 Text에 GemInt 표시
            일일제한 코인,보석을 표시
        */
        coinText.text = coinInt.ToString();
        gemText.text = gemInt.ToString();

        freeCoinText.text = "무료코인 : " + countCoin + " / 5";
        freeGemText.text = "무료보석 : " + countGem + " / 5";

    }

    // 광고보상형 코인 얻는 함수
    public double GetCoin(double rewardCoin) {
        rewardCoin += PlayerPrefs.GetInt("Coin", 0);
        coinInt = (int)rewardCoin;
        Debug.Log("코인 얻었다");

        return coinInt;
    }

    // 광고보상형 보석 얻는 함수
    public double GetGem(double rewardGem) {
        rewardGem += PlayerPrefs.GetInt("Gem", 0); ;
        gemInt = (int)rewardGem;
        Debug.Log("보석 얻었다");

        return gemInt;
    }

    // 무료보상 코인, 하루 5번까지
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
            coinInt += freeCoin;
            PlayerPrefs.SetInt("Coin", coinInt);
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
            gemInt += freeGem;
            PlayerPrefs.SetInt("Coin", coinInt);
            PlayerPrefs.Save();
        }

        else {
            Debug.Log("일일 제한을 초과하셨습니다.");
        }
        
    }

    // // 코인 잃는 함수
    // public void LostCoin() {
    //     /*
    //         CoinInt가 40이상이라면 , CoinInt가 40 줄어듭니다.
    //     */
    //     if (CoinInt >= 40) {
    //         CoinInt -= 40;
    //         Debug.Log("돈 잃었다..");
    //     }

    //     /*
    //         만약에 부족하다면 메시지 오브젝트를 활성화 , 
    //         MSG의 Text를 "돈이 부족합니다"로 출력합니다.
    //         TimeSet를 true로 합니다.
    //     */
    //     else {
    //         Message.SetActive(true);
    //         MSG.text = "돈이 부족합니다".ToString();
    //         TimeSet = true;
    //     }
    // }

    

    // // 보석 잃는 함수
    // public void LostGem() {
    //     /*
    //         GemInt가 40이상이라면 , CoinInt가 40 줄어듭니다.
    //     */
    //     if (GemInt >= 50) {
    //         GemInt -= 50;
    //         Debug.Log("보석 잃었다..");
    //     }

    //     /*
    //         만약에 부족하다면 메시지 오브젝트를 활성화 , 
    //         MSG의 Text를 "돈이 부족합니다"로 출력합니다.
    //         TimeSet를 true로 합니다.
    //     */
    //     else {
    //         Message.SetActive(true);
    //         MSG.text = "보석이 부족합니다".ToString();
    //         TimeSet = true;
    //     }
    // }
}
