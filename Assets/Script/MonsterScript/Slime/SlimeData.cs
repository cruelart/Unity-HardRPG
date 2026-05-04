using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeData : MonsterData
{
    public SlimeData()
    {
        monster_level = 1;
        attack_value = 10;
        defense_value = 3;
        monster_maxHp = 100;
        monster_hp = 100;
        visibleRange = 60;
        attackRange = 8.0f;
        viewAngle = 150.0f;
    }
}
