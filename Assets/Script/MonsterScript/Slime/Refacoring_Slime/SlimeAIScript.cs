using UnityEngine;
using BehaviorTree; // 만든 커스텀 테스트용 행동트리 사용예정

public class SlimeAIScript : MonsterBase, IMonsterDamageable
{
    //먼저 슬라임의 행동트리 구성
    //대기, 공격, 사망, 이동, 피격

    //몬스터 AI
    private Node root; // 루트노드

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectorNode rootSelector = new SelectorNode();
        //root = new SelectorNode(); // 처음엔 설렉터로 가자 -> 이렇게하면 root의 자료형이 Node이기 때문에 ListAdd호출이 불가능

        //공격노드 생성
        ConditionNode attackNode = new ConditionNode(
            () => IsTargetInRange(attackRange), new ActionNode(Attack) // 너무 가까워지면 바로 공격
            );

        //추적노드 생성
        ConditionNode chaseNode = new ConditionNode(
            () => ViewAngle.isFindPlayer(detectionRange, viewAngle, target.transform, transform) , new ActionNode(Chase)
            );

        ActionNode idleNode = new ActionNode(Idle);

        rootSelector.ListAdd(attackNode);
        rootSelector.ListAdd(chaseNode);
        rootSelector.ListAdd(idleNode);

        root = rootSelector; // 주소넘겨주기
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }

    void OnDamaged(int _damage, Vector3 _attackerPos)
    {
        return;
    }

    //공격, 추격 범위확인 함수
    bool IsTargetInRange(float _range)
    {
        if (target == null)
            return false;

        return Vector3.Distance(this.transform.position, target.position) < _range;
    }

    tasks Attack()
    {
        return tasks.Success;
    }

    tasks Chase()
    {
        //먼저 플레이어를 바라보는 방향으로 전환
        //전환 후 실제 플레이어의 위치로 이동하는 로직
        return tasks.Running;
    }

    tasks Idle()
    {
        return tasks.Success;
    }

    tasks React()
    {
        return tasks.Success;
    }
}
