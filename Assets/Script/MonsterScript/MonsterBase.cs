using UnityEngine;
using BehaviorTree;

public abstract class MonsterBase : MonoBehaviour
{
    //------------------------------
    [Header("추격대상")]
    public Transform target; // 테스트용으로 public일단 선언하자

    //-------------------------------
    [Header("인지 범위 설정")]
    [SerializeField]
    protected float viewAngle = 90f;

    //탐지범위
    [SerializeField]
    protected float detectionRange = 10f;

    //공격범위
    [SerializeField]
    protected float attackRange = 2f;

    [SerializeField]
    protected LayerMask playerLayer;
    //-------------------------------

    //피격
    protected bool isHitted = false;

    //블랙보드
    protected Blackboard blackboard = new Blackboard(); // 슬라임에 필요한 데이터를 다루는 블랙보드생성
}
