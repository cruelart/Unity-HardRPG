using UnityEngine;
using BehaviorTree;

public class MonsterBase : MonoBehaviour
{
    private MonsterStatManager stat; // 구독할 곳
    private MonsterAIController ai;

    private MonsterType monsterType;

    public void Init()
    {
        stat = GetComponent<MonsterStatManager>();
        ai = GetComponent<MonsterAIController>();

        stat.OnDeath += HandleDeath;
    }

    public void SetMonsterType(MonsterType _monsterType)
    {
        monsterType = _monsterType;
    }
    //몬스터의 자체 상태를 관리
    private void HandleDeath()
    {
        if(ai == null)
        {
            return;
        }
        ai.enabled = false;

        MonsterZen.instance.ReturnMonster(monsterType, gameObject);
    }
}
