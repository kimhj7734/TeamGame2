using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 10;
    private Health health;

    void Start() {
        health = GetComponent<Health>();
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "CharacterCollider") {
            // health = other.GetComponent<Health>();
            
            if (health != null) {   
                
            } 
        }

    }
    
    // void OnTriggerEnter2D(Collider2D other) {
        
    //     // 정중앙 캐릭터와 충돌한 경우
    //     if (other.CompareTag("CharacterCollider"))
    //     {
    //         // 데미지를 입히고 프리팹을 제거
    //         Health health = other.GetComponent<Health>();
    //         if (health != null)
    //         {
    //             health.TakeDamage(damageAmount);
    //         }

    //         // 현재 충돌한 프리팹 제거
    //         Debug.Log("체력 다되서 제거됨...");
    //         //Destroy(gameObject);
    //         //
    //     }
    // }
}



