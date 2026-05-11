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
        //미리 몬스터들 생성
        for(int monsterNum = 0; monsterNum < poolSize; monsterNum++)
        {
            CreateMonsterInPool();
        }

        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMonsterInPool() // 풀에 들어갈 몬스터 생성
    {
        GameObject Monster = Instantiate(monsterPrefab, this.transform); // 해당 스크립트를 지닌 오브젝트의 하위에 몬스터 생성
        Monster.SetActive(false); // 처음에는 전부 안보이게 그냥 초기 생성용
        monsterPool.Enqueue(Monster);
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTime); // 스폰시간만큼 대기
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        while (monsterPool.Count > 0) // 풀에 아직도 몬스터가 남아있다면
        {
            GameObject monster = monsterPool.Dequeue(); // 몬스터 뺴오고

            // 랜덤위치에 스폰시킴
            monster.transform.position = this.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            monster.SetActive(true);
        }
        //풀에 몬스터가 남아있지 않다는건 필드에 이미 정해둔 수만큼의 몬스터가 젠이 됐기때문에 아무것도 안함
    }
}
