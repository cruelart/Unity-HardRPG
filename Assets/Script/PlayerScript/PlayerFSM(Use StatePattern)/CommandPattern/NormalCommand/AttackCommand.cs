using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : Command
{
    private Animator anime;
    // Start is called before the first frame update
    public AttackCommand(Animator _anime)
    {
        anime = _anime;

        anime.SetBool("isMove", false);
        anime.SetBool("isAttack", true);
        anime.SetBool("isChangeState", true);

    }

    public override void Execute()
    {
        CheckNormalCommand();
        Debug.Log("AttackCommand가 실행");
    }

    private void CheckNormalCommand() // foreach보다 좀 더 빠른 코드 추정
    {
        switch (Attack.attack.normalAttackNum)
        {
            case 0:
                anime.SetInteger("isCombo", 0);
                PlayerData.playerData.player_hungry -= 0.005f;
                return;
            //일반 콤보공격 첫번째 동작
            case 1:
                anime.SetInteger("isCombo", 1);
                PlayerData.playerData.player_hungry -= 0.005f;
                return;
            //두번째 동작
            case 2:
                anime.SetInteger("isCombo", 2);
                PlayerData.playerData.player_hungry -= 0.005f;
                return;
            //세번째 동작
            case 3:
                anime.SetInteger("isCombo", 3);
                PlayerData.playerData.player_hungry -= 0.005f;
                return;
            case 4:
                anime.SetInteger("isCombo", 4);
                PlayerData.playerData.player_hungry -= 0.005f;
                return;
        }
    }
    
}
