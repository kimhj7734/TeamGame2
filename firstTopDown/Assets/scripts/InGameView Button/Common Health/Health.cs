using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    public int damage;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log("들어오는 데미지량 : " + damage);
                Die();
            } else 
            {
                Destroy(GameObject.FindGameObjectWithTag("PlayerCharacter"));
            }
    } 

    void Die()
    {
        // 여기에서 프리팹이 파괴되거나 다른 종료 작업을 수행할 수 있습니다.
        Destroy(gameObject);
    }
}