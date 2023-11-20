using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabList;
    public int numberOfPrefabsToSpawn = 10; // 생성할 프리팹의 수 100
    public float spawnRadius = 2.0f; // 생성 반경
    public float spawnInterval = 1.0f; // 생성 간격 (초)

    private int spawnedCount = 0; // 이미 생성한 프리팹 수
    public float moveSpeed =  0.0005f; // 이동 속도
    private float lastSpawnTime = 0;

    private bool canSpawn = true;

    public int damageAmount = 10;
    
    void Start()
    {
        lastSpawnTime = Time.time; // 시작 시간 초기화
        
    }

    void Update()
    {
        if (spawnedCount < numberOfPrefabsToSpawn && Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnPrefab();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnPrefab()
    {
        /* 좌표값 */
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        float randomY = Random.Range(-2.5f, 2.5f); // 여기서 minY와 maxY는 y 좌표의 범위
        float randomX = Random.Range(-2, 2); // 여기서 minZ와 maxZ는 z 좌표의 범위
        Vector3 spawnPosition = transform.position + new Vector3(randomX * spawnRadius, randomY * spawnRadius, 0);
        
        /* 난수 생성 */
        // int randomIndex = Random.Range(0, prefabList.Length);
        

        /* 프리팹 생성 */
        if (canSpawn) {
            
            for (int randomIndex = Random.Range(0, prefabList.Length); randomIndex < prefabList.Length; randomIndex++) {
                // 프리팹 생성
                GameObject newPrefab = Instantiate(prefabList[randomIndex], spawnPosition, Quaternion.identity);
                StartCoroutine(MovePrefabToZero(newPrefab.transform));
                spawnedCount++;
                
                // 이미 모든 프리팹을 생성했으면 종료
                if (spawnedCount >= numberOfPrefabsToSpawn) {
                    // canSpawn 상태 업데이트
                    canSpawn = false;
                    
                    GameObject prefabsList = GameObject.Find("prefabsList").GetComponent<GameObject>();
                    Debug.Log(prefabsList);

                    return;
                }
            }
        }

        // if (canSpawn)
        // {
        //     canSpawn = false;
        //     // 프리팹 생성
        //     // GameObject newPrefab = Instantiate(prefabList[randomIndex], spawnPosition, Quaternion.identity);
        //     StartCoroutine(MovePrefabToZero(newPrefab.transform));
        //     spawnedCount++;
        // }

        // spawnedCount++;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CharacterCollider"))
        {
            // 정중앙 캐릭터와 충돌한 경우
            // 데미지를 입히고 정중앙 플레이어를 파괴하지 않음
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
        else if (other.CompareTag("Prefab"))
        {
            // 프리팹과 충돌한 경우
            // 데미지를 입히고 프리팹을 제거
            Health prefabHealth = other.GetComponent<Health>();
            if (prefabHealth != null)
            {
                prefabHealth.TakeDamage(damageAmount);
            }
            Destroy(other.gameObject);
        }
    }

        // 프리팹을 목표 위치로 천천히 이동시키는 코루틴
    IEnumerator MovePrefabToZero(Transform prefabTransform)
    {
        Vector3 targetPosition = Vector3.zero;
        float elapsedTime = 0f;

        while (prefabTransform != null)
        {
            prefabTransform.position = Vector3.Lerp(prefabTransform.position, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
}