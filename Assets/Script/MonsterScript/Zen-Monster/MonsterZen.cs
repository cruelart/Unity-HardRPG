using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZen : MonoBehaviour
{
    [Header("설정")]
    public GameObject monsterPrefab; // 스폰할 몬스터 프리팹
    public float spawnTime = 3.0f; // 젠 시간 -> 디포트값은 3으로하자 그냥
    public int poolSize = 10; // 젠 될 몬스터의 수

    private Queue<GameObject> monsterPool = new Queue<GameObject>(); // 오브젝트 풀링용 큐

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int monsterNum = 0; monsterNum < poolSize; monsterNum++)
        {
            GameObject Monster = Instantiate(monsterPrefab, this.transform);
            Monster.SetActive(false); // 처음에는 전부 안보이게 그냥 초기 생성용
            monsterPool.Enqueue(Monster);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            
        }
    }

    void SpawnMonster()
    {
        if (monsterPool.Count > 0)
        {
            GameObject monster = monsterPool.Dequeue(); // 몬스터 뺴오고
            monster.transform.position = this.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

            monster.SetActive(true);
            
        }
        else 
        { 

        }
    }
}
