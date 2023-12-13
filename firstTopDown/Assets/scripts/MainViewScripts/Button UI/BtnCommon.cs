using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCommon : MonoBehaviour
{
    /*
        버튼 클릭(탭) 딜레이
        각 버튼의 활성/비활성
    */

    [SerializeField] private GameObject target1;

    // 이미지팝업이 활성화 되어있을 때, ETC Btn 숨기기
    private Button[] btnTarget = new Button[3];

    // 출석체크별(chkCanvas > CheckIMG > BtnCanvasList) 1~31일까지 있어야 하므로, 31개 필요.
    private Button[] chkBtnTarget = new Button[31];

    /* 각 버튼별 빈곳클릭 or 확인버튼 인덱스를 담을 배열 */
    private Button[] optionCloseBtn;
    private Button[] checkCloseBtn;
    private Button[] missionCloseBtn;

    /* 
        출석체크 closeBtn
        공통 버튼이기 때문에 1개만 있으면됨.
        [SerializeField] 으로 타겟 설정필요함. (경로수정필요)
        일일이 31개변수를 지정하면 복잡해지기때문
    */
    private Button[] popupCloseBtn;
    private int day5count = 4; // 인덱스상으로는 4번째 버튼, 게임화면 상으로는 5번째 버튼
    
    // 출석체크 시 보상할 코인, 보석 변수
    private int dayCoin;
    private int dayGem;

    // 출석체크 날짜 계산 (현재날짜와 비교, 동적으로 비교함)
    private Dictionary<int, DateTime> buttonDates = new Dictionary<int, DateTime>();

    // 현재날짜를 문자열로 표기함.
    private String nowDate = DateTime.Now.ToString("yyyy-MM-dd");


    private bool btnActive = true;

    public float clickDelay = 0.5f; // 클릭 딜레이 시간
    private float lastClickTime = 0f;

    // 현재 날짜정보를 저장함. (하지만 이 부분은 Start 메서드가 호출될 때마다 실행되므로 한 번만 실행되도록 수정이 필요)
    void Awake() {
        // PlayerPrefs에 저장된 날짜가 없다면 현재 날짜 저장, PlayerPrefs.HasKey() <-- 키 값이 있는지 확인하는 기능
        if (!PlayerPrefs.HasKey("LastActiveDate")) {
            PlayerPrefs.SetString("LastActiveDate", nowDate);          
        }
    }

    void Start() {
        PlayerPrefs.SetInt("dayCount", day5count);
        // btnTarget = GameObject.FindWithTag("commonBtn").GetComponent<Button>(); // 비활성화 오브젝트 못 찾음, NullException 뜸, 즉 항상 활성화여야 함.
        
        // EtcUICanvas 하위의 EtcPanel 하위에 있는 모든 Button 컴포넌트 찾음.
        btnTarget = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel").GetComponentsInChildren<Button>();

        // 출석체크 하위의 Button 컴포넌트
        /*********************************************************** 출석체크 버튼 배열 ****************************************************/
        /*
            chkBtnTarget[31] = 
                1 ~ 31일 까지의 버튼이 필요.
            chkBtnTarget 배열을 확인하여 인덱스에 해당되는 각 버튼의 분기점.
        */
        chkBtnTarget = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("ChkCanvas").transform.Find("CheckBtn").transform.Find("CheckPanel").transform.Find("CheckIMG")
                    .transform.Find("BtnCanvasList").GetComponentsInChildren<Button>();                                 
        if (chkBtnTarget != null) {
            for (int i = 0; i < chkBtnTarget.Length; i++) {
                // 처음에 모든 버튼 비활성화
                chkBtnTarget[i].interactable = false;
            }

            int activeButtonIndex = GetActiveButtonIndex();
            // 조건문에 해당되는 인덱스(버튼)만 활성화.
            if (activeButtonIndex >= 0 && activeButtonIndex < chkBtnTarget.Length) {
                chkBtnTarget[activeButtonIndex].interactable = true;
                // 기존에 등록된 이벤트 핸들러 제거 (다중호출방지)
                chkBtnTarget[activeButtonIndex].onClick.RemoveAllListeners();
                // 새로운 이벤트 핸들러 등록
                chkBtnTarget[activeButtonIndex].onClick.AddListener(() => popupExitBtnClick(activeButtonIndex));
            }
        }
        /*********************************************************** 출석체크 버튼 배열 ****************************************************/

        /* 각 버튼별 부모 하위 객체(자식)의 버튼 컴포넌트 경로, 버튼 컴포넌트들을 배열로 담음 */
        /* OptionBtn > closeBtn , OptionImg > confirmBtn */
        // optionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
        //             .transform.Find("OptionBtn").transform.Find("OptionPanel").GetComponentsInChildren<Button>();

        optionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("OptCanvas").transform.Find("OptionBtn").transform.Find("OptionPanel").GetComponentsInChildren<Button>();


        /* CheckBtn > closeBtn , CheckIMG > confirmBtn */
        // checkCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
        //             .transform.Find("CheckBtn").transform.Find("CheckPanel").GetComponentsInChildren<Button>();

        // closeBtn, confirmBtn
        checkCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("ChkCanvas").transform.Find("CheckBtn").transform.Find("CheckPanel").GetComponentsInChildren<Button>();


        /* CheckBtn > 출석체크 클로즈버튼 */
        popupCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("ChkCanvas").transform.Find("CheckBtn").transform.Find("CheckPanel").transform.Find("CheckIMG")
                    .transform.Find("BtnCanvasList").transform.Find("chkBtnCanvas1")
                    .transform.Find("ChkBtn1").transform.Find("chkPopupPanel1").GetComponentsInChildren<Button>();
        // if(popupCloseBtn != null) {
        //     int i;
        //     for(i = 0; i < popupCloseBtn.Length; i++)
        //     Debug.Log(popupCloseBtn[i]);
        // }                                 

        /* MissionBtn > closeBtn , MissionIMG > confirmBtn */
        // missionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
        //             .transform.Find("MissionBtn").transform.Find("MissionPanel").GetComponentsInChildren<Button>();

        missionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("MissionCanvas").transform.Find("MissionBtn").transform.Find("MissionPanel").GetComponentsInChildren<Button>();        
        
        /*
            btnTarget[] = 0 : OptionBtn , 1 : CheckBtn , 2 : MissonBtn
            btnTarget 배열을 확인하여 인덱스에 해당되는 각 버튼의 분기점.
        */
        for (int i = 0; i < btnTarget.Length; i++) {
            switch (i) {
                case 0:
                    /*
                        OptionBtn
                        OptionBtn에 대한 동작을 추가
                        OptionBtn 버튼 눌렀을 때, 나머지 버튼 비활성화
                    */
                    int buttonIndex1 = i;
                    btnTarget[i].onClick.AddListener(() => OptionBtnClick(buttonIndex1));
                    break;
                case 1:
                    /*
                        CheckBtn
                        CheckBtn에 대한 동작을 추가
                        CheckBtn 제외 나머지 비활성화
                    */
                    int buttonIndex2 = i;
                    btnTarget[i].onClick.AddListener(() => OptionBtnClick(buttonIndex2));
                    break;
                case 2:
                    /*
                        MissonBtn
                        MissonBtn에 대한 동작을 추가
                        MissonBtn 제외 나머지 비활성화
                    */
                    int buttonIndex3 = i;
                    btnTarget[i].onClick.AddListener(() => OptionBtnClick(buttonIndex3));
                    break;
                default:
                    // 기타 버튼에 대한 처리
                    int buttonIndex4 = i;
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /* OptionBtn , CheckBtn , MissionBtn Start */
        /* OptionBtn > closeBtn , confirmBtn 기능 */
        for (int a = 0; a < optionCloseBtn.Length; a++) {
            switch (a) {
                case 0:
                    /*
                        OptionBtn > closeBtn : closeBtn[0] 동작
                    */
                    int btnIndex1 = a;
                    optionCloseBtn[a].onClick.AddListener(() => optionExitBtnClick(btnIndex1));
                    break;
                
                case 1:
                    /*
                        OptionImg > confirmBtn : confirmBtn 동작
                    */
                    int btnIndex2 = a;
                    optionCloseBtn[a].onClick.AddListener(() => optionExitBtnClick(btnIndex2));
                    break;
                
                default:
                    break;
            }
            
        }

        /* CheckBtn > closeBtn , confirmBtn 기능 */
            for (int a = 0; a < checkCloseBtn.Length; a++) {
                switch (a) {
                    case 0:
                        /*
                            CheckBtn > closeBtn : closeBtn[0] 동작
                        */
                        int btnIndex1 = a;
                        checkCloseBtn[a].onClick.AddListener(() => checkExitBtnClick(btnIndex1));
                        break;

                    case 1:
                        /*
                            CheckImg > confirmBtn : confirmBtn 동작
                        */
                        int btnIndex2 = a;
                        checkCloseBtn[a].onClick.AddListener(() => checkExitBtnClick(btnIndex2));
                        break;

                    default:
                        break;
                }

            }

        /* MissionBtn > closeBtn , confirmBtn 기능 */
            for (int a = 0; a < missionCloseBtn.Length; a++) {
                switch (a) {
                    case 0:
                        /*
                            MissionBtn > closeBtn : closeBtn[0] 동작
                        */
                        int btnIndex1 = a;
                        missionCloseBtn[a].onClick.AddListener(() => missionExitBtnClick(btnIndex1));
                        break;

                    case 1:
                        /*
                            MissionImg > confirmBtn : confirmBtn 동작
                        */
                        int btnIndex2 = a;
                        missionCloseBtn[a].onClick.AddListener(() => missionExitBtnClick(btnIndex2));
                        break;

                    default:
                        break;
                }

            }

        /* chkBtnCanvas1 > chkBtn1 > closeBtn , confirmBtn 기능 */
        for (int a = 0; a < popupCloseBtn.Length; a++) {
            switch (a) {
                case 0:
                    /*
                        chkBtn1 > closeBtn : closeBtn[0] 동작
                    */
                    int btnIndex1 = a;
                    popupCloseBtn[a].onClick.AddListener(() => popupExitBtnClick(btnIndex1));

                    // 기존에 등록된 이벤트 핸들러 제거 (다중호출방지)
                    popupCloseBtn[a].onClick.RemoveAllListeners();
                    break;

                default:
                    break;
            }

        }
        
    }
    /* Start() End */
    /* OptionBtn > closeBtn , confirmBtn End */
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    /* 각 버튼별 상태전환 기능 */
    public void OptionBtnClick(int buttonIndex) {
        // 클릭된 버튼을 제외한 나머지 버튼의 상태를 업데이트
        for (int i = 0; i < btnTarget.Length; i++) {
            
            if (i == buttonIndex) {
                btnTarget[i].interactable = btnActive; // 클릭된 버튼 상태를 반전
                
            } else {
                btnTarget[i].interactable = !btnActive; // 나머지 버튼 상태를 반전

            }
        }

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* closeBtn, confirmBtn Start */

    /* optionBtn 빈 곳, 확인 버튼 눌렀을 때, 활성 or 비활성 기능 */
    public void optionExitBtnClick(int btnIndex) {
        for (int i = 0; i < optionCloseBtn.Length; i++) {
            if (i == btnIndex) {   // closeBtn or confirmBtn 을 눌렀을 때, btnTarget 모두 활성화.
                for (int a = 0; a < btnTarget.Length; a++) {
                    btnTarget[a].interactable = btnActive;
                }
            } 
        }
    }

    /* checkBtn 빈 곳, 확인 버튼 눌렀을 때, 활성 or 비활성 기능 */
    public void checkExitBtnClick(int btnIndex) {
        for (int i = 0; i < checkCloseBtn.Length; i++) {
            if (i == btnIndex) {   // closeBtn or confirmBtn 을 눌렀을 때, btnTarget 모두 활성화.
                for (int a = 0; a < btnTarget.Length; a++) {
                    btnTarget[a].interactable = btnActive;
                }
            }
        }
    }    

    /* missionBtn 빈 곳, 확인 버튼 눌렀을 때, 활성 or 비활성 기능 */
    public void missionExitBtnClick(int btnIndex) {
        for (int i = 0; i < missionCloseBtn.Length; i++) {
            if (i == btnIndex) {   // closeBtn or confirmBtn 을 눌렀을 때, btnTarget 모두 활성화.                
                for (int a = 0; a < btnTarget.Length; a++) {
                    btnTarget[a].interactable = btnActive;                    
                }

            }
        }
    }

    /* popupCloseBtn 빈 곳 눌렀을 때, 활성 or 비활성 기능 */
    public void popupExitBtnClick(int btnIndex) {
        for (int i = 0; i < chkBtnTarget.Length; i++) {
            if (i == btnIndex) {
                dayCoin += 10;  // 코인 10개 보상
                GoodsManager.coinInt += dayCoin;
                PlayerPrefs.SetInt("Coin", GoodsManager.coinInt);
                
                // 이미 보상받은 시점에서, 버튼 비활성화 처리.
                chkBtnTarget[i].interactable = false;
                day5count++;
                PlayerPrefs.SetInt("dayCount", day5count);
            }
            
            // dayCount 가 4이고 버튼의 인덱스를 나눴을 때, 4의 배수일 때마다(화면상에서는 5번째 버튼에 해당), 보석 보상
            else if (PlayerPrefs.GetInt("dayCount", day5count) % i == 0 && i == btnIndex) {
                dayGem += 3; // 보석 3개 보상
                GoodsManager.gemInt += dayGem;
                PlayerPrefs.SetInt("Gem", GoodsManager.gemInt);
                // 이미 보상받은 시점에서, 버튼 비활성화 처리.
                chkBtnTarget[i].interactable = false;
                day5count++;
                PlayerPrefs.SetInt("dayCount", day5count);
            }

            // 일일보상 다 받았다면(화면상 버튼 30번까지, 인덱스상으로는 32까지) dayCount 처음으로 초기화, 다음달에 다시 보상받을수 있게,
            else if (day5count == 32) { // 조건식 or 연산자 추가필요, 날짜가 다음달로 넘어갔는지에 대한 식 필요.
                day5count = 4;
                PlayerPrefs.SetInt("dayCount", day5count);
                // 날짜도 초기화 필요.
            }
        }
        
    }
    
    // 현재 날짜에 따라 활성화되어야 하는 버튼의 인덱스를 반환하는 함수
    private int GetActiveButtonIndex() {
        // 저장된 날짜 불러오기
        string savedDate = PlayerPrefs.GetString("LastActiveDate", nowDate);
        DateTime currentDate = DateTime.Now;
        // 저장된 날짜 정보가 딕셔너리에 없으면 추가 (딕셔너리 날짜정보 업데이트)
        if (!buttonDates.ContainsValue(currentDate)) {
            int newIndex = buttonDates.Count;
            buttonDates.Add(newIndex, currentDate);
        }

        // 최신 날짜를 딕셔너리에 추가 또는 갱신
        int lastIndex = buttonDates.Keys.Max() + 1;
        buttonDates[lastIndex] = currentDate;

        foreach (var entry in buttonDates) {
            if (currentDate == entry.Value && savedDate == entry.Value.ToString("yyyy-MM-dd")) {        
                // Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");  // key 0, value 2023.12.13  
                return entry.Key;
            }
        }

        // 최신 날짜를 딕셔너리에 추가 또는 갱신
        // int lastIndex = 0;
        // if (buttonDates.ContainsKey(lastIndex)) {
        //     lastIndex = buttonDates.Keys.Max() + 1;
        // }
        // buttonDates[lastIndex] = currentDate;

        return -1;
    }        

    /* closeBtn, confirmBtn End */
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // 메뉴 버튼 클릭 딜레이 설정
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
