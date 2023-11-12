using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (gameObject.CompareTag("Player")) // 플레이어인 경우만 체력 감소 적용
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    } 

    void Die()
    {
        // 여기에서 프리팹이 파괴되거나 다른 종료 작업을 수행할 수 있습니다.
        Destroy(gameObject);
    }
}