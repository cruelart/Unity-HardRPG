using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    int monsterAttackValue = 100;

    private void Awake()
    {
        //대충 몬스터의 데이터에서 해당 몬스터의 공격력을 가져오는 로직짤 예정
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(_col.gameObject.CompareTag("Player"))
        {
            IT_PlayerDamaged it_playerDamaged = _col.gameObject.GetComponent<IT_PlayerDamaged>();

            if(it_playerDamaged != null)
            {
                it_playerDamaged.OnDamaged(monsterAttackValue); // 데미지 100을 입힌다.
            }
        }
    }
}
