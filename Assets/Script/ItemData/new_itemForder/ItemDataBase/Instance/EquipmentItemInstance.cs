using System.Collections.Generic;
using UnityEngine;

//장비 아이템 인스턴스 스크립트
public class EquipmentItemInstance
{
    public EquipmentItemDB settings;
    //public ItemRawData data; // Json에서 받아올 데이터 수치를 기록할거임

    //실시간 가변변수들은 인스턴스에서 관리
    public int upgradeLv = 0; // 강화 단계
    public int durability = 0; // 내구도

    //몬스터 드랍 용도
    public EquipmentItemInstance(EquipmentItemDB _EDB)
    {
        this.settings = _EDB;
        //this.data = _raw;
        durability = UnityEngine.Random.Range(50, 101); // 50~ 100까지의 랜덤한 내구도 제공
    }

    //퀘스트지급이나 확정지급일 경우 내구도 조절해서 주는 용도의 함수
    public EquipmentItemInstance(EquipmentItemDB _EDB, int _durability)
    {
        this.settings = _EDB;
        //this.data = _raw;
        this.durability = _durability;
    }

    //장비 강화 함수
    public void Upgrade()
    {
        upgradeLv++; // 장비 레벨 상승
    }

    public float GetStat(StatType _type)
    {
        Stat baseStat = settings.stats.Find(stat => stat.type == _type);
        if (baseStat != null) return 0;

        return baseStat.value + (upgradeLv * 5.0f); // 기본 스텟 + 추가 스텟합산 반환하기
    }
}
