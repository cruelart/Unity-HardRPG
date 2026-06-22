using System.Collections.Generic;
using UnityEngine;

public class MonsterDBManager : MonoBehaviour
{
    public static MonsterDBManager instance;
    //모든 몬스터의 데이터를 담을 해시테이블 선언
    public Dictionary<int, MonsterDB> monsterDBMap = new Dictionary<int, MonsterDB>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.root.gameObject); // 파괴 방지

        LoadData();
    }

    private void LoadData()
    {
        MonsterDB[] monster = Resources.LoadAll<MonsterDB>("monsters");

        foreach(MonsterDB monsterDB in monster)
        {
            if(monsterDBMap.ContainsKey(monsterDB.monsterID))
            {
                continue;
            }

            monsterDBMap.Add(monsterDB.monsterID, monsterDB);
        }
    }
}
