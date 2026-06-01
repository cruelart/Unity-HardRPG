using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour , IF_OnDamaged
{
    //플레이어의 고정 데이터
    private PlayerDB playerDB;

    //플레이어의 유동 데이터
    private int currentHp;
    private int currentMp;
    private long currentExp;

    //이벤트
    public static event Action<int> OnLevelUp;
    public static event Action<int, int> OnHpChanged;

    private Dictionary<StatType, Stat> statDict; // 편의를 위한 해시작성

    public void Init(PlayerDB _data)
    {
        playerDB = _data;
        statDict = playerDB.stats.ToDictionary(s => s.type); // playerDB를 해시와 연결

        currentHp = (int)statDict[StatType.HP].value;
        currentMp = (int)statDict[StatType.MP].value;
        currentExp = 0;
    }

    public Stat GetStat(StatType _type)
    {
        statDict.TryGetValue(_type, out var stat);
        return stat;
    }

    public float GetStatValue(StatType _type)
    {
        statDict.TryGetValue(_type, out var stat);
        return stat.value;
    }


    public void OnDamaged(int _damage)
    {
        currentHp -= _damage;
        OnHpChanged?.Invoke(currentHp, (int)GetStatValue(StatType.HP)); // hp 변동사항 알림

        Debug.Log("PlayerStatManager에 있는 OnDamage함수 호출");

        //if(playerHp <= 0)
        //{
        //    Die();
        //}
    }

    public void GetExp(int _exp)
    {
        currentExp += _exp;
        //경험치가 너무많이 오르면
        if (playerDB.maxExp < currentExp)
        {
            LevelUp();
            currentExp -= playerDB.maxExp; // 레벨업했으니 경험치 다시 초기화
        }
    }

    public void LevelUp()
    {
        playerDB.level += 1; // 일단 레벨업하고
        OnLevelUp?.Invoke(playerDB.level); // 이벤트호출 (확성기 날림)

        //대충 스텟이 올라가는 로직
        GetStat(StatType.DEX).value += 1; // 테스트용 덱스 상승
    }
}
