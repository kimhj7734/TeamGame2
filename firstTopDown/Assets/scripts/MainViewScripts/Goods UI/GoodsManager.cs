using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsManager : MonoBehaviour
{
    // 스탯 구매버튼
    private static Button buyAtkBtn;
    private static Button buyHealthBtn;
    private static Button buyAtkSpdBtn;
    private static Button buyCriticalBtn;


    private static bool targetActive = true;

    // 코인 , 다이아 를 담을 변수
    public static int coinInt;
    public static int gemInt;

    // 각 스탯 별 필요코인 저장 변수
    public static int buyAtkCoinInt;
    public static int buyHealthCoinInt;
    public static int buyAtkSpdCoinInt;
    public static int buyCriticalCoinInt;

    public static int buyGemInt;

    // 코인 , 다이아 를 표시할 텍스트
    public static TMP_Text coinText;
    public static TMP_Text gemText;

    // 무료 재화를 표시할 텍스트
    public static TMP_Text freeCoinText;
    public static TMP_Text freeGemText;

    // 일일제한 코인, 보석 변수
    public static int countCoin = 0;
    public static int countGem = 0;

    // 잃는 코인, 보석 변수
    // public static StatManager buyCoin;
    // public static int buyGem;

    // 각 스탯 별 필요코인 ,보석 표시할 텍스트
    public static TMP_Text buyAtkCoinText;
    public static TMP_Text buyHealthCoinText;
    public static TMP_Text buyAtkSpdCoinText;
    public static TMP_Text buyCriticalCoinText;


    public static TMP_Text buyGemText;

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

        /*
            buyAtkCoinText 는 BtnCanvas/StatBtn/StatCanvas/StatIMG/buyAtkBtn            에 있는 자식 컴포넌트인 buyAtkCoinText       입니다.
            buyHealthCoinText 는 BtnCanvas/StatBtn/StatCanvas/StatIMG/buyHealthBtn      에 있는 자식 컴포넌트인 buyHealthCoinText    입니다.
            buyAtkSpdCoinText 는 BtnCanvas/StatBtn/StatCanvas/StatIMG/buyAtkSpdBtn      에 있는 자식 컴포넌트인 buyAtkSpdCoinText    입니다.
            buyCriticalCoinText 는 BtnCanvas/StatBtn/StatCanvas/StatIMG/buyCriticalBtn  에 있는 자식 컴포넌트인 buyCriticalCoinText  입니다.
        */
        buyAtkCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyAtkBtn").GetComponentInChildren<TMP_Text>();
        if (buyAtkCoinText == null) {
            Debug.Log("buyCoinText not found !");
        }

        buyHealthCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyHealthBtn").GetComponentInChildren<TMP_Text>();
        if (buyHealthCoinText == null) {
            Debug.Log("buyCoinText not found !");
        }

        buyAtkSpdCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyAtkSpdBtn").GetComponentInChildren<TMP_Text>();
        if (buyAtkSpdCoinText == null) {
            Debug.Log("buyCoinText not found !");
        }

        buyCriticalCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyCriticalBtn").GetComponentInChildren<TMP_Text>();
        if (buyCriticalCoinText == null) {
            Debug.Log("buyCoinText not found !");
        }

        /************************************************* Text 경로 End *******************************************************************************/

        /************************************************* 버튼 경로 Start *****************************************************************************/
        // buyAtkBtn
        buyAtkBtn = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .GetComponentInChildren<Button>();
        if (buyAtkBtn != null) {
            buyAtkBtn.interactable = targetActive; // true
        }
        else {
            Debug.Log("target not found");
        }

        // buyHealthBtn
        buyHealthBtn = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .GetComponentInChildren<Button>();
        if (buyHealthBtn != null) {
            buyHealthBtn.interactable = targetActive; // true
        }
        else {
            Debug.Log("target not found");
        }

        // buyAtkSpdBtn
        buyAtkSpdBtn = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .GetComponentInChildren<Button>();
        if (buyAtkSpdBtn != null) {
            buyAtkSpdBtn.interactable = targetActive; // true
        }
        else {
            Debug.Log("target not found");
        }

        // buyCriticalBtn
        buyCriticalBtn = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .GetComponentInChildren<Button>();
        if (buyCriticalBtn != null) {
            buyCriticalBtn.interactable = targetActive; // true
        }
        else {
            Debug.Log("target not found");
        }
        /******************************************** 컴포넌트 / 오브젝트 참조 경로 End *****************************************************************/

        // PlayerPrefs 내에 저장되어있는 'Coin'을 불러옵니다. 만약에 저장된 정보가 없다면 0을 저장합니다.
        coinInt = PlayerPrefs.GetInt("Coin", 0);
        gemInt = PlayerPrefs.GetInt("Gem", 0);

        // 스탯 별 소비코인,보석
        buyAtkCoinInt = PlayerPrefs.GetInt("buyAtkCoin", 0);
        buyHealthCoinInt = PlayerPrefs.GetInt("buyHealthCoin", 0);
        buyAtkSpdCoinInt = PlayerPrefs.GetInt("buyAtkSpdCoin", 0);
        buyCriticalCoinInt = PlayerPrefs.GetInt("buyCriticalCoin", 0);


        buyGemInt = PlayerPrefs.GetInt("buyGem", 0);

        // 현재 시간을 제외하고 날짜만 저장
        PlayerPrefs.GetString("SavedDate", currentDate);

        // 현재까지 저장된 데이터 모두 삭제 (테스트용)
        PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();
    }
    
    void Update() {
        // coinInt , gemInt , currentDate 를 PlayerPrefs 내에 저장되어있는 'Coin' , 'Gem' , 'SavedDate' 에 저장합니다.
        PlayerPrefs.SetInt("Coin", coinInt);
        PlayerPrefs.SetInt("Gem", gemInt);
        PlayerPrefs.SetString("SavedDate", currentDate);

        // buyCoin , buyGem 를 buyCoin , buyGem 변수에 저장함.
        // PlayerPrefs.SetInt("buyCoin", buyCoinInt);
        // PlayerPrefs.SetInt("buyGem", buyGemInt);

        /*
            CoinText의 Text에 CoinInt , GemText 의 Text에 GemInt 표시
            일일제한 코인,보석을 표시
            필요코인 표시
        */
        coinText.text = coinInt.ToString();
        gemText.text = gemInt.ToString();

        freeCoinText.text = "무료코인 : " + countCoin + " / 5";
        freeGemText.text = "무료보석 : " + countGem + " / 5";

        buyAtkCoinText.text = PlayerPrefs.GetInt("buyAtkCoin", buyAtkCoinInt).ToString() + " 원";
        buyHealthCoinText.text = PlayerPrefs.GetInt("buyHealthCoin", buyHealthCoinInt).ToString() + " 원";
        buyAtkSpdCoinText.text = PlayerPrefs.GetInt("buyAtkSpdCoin", buyAtkSpdCoinInt).ToString() + " 원";
        buyCriticalCoinText.text = PlayerPrefs.GetInt("buyCriticalCoin", buyCriticalCoinInt).ToString() + " 원";

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

    /******************************************** 스탯 별 필요코인 함수 Start *****************************************************************/
    // 공격 스탯 구매 시,코인 잃는 함수
    public static int LostAtkCoin(int buyAtkCoinInt) {
        PlayerPrefs.GetInt("Coin", coinInt);
        if (coinInt >= buyAtkCoinInt) {
            coinInt -= buyAtkCoinInt;
            
            Debug.Log("돈 잃었다..");

            PlayerPrefs.SetInt("buyAtkCoin", buyAtkCoinInt);

            PlayerPrefs.SetInt("Coin", coinInt);
            PlayerPrefs.Save();

            return coinInt;
        }

        // 필요코인 부족할 때,
        else if(coinInt < buyAtkCoinInt) {
            buyAtkBtn.interactable = !targetActive;    // false
            return coinInt;
        } 
        
        else {  // 필요코인 충분할 때(재구매 가능)
            buyAtkBtn.interactable = targetActive;     // true
            return coinInt;
        }
    }

    // 체력 스탯 구매 시, 잃는코인 함수
    public static int LostHealthCoin(int buyHealthCoinInt) {
        PlayerPrefs.GetInt("Coin", coinInt);
        if (coinInt >= buyHealthCoinInt) {
            coinInt -= buyHealthCoinInt;

            Debug.Log("돈 잃었다..");

            PlayerPrefs.SetInt("buyHealthCoin", buyHealthCoinInt);

            PlayerPrefs.SetInt("Coin", coinInt);
            PlayerPrefs.Save();

            return coinInt;
        }

        // 필요코인 부족할 때,
        else if (coinInt < buyHealthCoinInt) {
            buyHealthBtn.interactable = !targetActive;    // false
            return coinInt;
        }

        else {  // 필요코인 충분할 때(재구매 가능)
            buyHealthBtn.interactable = targetActive;     // true
            return coinInt;
        }
    }

    // 공속 스탯 구매 시, 잃는코인 함수
    public static int LostAtkSpdCoin(int buyAtkSpdCoinInt) {
        PlayerPrefs.GetInt("Coin", coinInt);
        if (coinInt >= buyAtkSpdCoinInt) {
            coinInt -= buyAtkSpdCoinInt;

            Debug.Log("돈 잃었다..");

            PlayerPrefs.SetInt("buyAtkSpdCoin", buyAtkSpdCoinInt);

            PlayerPrefs.SetInt("Coin", coinInt);
            PlayerPrefs.Save();

            return coinInt;
        }

        // 필요코인 부족할 때,
        else if (coinInt < buyAtkSpdCoinInt) {
            buyAtkSpdBtn.interactable = !targetActive;    // false
            return coinInt;
        }

        else {  // 필요코인 충분할 때(재구매 가능)
            buyAtkSpdBtn.interactable = targetActive;     // true
            return coinInt;
        }
    }

    // 치명타 스탯 구매 시, 잃는코인 함수
    public static int LostCriticalCoin(int buyCriticalCoinInt) {
        PlayerPrefs.GetInt("Coin", coinInt);
        if (coinInt >= buyCriticalCoinInt) {
            coinInt -= buyCriticalCoinInt;

            Debug.Log("돈 잃었다..");

            PlayerPrefs.SetInt("buyCriticalCoin", buyCriticalCoinInt);

            PlayerPrefs.SetInt("Coin", coinInt);
            PlayerPrefs.Save();

            return coinInt;
        }

        // 필요코인 부족할 때,
        else if (coinInt < buyCriticalCoinInt) {
            buyCriticalBtn.interactable = !targetActive;    // false
            return coinInt;
        }

        else {  // 필요코인 충분할 때(재구매 가능)
            buyCriticalBtn.interactable = targetActive;     // true
            return coinInt;
        }
    }

    /******************************************** 스탯 별 필요코인 함수 End *****************************************************************/

    // 보석 잃는 함수
    public static int LostGem(int buyGem) {
        if (gemInt >= buyGem) {
            gemInt -= buyGem;
            PlayerPrefs.GetInt("Gem", gemInt);
            Debug.Log(gemInt);

            Debug.Log("보석 잃었다..");
            return gemInt;
        }

        else {
            return gemInt;
        }
    }
}
