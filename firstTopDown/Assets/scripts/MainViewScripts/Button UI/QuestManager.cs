using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestData {
    public string questTitle;
    public string questContent;
    public string questAward;
    

    public QuestData(string title, string content, string award) {
        questTitle = title;
        questContent = content;
        questAward = award;
    }
}

public class QuestManager : MonoBehaviour
{
    // 일일미션 날짜를 저장할 딕셔너리
    private Dictionary<int, DateTime> dayQuest = new Dictionary<int, DateTime>();
    // 현재날짜를 문자열로 표기함.
    private String questDate = DateTime.Now.ToString("yyyy-MM-dd");

    // 퀘스트데이터 리스트
    private Dictionary<int, QuestData> questList;

    // 퀘스트 진행도를 담을 변수, 퀘스트 클리어 조건을 담을 변수    
    private int questValue;
    private int questClearValue;

    // 현재 진행중인 퀘스트 인덱스를 저장할 변수
    private int questIdx;

    // 퀘스트 초기화할 변수
    private bool questAllClear = false;

    // 퀘스트 텍스트 배열
    private TMP_Text[] awardText;


    // 게임실행시, 맨처음으로 동작함, (현재날짜 정보가 저장되어있는지 확인)
    void Awake() {
        if (!PlayerPrefs.HasKey("firstQuestDate")) {
            PlayerPrefs.SetString("firstQuestDate", questDate);
        }
    }   

    void Start() {
        // awardText 배열의 참조 경로
        awardText = GameObject.Find("EtcUICanvas").transform.Find("EtcPanel").transform.Find("MissionCanvas").transform.Find("MissionBtn")
        .transform.Find("MissionPanel").transform.Find("MissionIMG").transform.Find("MissionCanvasList").transform.Find("MisCanvas1")
        .transform.Find("MisPanel1").transform.Find("fixAwardText1").GetComponentsInChildren<TMP_Text>();
        if(awardText != null) {
            Debug.Log(awardText[0]);
            Debug.Log(awardText[1]);
        }

        // 퀘스트 리스트 변수에 추가할 퀘스트함수(AddQuestList) 실행
        questList = new Dictionary<int, QuestData>();
        AddQuestList();
    }

    void Update() {
        
    }

    // 퀘스트리스트의 키 값을 참조하여, QuestData클래스에 각각의 변수(title, content, award)에 데이터를 저장함.
    // AddQuestList 함수는 퀘스트의 정보를 저장하는 역할을 한다.
    public void AddQuestList() {
        questList.Add(10, new QuestData("몬스터 5마리 잡기", "몬스터 5마리 잡기 0/5", "보상 : 코인 10개"));
        questList.Add(20, new QuestData("웨이브 5단계 진입하기", "웨이브 5단계까지 가기 0/5", "보상 : 코인 10개"));
        questList.Add(30, new QuestData("웨이브 10단계 진입하기", "웨이브 10단계까지 가기 0/10", "보상 : 보석 5개"));
    }

}
