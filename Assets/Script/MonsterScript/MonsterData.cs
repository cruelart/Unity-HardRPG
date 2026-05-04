using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData
{
    public int monster_level; // 레벨
    public int attack_value; // 공격력
    public int defense_value; // 방어력
    public float monster_hp; // 현재 체력
    public float monster_maxHp; // 최대 체력
    public float visibleRange; // 플레이어 인지거리
    public float attackRange; // 플레이어 공격거리
    public float viewAngle; // 몬스터의 시야각

    public MonsterData()
    {

    }

    //몬스터의 정보를 수정하는 함수
    public void Detail_modifyValue(int _attackValue, int _defanseValue, int _monsterHp) // 세부 강화수치 조정
    {
        attack_value += _attackValue;
        defense_value += _defanseValue;
        monster_hp += _monsterHp;
    }
}
