using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        MonsterEvent.OnMonsterDead += DropItem;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DropItem(MonsterDeadInfo _monsterDeadInfo)
    {
        List<DropTableEntry> monsterDropList = DropSystem.GetMonsterDropList(_monsterDeadInfo.monsterID);

        foreach(DropTableEntry dropTableEntry in monsterDropList)
        {
            float randomNum = Random.Range(0f, 100f);
            //확률 싸움하기
            if(randomNum <= dropTableEntry.dropPercent)
            {
                //아이템떨구기 코드작성하기
                Debug.Log("몬스터가 사망하여 아이템을 지급합니다");
                continue;
            }
        }
    }
}
