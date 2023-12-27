using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBtn : MonoBehaviour {
    public void GameExitBtn() {
        // 씬 전환
        SceneManager.LoadScene("TitleScene");
    }
}
