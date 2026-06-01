using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterPoolData
{
    public MonsterType monsterType;
    public GameObject monsterObj; // 몬스터 프리팹
    public int poolSize = 10;
}

//모든 몬스터의 풀을 들고있음
public class MonsterZen : MonoBehaviour
{
    //[Header("설정")]
    //public GameObject monsterPrefab; // 스폰할 몬스터 프리팹
    //public float spawnTime = 3.0f; // 젠 시간 -> 디포트값은 3으로하자 그냥
    //public int poolSize = 10; // 젠 될 몬스터의 수

    public static MonsterZen instance;

    [SerializeField]
    private List<MonsterPoolData> monsterPoolDataList;

    private Dictionary<MonsterType, Queue<GameObject>> monsterPoolMap = new Dictionary<MonsterType, Queue<GameObject>>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        CreateMonsterInPool();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMonsterInPool() // 풀에 들어갈 몬스터 생성
    {
        //GameObject Monster = Instantiate(monsterPrefab, this.transform); // 해당 스크립트를 지닌 오브젝트의 하위에 몬스터 생성
        //Monster.SetActive(false); // 처음에는 전부 안보이게 그냥 초기 생성용
        //monsterPool.Enqueue(Monster);

        foreach(MonsterPoolData data in monsterPoolDataList)
        {
            Queue<GameObject> monster_pool = new Queue<GameObject>();

            for(int i = 0; i < data.poolSize; i++)
            {
                GameObject monster = Instantiate(data.monsterObj, this.transform);

                monster.SetActive(false);

                MonsterBase monsterBaseComp = monster.GetComponent<MonsterBase>();

                //Debug.Log("뭉탱이:" + data.monsterType);
                monsterBaseComp.SetMonsterType(data.monsterType);

                monster_pool.Enqueue(monster);
            }

            monsterPoolMap.Add(data.monsterType, monster_pool);
        }
    }

    public void SpawnMonster(MonsterType _monsterType, Transform _transform)
    {
        Queue<GameObject> pool = monsterPoolMap[_monsterType];

        if(pool.Count <= 0)
        {
            return; // 이미 모든 몬스터를 내보냈으므로 return
        }

        while (pool.Count > 0)
        {
            GameObject monster = pool.Dequeue();

            monster.transform.position = _transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            monster.SetActive(true);

        }
        //while (monsterPool.Count > 0) // 풀에 아직도 몬스터가 남아있다면
        //{
        //    GameObject monster = monsterPool.Dequeue(); // 몬스터 뺴오고

        //    // 랜덤위치에 스폰시킴
        //    monster.transform.position = this.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        //    monster.SetActive(true);
        //}
        //풀에 몬스터가 남아있지 않다는건 필드에 이미 정해둔 수만큼의 몬스터가 젠이 됐기때문에 아무것도 안함
    }

    public void ReturnMonster(MonsterType _monsterType, GameObject _monster)
    {
        _monster.SetActive(false); // 비활성화 하고
        monsterPoolMap[_monsterType].Enqueue(_monster); // 다시 넣어줌
    }
}
