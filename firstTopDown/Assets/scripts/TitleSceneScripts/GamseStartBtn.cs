using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamseStartBtn : MonoBehaviour {

    private Button target;

    void Start() {
        target = GameObject.Find("GameStartCanvas").GetComponentInChildren<Button>();

    }

    // 클릭 시, 실행되는 메서드
    public void startBtn() {
        // 씬 전환
        SceneManager.LoadScene("MainView");
    }
}
