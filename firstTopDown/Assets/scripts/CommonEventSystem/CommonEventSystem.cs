using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommonEventSystem : MonoBehaviour
{

    private static CommonEventSystem instance;

    private void Awake() {
        // 이 객체를 파괴하지 않도록 설정
        DontDestroyOnLoad(gameObject);

        // 이미 인스턴스가 존재하는지 확인
        if (instance == null) {
            instance = this;
        }
        
        else {
            Destroy(gameObject);
        }
    }

}
