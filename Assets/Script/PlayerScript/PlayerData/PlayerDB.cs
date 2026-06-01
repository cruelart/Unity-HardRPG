using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerDB
{
    public string playerName; // 이름

    public int level;
    public long maxExp;

    //플레이어 위치
    public string currentMap; // 플레이어가 현재 위치하고 있는 맵
    public float posX;
    public float posY;
    public float posZ;

    public List<Stat> stats;

    public PlayerDB()
    {
        playerName = "New Player";
        level = 1;
        maxExp = 100;

        currentMap = "tutorialMap";
        posX = 0;
        posY = 1.0f;
        posZ = 0;

        stats = new List<Stat>();

        //초기 스텟
        InitStat();
    }

    private void AddStat(StatType _type, float _value)
    {
        stats.Add(new Stat()
        {
            type = _type,
            value = _value
        });
    }

    private void InitStat()
    {
        AddStat(StatType.Attack, 10);
        AddStat(StatType.Defense, 10);
        AddStat(StatType.MoveSpeed, 1);
        AddStat(StatType.CriticalPercent, 0);
        AddStat(StatType.HP, 100);
        AddStat(StatType.MP, 100);
        AddStat(StatType.Accuracy, 10);
        AddStat(StatType.STR, 5);
        AddStat(StatType.DEX, 5);
        AddStat(StatType.INT, 5);
        AddStat(StatType.LUK, 5);
    }
}
