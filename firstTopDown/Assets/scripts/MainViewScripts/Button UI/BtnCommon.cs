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

    /* 각 버튼별 빈곳클릭 or 확인버튼 인덱스를 담을 배열 */
    private Button[] optionCloseBtn;
    private Button[] checkCloseBtn;
    private Button[] missionCloseBtn;

    private bool btnActive = true;

    public float clickDelay = 0.5f; // 클릭 딜레이 시간
    private float lastClickTime = 0f;

    void Start() {
        // btnTarget = GameObject.FindWithTag("commonBtn").GetComponent<Button>(); // 비활성화 오브젝트 못 찾음, NullException 뜸, 즉 항상 활성화여야 함.
        
        // EtcUICanvas 하위의 EtcPanel 하위에 있는 모든 Button 컴포넌트 찾음.
        btnTarget = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel").GetComponentsInChildren<Button>();

        /* 각 버튼별 부모 하위 객체(자식)의 버튼 컴포넌트 경로, 버튼 컴포넌트들을 배열로 담음 */
        /* OptionBtn > closeBtn , OptionImg > confirmBtn */
        optionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("OptionBtn").transform.Find("OptionPanel").GetComponentsInChildren<Button>();

        /* CheckBtn > closeBtn , CheckIMG > confirmBtn */
        checkCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("CheckBtn").transform.Find("CheckPanel").GetComponentsInChildren<Button>();

        /* MissionBtn > closeBtn , MissionIMG > confirmBtn */
        missionCloseBtn = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel")
                    .transform.Find("MissionBtn").transform.Find("MissionPanel").GetComponentsInChildren<Button>();

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
            for (int a = 0; a < checkCloseBtn.Length; a++) {
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
