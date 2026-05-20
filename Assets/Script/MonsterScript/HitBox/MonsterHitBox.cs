using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    int monsterAttackValue = 10;

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

    private void OnTriggerEnter(Collider _col)
    {

        Debug.Log("뭔가 충돌하긴함");
        if (_col.gameObject.CompareTag("Player"))
        {
            IT_PlayerDamaged it_playerDamaged = _col.gameObject.GetComponent<IT_PlayerDamaged>();

            if(it_playerDamaged != null)
            {
                Debug.Log("몬스터가 플레이어를 공격성공");
                it_playerDamaged.OnDamaged(monsterAttackValue); // 데미지 100을 입힌다.
            }
        }
    }
}
