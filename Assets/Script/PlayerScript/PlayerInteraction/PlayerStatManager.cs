using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour , IT_PlayerDamaged
{
    private PlayerDB playerDB;

    //이벤트
    public event Action<int> OnLevelUp;
    public event Action<float> OnHpChanged;

    private Dictionary<StatType, Stat> statDict; // 편의를 위한 해시작성

    void Start()
    {
        statDict = playerDB.stats.ToDictionary(s => s.type); // playerDB를 해시와 연결
    }

    public void Init(PlayerDB _data)
    {
        playerDB = _data;
    }

    public Stat GetStat(StatType _type)
    {
        statDict.TryGetValue(_type, out var stat);
        return stat;
    }


    public void OnDamaged(int _damage)
    {
        playerDB.currentHp -= _damage;
        OnHpChanged?.Invoke(playerDB.currentHp); // hp 변동사항 알림

        //if(playerHp <= 0)
        //{
        //    Die();
        //}
    }

    public void GetExp(int _exp)
    {
        playerDB.currentExp += _exp;
        //경험치가 너무많이 오르면
        if (playerDB.maxExp < playerDB.currentExp)
        {
            LevelUp();
            playerDB.currentExp -= playerDB.maxExp; // 레벨업했으니 경험치 다시 초기화
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
