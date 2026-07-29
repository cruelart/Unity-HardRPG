using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    //플레이어의 고정 데이터
    public PlayerSaveData playerBaseDB { get; private set; }

    //플레이어의 유동 데이터
    //public int currentHp { get; private set; }
    //public int currentMp { get; private set; }

    public long maxExp { get; private set; }
    //public long currentExp { get; private set; }

    public int final_attack { get; private set; }

    //public int stat_upgradePossibleValue { get; private set; } // 스탯 업그레이트 가능 횟수

    //이벤트
    public event Action<int> OnLevelUp;
    public event Action<int, int> OnHpChanged;
    public event Action<long, long> OnExpChanged;
    public event Action OnChangeStat;

    private List<Stat> final_statList;
    private Dictionary<StatType, Stat> final_statDict; // 편의를 위한 해시작성

    public void Init(PlayerSaveData _data)
    {
        playerBaseDB = _data;

        //깊은 복사시키기
        final_statList = playerBaseDB.stats.Select(s => new Stat { type = s.type, value = s.value }).ToList();
        final_statDict = final_statList.ToDictionary(s => s.type);

        playerBaseDB.stat_upgradePossibleValue = 5;

        RefreshStats();

        playerBaseDB.currentHp = (int)final_statDict[StatType.HP].value;
        playerBaseDB.currentMp = (int)final_statDict[StatType.MP].value;

        maxExp = playerBaseDB.level * 100;
    }

    public string GetPlayerName()
    {
        return playerBaseDB.playerName;
    }
    public int GetPlayerLv()
    {
        return playerBaseDB.level;
    }

    public Stat GetStat(StatType _type)
    {
        final_statDict.TryGetValue(_type, out var stat);
        return stat;
    }

    public float GetStatValue(StatType _type)
    {
        final_statDict.TryGetValue(_type, out var stat);
        return stat.value;
    }


    public void OnDamaged(int _damage)
    {
        playerBaseDB.currentHp -= _damage;
        OnHpChanged?.Invoke(playerBaseDB.currentHp, (int)GetStatValue(StatType.HP)); // hp 변동사항 알림

        Debug.Log("PlayerStatManager에 있는 OnDamage함수 호출");

        //if(playerHp <= 0)
        //{
        //    Die();
        //}
    }

    public void GetExp(long _exp)
    {
        playerBaseDB.currentExp += _exp;
        //경험치가 너무많이 오르면
        if (maxExp < playerBaseDB.currentExp)
        {
            playerBaseDB.currentExp -= maxExp; // 레벨업했으니 경험치 다시 초기화
            LevelUp();
        }

        OnExpChanged?.Invoke(playerBaseDB.currentExp, maxExp);
    }

    public void LevelUp()
    {
        playerBaseDB.level += 1; // 일단 레벨업하고

        OnLevelUp?.Invoke(playerBaseDB.level); // 이벤트호출 (확성기 날림)

        //대충 레벨업하면 일어나는 일들
        PlusStat(StatType.STR,1); // 테스트용 힘 상승
        playerBaseDB.stat_upgradePossibleValue += 5;

        RefreshStats();

        playerBaseDB.currentHp = (int)final_statDict[StatType.HP].value; // 풀피로 변경
        playerBaseDB.currentMp = (int)final_statDict[StatType.MP].value;

        OnHpChanged?.Invoke(playerBaseDB.currentHp, (int)final_statDict[StatType.MP].value);
    }

    public void UpgradeStatusStat(StatType _statType, int _value)
    {
        if(playerBaseDB.stat_upgradePossibleValue <= 0)
        {
            return;
        }
        playerBaseDB.stat_upgradePossibleValue--;
        playerBaseDB.statDict[_statType].value += _value;
        RefreshStats();
    }

    public void PlusStat(StatType _statType, int _value)
    {
        playerBaseDB.statDict[_statType].value += _value;
        RefreshStats();
    }

    public void MinusStat(StatType _statType, int _value)
    {
        playerBaseDB.statDict[_statType].value -= _value;
        RefreshStats();
    }

    public void RefreshStats()
    {
        CalculateStat();
        //대충 스텟이 변했다는 걸 알려줄 수도 있는 것들을 적는 자리
        OnChangeStat?.Invoke();
    }

    public void CalculateStat()
    {
        final_statDict[StatType.Attack].value = (int)(playerBaseDB.statDict[StatType.Attack].value + playerBaseDB.statDict[StatType.STR].value * 2 + playerBaseDB.statDict[StatType.DEX].value);
        final_statDict[StatType.Defense].value = (int)(playerBaseDB.statDict[StatType.Defense].value + playerBaseDB.statDict[StatType.DEX].value * 2 + playerBaseDB.statDict[StatType.STR].value);
        final_statDict[StatType.Mental].value = (int)(playerBaseDB.statDict[StatType.Mental].value + playerBaseDB.statDict[StatType.INT].value * 2 + playerBaseDB.statDict[StatType.LUK].value);
        final_statDict[StatType.Accuracy].value = (int)(playerBaseDB.statDict[StatType.Accuracy].value + playerBaseDB.statDict[StatType.DEX].value * 1);
        final_statDict[StatType.CriticalPercent].value = (int)(playerBaseDB.statDict[StatType.CriticalPercent].value + playerBaseDB.statDict[StatType.LUK].value * 1);
        final_statDict[StatType.MoveSpeed].value = (int)(playerBaseDB.statDict[StatType.MoveSpeed].value + playerBaseDB.statDict[StatType.DEX].value * 1);
        final_statDict[StatType.Avoidance].value = (int)(playerBaseDB.statDict[StatType.Avoidance].value + playerBaseDB.statDict[StatType.LUK].value * 1);
        final_statDict[StatType.HP].value = (int)(playerBaseDB.statDict[StatType.HP].value + playerBaseDB.statDict[StatType.STR].value * 30);
        final_statDict[StatType.MP].value = (int)(playerBaseDB.statDict[StatType.MP].value + playerBaseDB.statDict[StatType.INT].value * 30);


        final_statDict[StatType.STR].value = (int)(playerBaseDB.statDict[StatType.STR].value);
        final_statDict[StatType.DEX].value = (int)(playerBaseDB.statDict[StatType.DEX].value);
        final_statDict[StatType.INT].value = (int)(playerBaseDB.statDict[StatType.INT].value);
        final_statDict[StatType.LUK].value = (int)(playerBaseDB.statDict[StatType.LUK].value);

        maxExp = playerBaseDB.level * 100;
    }
}
