using System.Net.Mime;
using System.Dynamic;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GoogleAdMob : MonoBehaviour
{
    public static GoodsManager gm;
    public static PixGoodsScripts pg;
    public static int rewardCoin;
    public static int rewardGem;

    /* 안드로이드 , 아이폰 보상형 광고 테스트ID */
    #if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    #elif UNITY_IPHONE
        private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
    #else
        private string _adUnitId = "unused";
    #endif

    void Awake() {
        // 재화(돈,보석) 변수를 쓰기 위한 클래스 참조
        // GameObject.Find("(a스크립트가 있는 오브젝트의 이름)").GetComponent<a>().c 로 접근
        // 'gm' 변수에 'gm' 스크립트의 인스턴스를 할당
        // gm = FindObjectOfType<YourGMScriptType>();
        gm = FindObjectOfType<GoodsManager>();
        pg = FindObjectOfType<PixGoodsScripts>();

        // GoodsManager gm = GameObject.Find("GoodsCanvas").transform.Find("GoodsPanel").transform.Find("CoinRawImage")
        // .GetComponentInChildren<GoodsManager>();
    }

    void Start() {
        // MobileAds.Initialize((InitializationStatus initStatus) => {
        //     // This callback is called once the MobileAds SDK is initialized.
        //     // LoadRewardedAd();
        // });
    }

    private RewardedAd rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    
    // 코인 리워드
    public void LoadRewardedAdCoin() {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null) {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                

                RegisterEventHandlers(rewardedAd);

                ShowRewardedAdCoin();
            });
    }

    // 보석 리워드
    public void LoadRewardedAdGem() {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null) {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;


                RegisterEventHandlers(rewardedAd);

                ShowRewardedAdGem();
            });
    }

    // 코인 리워드
    public void ShowRewardedAdCoin() {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}, amount: {1}";
        
        // 여기에서 광고 보상이 지급됨.
        if (rewardedAd != null && rewardedAd.CanShowAd()) {
            rewardedAd.Show((Reward reward) => {
                // TODO: Reward the user.
                
                // 보상의 양을 int로 변환
                int rewardCoin = (int)reward.Amount;

                // 메인게임씬 광고보상을 코인에 추가 
                if (gm != null) {
                    double newCoinAmount = gm.GetCoin(rewardCoin);
                    
                    // 코인을 PlayerPrefs에 저장
                    PlayerPrefs.SetInt("Coin", (int)newCoinAmount);
                    PlayerPrefs.Save();

                }

                // 인게임씬 광고보상을 코인에 추가
                if (pg != null) {
                    double newCoinAmount2 = pg.GetCoin(rewardCoin);

                    PlayerPrefs.SetInt("Coin", (int)newCoinAmount2);
                    PlayerPrefs.Save();
                }

                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        // 광고닫기 기능
        rewardedAd = null;
    }

    // 보석 리워드
    public void ShowRewardedAdGem() {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}, amount: {1}";

        // 여기에서 광고 보상이 지급됨. 추후에 광고제거를 샀을 경우 여기에서 조건문
        if (rewardedAd != null && rewardedAd.CanShowAd()) {
            rewardedAd.Show((Reward reward) => {
                // TODO: Reward the user.

                // 보상의 양을 int로 변환
                int rewardCoin = (int)reward.Amount;
                int rewardGem = (int)reward.Amount;

                // 메인게임씬 광고보상을 코인에 추가
                if (gm != null) {
                    double newGemAmount = gm.GetGem(rewardGem);

                    // 보석을 PlayerPrefs에 저장
                    PlayerPrefs.SetInt("Gem", (int)newGemAmount);
                    PlayerPrefs.Save();

                }

                // 인게임씬 광고보상을 보석에 추가 
                if (pg != null) {
                    double newGemAmount2 = pg.GetGem(rewardGem);

                    // 보석을 PlayerPrefs에 저장
                    PlayerPrefs.SetInt("Gem", (int)newGemAmount2);
                    PlayerPrefs.Save();

                }

                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        // 광고닫기 기능
        rewardedAd = null;
    }

    private void RegisterEventHandlers(RewardedAd ad) {
        // Raised when the ad is estimated to have earned money. (광고로 수익이 발생한 것으로 추정되는 경우 발생합니다.)
        ad.OnAdPaid += (AdValue adValue) => {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () => {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () => {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () => {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () => {
            Debug.Log("Rewarded ad full screen content closed.");
            /* 광고 닫기 동작하기 위한 함수 호출 */
            ShowRewardedAdCoin();   // 코인
            ShowRewardedAdGem();    // 보석
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) => {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            ShowRewardedAdCoin();   // 코인
            ShowRewardedAdGem();    // 보석
        };
    }
}
