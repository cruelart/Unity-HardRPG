using System;
using UnityEngine;

public class MonsterDeadInfo
{
    public int monsterID;
    public GameObject monsterObj;
    public MonsterSpawnZone spawnZone;

    public MonsterDeadInfo(int _monsterID, GameObject _monsterObj, MonsterSpawnZone _monsterSpawnZone)
    {
        monsterID = _monsterID;
        spawnZone = _monsterSpawnZone;
        monsterObj = _monsterObj;
    }
}
public static class MonsterEvent
{
    public static Action<MonsterDeadInfo> OnMonsterDead; // 죽은 몬스터의 키 int 값을 받기 ㄱ
}
