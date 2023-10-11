using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamseStartBtn : MonoBehaviour {

    // 클릭 시, 실행되는 메서드
    public void startBtn() {
        // 씬 전환
        Debug.Log("Scene Switching !");
        SceneManager.LoadScene("MainView");
    }
}
