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

    [Header("속도 조절")]
    public float moveSpeed = 2.0f;

    //몬스터 애니메이션
    protected Animator animator;
    private string currentAnime_name = ""; // 실행 중인 애니메이션 저장

    //몬스터 RigidBody
    protected Rigidbody rigid;

    //블랙보드
    protected Blackboard blackboard = new Blackboard(); // 슬라임에 필요한 데이터를 다루는 블랙보드생성

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // X축과 Z축 회전을 막아서 이상한 회전 방지하기

        blackboard.SetData("IsHitted", false);
        blackboard.SetData("Target", null);
        blackboard.SetData("IsDead", false);
    }

    //애니메이션 재생 함수
    protected void PlayAnime(string _anime_name, float crossFade)
    {
        if (currentAnime_name == _anime_name) return; // 애니메이션이 같으면 바꿀필요 없음

        animator.CrossFade(_anime_name, crossFade); // 애니메이션간 부드럽게 교체하고
        currentAnime_name = _anime_name; // 실행 중인 애니메이션 교체
    }

    //애니메이션이 끝났는지 체크
    protected bool IsAnimationFinished(string _anime_name, float stopTime)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.IsName(name) && info.normalizedTime >= stopTime;
    }

}
