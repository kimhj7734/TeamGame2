using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameStart : MonoBehaviour
{
    public void InGameStartBtn() {
        // 씬 전환
        SceneManager.LoadScene("InGameScene");
    }
}
