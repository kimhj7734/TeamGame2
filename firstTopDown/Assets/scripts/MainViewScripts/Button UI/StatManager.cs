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
    public static TMP_Text buyAtkCoinText;
    public static TMP_Text buyHealthCoinText;
    public static TMP_Text buyAtkSpdCoinText;
    public static TMP_Text buyCriticalCoinText;

    // public static TMP_Text buyCoinText;
    // public static TMP_Text buyGemText;

    // 캐릭터 기본 스탯 (인게임씬 infoStatManager에서 쓰여야 하기 때문에 private > public 으로 바꿈)
    public static float playerAtk = 0.05f;
    public static float playerHealth = 0.05f;
    public static float playerAtkSpeed = 0.05f;
    public static float playerCritical = 0f;

    void Start() {
        // 공격력
        buyAtkCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyAtkBtn").GetComponentInChildren<TMP_Text>();
        if (buyAtkCoinText == null) {
            Debug.Log("buyAtkCoinText not found !");
        }

        // 체력
        buyHealthCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyHealthBtn").GetComponentInChildren<TMP_Text>();
        if (buyHealthCoinText == null) {
            Debug.Log("buyHealthCoinText not found !");
        }

        // 공격력 속도
        buyAtkSpdCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyAtkSpdBtn").GetComponentInChildren<TMP_Text>();
        if (buyAtkSpdCoinText == null) {
            Debug.Log("buyAtkSpdCoinText not found !");
        }

        // 치명타
        buyCriticalCoinText = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG")
        .transform.Find("buyCriticalBtn").GetComponentInChildren<TMP_Text>();
        if (buyCriticalCoinText == null) {
            Debug.Log("buyCriticalCoinText not found !");
        }

        // 플레이어 기본 스탯 저장된 값 불러오고, 값이 없으면 우측에 표시된 값으로 불러옴
        PlayerPrefs.GetFloat("ATK", 0.05f);
        PlayerPrefs.GetFloat("Health", 0.05f);
        PlayerPrefs.GetFloat("AtkSpd", 0.05f);
        PlayerPrefs.GetFloat("Critical", 0.0f);

        /********************************************** Start() Text 경로 End *************************************************************************/
        
        // 처음에 2원 계산을 하기 위해, buyAtkCoinInt를 2로 설정
        PlayerPrefs.SetInt("buyAtkCoin", 2);
        PlayerPrefs.SetInt("buyHealthCoin", 2);
        PlayerPrefs.SetInt("buyAtkSpdCoin", 2);
        PlayerPrefs.SetInt("buyCriticalCoin", 2);

        buyAtkCoinInt = PlayerPrefs.GetInt("buyAtkCoin", buyAtkCoinInt);
        buyHealthCoinInt = PlayerPrefs.GetInt("buyHealthCoin", buyHealthCoinInt);
        buyAtkSpdCoinInt = PlayerPrefs.GetInt("buyAtkSpdCoin", buyAtkSpdCoinInt);
        buyCriticalCoinInt = PlayerPrefs.GetInt("buyCriticalCoin", buyCriticalCoinInt);


        buyAtkCoinText.text = buyAtkCoinInt.ToString() + " 원";
        buyHealthCoinText.text = buyHealthCoinInt.ToString() + " 원";
        buyAtkSpdCoinText.text = buyAtkSpdCoinInt.ToString() + " 원";
        buyCriticalCoinText.text = buyCriticalCoinInt.ToString() + " 원";
    }

    /*
        공격력 스탯 구매
        GoodsManager 클래스 참조(코인잃는함수), 필요 코인 없으면, 구매 안됨
    */
    public void buyAtk() {
        if(GoodsManager.coinInt >= buyAtkCoinInt) {           
            if(buyAtkCoinInt == 2) { // 두번째 버튼 클릭 이 후부터는 2배 증가.
                playerAtk *= 1.1f;  // 스탯 증가량
                PlayerPrefs.SetFloat("ATK", playerAtk);
                PlayerPrefs.Save();

                int returnedValue_1 = GoodsManager.LostAtkCoin(GoodsManager.buy2AtkCoinInt);
                // 리턴된 값으로 buyAtkCoinInt 갱신
                buyAtkCoinInt = returnedValue_1;
            }
            
            else if(PlayerPrefs.GetInt("buyAtkCoin", buyAtkCoinInt) >= 4) {
                // LostAtkCoin 메서드 호출 및 결과값 획득
                int returnedValue_2 = GoodsManager.LostAtkCoin(GoodsManager.buy2AtkCoinInt);
                if (returnedValue_2 > 0) {
                    playerAtk *= 1.1f;  // 스탯 증가량
                    PlayerPrefs.SetFloat("ATK", playerAtk);
                    PlayerPrefs.Save();
                }
                else {
                    Debug.Log("돈이 충분치 않습니다.");
                }
            }

            // PlayerPrefs에 최신 값 저장
            PlayerPrefs.SetInt("buyAtkCoin", buyAtkCoinInt);            
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }

    // 체력 스탯 구매
    public void buyHealth() {
        if(GoodsManager.coinInt >= buyHealthCoinInt) {
            if (buyHealthCoinInt == 2) { // 두번째 버튼 클릭 이 후부터는 2배 증가.
                playerHealth += 1.0f;  // 스탯 증가량
                PlayerPrefs.SetFloat("Health", playerHealth);
                PlayerPrefs.Save();

                int returnedValue_1 = GoodsManager.LostHealthCoin(GoodsManager.buy2HealthCoinInt);

                // 리턴된 값으로 buyHealthCoinInt 갱신
                buyHealthCoinInt = returnedValue_1;
            }

            else if(PlayerPrefs.GetInt("buyHealthCoin", buyHealthCoinInt) >= 4) {
                int returnedValue_2 = GoodsManager.LostHealthCoin(GoodsManager.buy2HealthCoinInt);
                if(returnedValue_2 > 0) {
                    playerHealth += 1.0f;  // 스탯 증가량
                    PlayerPrefs.SetFloat("Health", playerHealth);
                    PlayerPrefs.Save();
                }
                else {
                    Debug.Log("돈이 충분치 않습니다.");
                }
            }
            PlayerPrefs.SetInt("buyHealthCoin", buyHealthCoinInt);
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }

    // 공속 스탯 구매
    public void buyAtkSpd() {
        if (GoodsManager.coinInt >= buyAtkSpdCoinInt) {
            if (buyAtkSpdCoinInt == 2) { // 두번째 버튼 클릭 이 후부터는 2배 증가.
                playerAtkSpeed += 0.5f;
                PlayerPrefs.SetFloat("AtkSpd", playerAtkSpeed);
                PlayerPrefs.Save();

                int returnedValue_1 = GoodsManager.LostAtkSpdCoin(GoodsManager.buy2AtkSpdCoinInt);

                // 리턴된 값으로 buyAtkSpdCoinInt 갱신
                buyAtkSpdCoinInt = returnedValue_1;
            }

            else if (PlayerPrefs.GetInt("buyAtkSpdCoin", buyAtkSpdCoinInt) >= 4) {
                int returnedValue_2 = GoodsManager.LostAtkSpdCoin(GoodsManager.buy2AtkSpdCoinInt);
                if (returnedValue_2 > 0) {
                    playerAtkSpeed += 0.5f;
                    PlayerPrefs.SetFloat("AtkSpd", playerAtkSpeed);
                    PlayerPrefs.Save();
                }
                else {
                    Debug.Log("돈이 충분치 않습니다.");
                }
            }
            PlayerPrefs.SetInt("buyAtkSpdCoin", buyAtkSpdCoinInt);
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }     
    }

    // 치명타확률 스탯 구매
    public void buyCritical() {
        if (GoodsManager.coinInt >= buyCriticalCoinInt) {
            if (buyCriticalCoinInt == 2) { // 두번째 버튼 클릭 이 후부터는 2배 증가.
                playerCritical += 0.5f;
                PlayerPrefs.SetFloat("Critical", playerCritical);
                PlayerPrefs.Save();

                int returnedValue_1 = GoodsManager.LostCriticalCoin(GoodsManager.buy2CriticalCoinInt);

                // 리턴된 값으로 buyCriticalCoinInt 갱신
                buyCriticalCoinInt = returnedValue_1;
            }

            else if (PlayerPrefs.GetInt("buyCriticalCoin", buyCriticalCoinInt) >= 4) {
                int returnedValue_2 = GoodsManager.LostCriticalCoin(GoodsManager.buy2CriticalCoinInt);
                if (returnedValue_2 > 0) {
                    playerCritical += 0.5f;
                    PlayerPrefs.SetFloat("Critical", playerCritical);
                    PlayerPrefs.Save();
                }
                
                else {
                    Debug.Log("돈이 충분치 않습니다.");
                }
            }
            PlayerPrefs.SetInt("buyCriticalCoin", buyCriticalCoinInt);
        }
        else {
            Debug.Log("돈이 충분치 않습니다.");
        }
    }
}
