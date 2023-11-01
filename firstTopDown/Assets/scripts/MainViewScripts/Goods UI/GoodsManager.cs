using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsManager : MonoBehaviour
{
    // 광고 보상(재화) 변수를 사용하기 위한 클래스 참조
    public GoogleAdMob adm;

    // 코인 , 다이아 를 담을 변수
    public static int coinInt;
    public static int gemInt;

    // 코인 , 다이아 를 표시할 텍스트
    public static TMP_Text coinText;
    public static TMP_Text gemText;

    void Awake() {
        // coinBtn 에 구글애드몹 스크립트가 있음.
        // adm = GameObject.Find("BtnCanvas").transform.Find("ShopBtn").transform.Find("ShopCanvas").transform.Find("ShopIMG").transform.Find("CoinBtn")
        // .GetComponentInChildren<GoogleAdMob>();
        adm = FindObjectOfType<GoogleAdMob>();
        if (adm == null) {
            Debug.Log("not found");
        }
    }

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

        // PlayerPrefs 내에 저장되어있는 'Coin'을 불러옵니다. 만약에 저장된 정보가 없다면 0을 저장합니다.
        coinInt = PlayerPrefs.GetInt("Coin", 0);
        gemInt = PlayerPrefs.GetInt("Gem", 0);
        PlayerPrefs.Save();

        // Message.SetActive(false);
    }
    
    void Update() {
        // coinInt , gemInt 를 PlayerPrefs 내에 저장되어있는 'Coin' , 'Gem'에 저장합니다.
        PlayerPrefs.SetInt("Coin", coinInt);
        PlayerPrefs.SetInt("Gem", gemInt);


        // CoinText의 Text에 CoinInt를 출력합니다.
        coinText.text = coinInt.ToString();
        gemText.text = gemInt.ToString();

    }

    // 코인 얻는 함수
    public double GetCoin(double rewardCoin) {
        rewardCoin += PlayerPrefs.GetInt("Coin", 0);
        coinInt = (int)rewardCoin;
        Debug.Log("코인 얻었다");

        return coinInt;
    }

    // 보석 얻는 함수
    public double GetGem(double rewardGem) {
        rewardGem += PlayerPrefs.GetInt("Gem", 0); ;
        gemInt = (int)rewardGem;
        Debug.Log("보석 얻었다");

        return gemInt;
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
