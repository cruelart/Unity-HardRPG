using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSectionScript : MonoBehaviour
{
    SlimeData slimeData;

    float attackTimer = 0.0f; // 공격 재사용 대기시간 측정변수
    float reAttackTimer = 1.3f; // 공격 재사용 대기시간 설정변수

    // Start is called before the first frame update
    void Start()
    {
        slimeData = new SlimeData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            if (PlayerData.playerData.Player_hp >= 0)
            {
                PlayerData.playerData.player_hp -= (PlayerData.playerData.defense_value - slimeData.attack_value);
                PlayerData.playerData.Detail_modifyValue(0, 0, -slimeData.attack_value, 0, 0, 0, 0, 0); // 플레이어 체력감소
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        //if (col.tag == "Player") // 슬라임공격영역과 플레이어가 충돌한다면
        //{
        //    //PlayerFSM player = col.GetComponent<PlayerFSM>();

        //    attackTimer += Time.deltaTime; // 재공격시간

        //    if (attackTimer >= reAttackTimer)
        //    {
        //        attackTimer = 0.0f;
        //    }

        //    if (attackTimer == 0.0f)
        //    {
        //        if (PlayerData.playerData.Player_hp >= 0)
        //        {
        //            PlayerData.playerData.Detail_modifyValue(0, 0, -slimeData.Attack_value, 0, 0, 0,0,0); // 플레이어 체력감소
        //        }
        //    }
        //    //MosnterAttackSection.SetActive(true); // 공격전용 영역 오브젝트 On
        //}
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            attackTimer = 0.0f;
        }
    }
}
