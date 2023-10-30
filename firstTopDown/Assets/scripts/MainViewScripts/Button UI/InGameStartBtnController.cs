using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameStartBtnController : MonoBehaviour
{
    private Image target1;         // ShopIMG
    private Image target2;         // StatIMG
    private Button target3;        // InGameStartBtn
    private GameObject target4;    // EtcCanvas
    
    /* Canvas 하위에 있는 오브젝트 */
    void Start() {
        target1 = GameObject.Find("BtnCanvas").transform.Find("ShopBtn").transform.Find("ShopCanvas").transform.Find("ShopIMG").GetComponent<Image>();
        target2 = GameObject.Find("BtnCanvas").transform.Find("StatBtn").transform.Find("StatCanvas").transform.Find("StatIMG").GetComponent<Image>();
        target3 = GameObject.Find("InGameStartBtn").GetComponent<Button>();
        target4 = GameObject.Find("EtcCanvas");

        // 처음엔 버튼 활성화
        target3.GetComponent<Button>().interactable = true;
    
    }

    /* IMG(ShopIMG or StatIMG)오브젝트가 활성화 되면, 게임시작 버튼, EtcCanvas(MenuBtn) 비활성화 처리 */
    public void ActiveUpdate() {
        bool imagesActive = target1.gameObject.activeSelf || target2.gameObject.activeSelf;
        if (imagesActive && target3.gameObject.activeSelf) {
            target3.GetComponent<Button>().interactable = false;
            target4.SetActive(false);
        }        

        else {
            target3.GetComponent<Button>().interactable = true;
            target4.SetActive(true);
        }

    }

}
