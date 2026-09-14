using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private int damage;
    private Collider hitCollider;

    private HashSet<IF_OnDamaged> hitTargets = new HashSet<IF_OnDamaged>(); // 중복타격 방지용 Set

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
        hitCollider.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack(int _attackValue) // -> 플레이어 애니메이션 이벤트스크립트에서 호출
    {
        //Debug.Log($"{gameObject.name} ON");
        hitTargets.Clear(); // 기존 처리했던 몬스터 초기화

        damage = _attackValue;
        hitCollider.enabled = true;
    }

    public void EndAttack() // -> 플레이어 애니메이션 이벤트스크립트에서 호출
    {
        //Debug.Log($"{gameObject.name} OFF");
        hitCollider.enabled = false; // 콜리더 끄기
    }

    public void CancelAttack() // 예상치 못한 피격이나 사망으로 애니메이션 끊길때 호출하는 예외 함수
    {
        EndAttack();
        hitTargets.Clear();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if (_col.TryGetComponent<IF_OnDamaged>(out IF_OnDamaged target)) // 공격이 가능한 상대이면
        {
            if (hitTargets.Contains(target)) // 이미 한대 때렸으면 pass하자
                return;

            target.OnDamaged(damage, transform.root.gameObject); // 맞으면 피격처리
            hitTargets.Add(target); // 맞은 애는 중복처리방지를 위해 Set에 넣어줌
        }
    }
}
