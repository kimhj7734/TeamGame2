using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popImgEvents : MonoBehaviour
{    
    [SerializeField] private GameObject target2;
    private bool popActive = false;

    void Start() {
        // GameObject target2 = GameObject.Find("OptionPop");
        GameObject target = GameObject.FindGameObjectWithTag("popImg");


        // 처음 비활성화
        target2.SetActive(popActive);

        if(target2.activeSelf == false) {
            // popActive = !popActive;
            target2.SetActive(!popActive);   // true
            Debug.Log(target2.activeSelf);
        } else {
            target2.SetActive(popActive);  // false
        }

    }
}
