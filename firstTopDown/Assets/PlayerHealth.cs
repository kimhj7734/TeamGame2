using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    private int currentHealth; // 현재 체력

    private void Start()
    {
        currentHealth = maxHealth; // 시작 시 현재 체력을 최대 체력으로 설정
    }

    public void TakeDamage(int damageAmount)
    {
        // 플레이어의 체력을 최대 체력으로 설정 (무한 체력)
        currentHealth = maxHealth;
        // 여기에 다른 데미지 관련 로직을 추가할 수도 있음
    }
}
