using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDB
{
    public string playerName; // 이름
    public int level;

    public int MaxHp;
    public int currentHp;

    public int MaxMp;
    public int currentMp;

    public float currentExp;
    public float maxExp;

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

        MaxHp = 100;
        currentHp = MaxHp;

        MaxMp = 100;
        currentMp = MaxMp;

        currentExp = 0;
        maxExp = level * 100;
        currentMap = "tutorialMap";
        posX = 0;
        posY = 1.0f;
        posZ = 0;
        stats = new List<Stat>();
    }
}
