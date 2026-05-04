using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCommand : Command
{
    private Animator anime;

    public IdleCommand(Animator _anime)
    {
        anime = _anime;
    }
    public void Idle()
    {
        //대기상태의 애니메이션 재생
        anime.SetBool("isNormalState", true);
        anime.SetBool("isSwordState", false);
        anime.SetBool("isSwordIdle", false);
        anime.SetBool("isIdle", true);
        anime.SetBool("isRun", false);
        anime.SetBool("isWalk", false);
        anime.SetBool("isJump", false);
        anime.SetBool("isMove", false);
        anime.SetBool("isAttack", false);
        anime.SetInteger("isCombo", 0);
        anime.SetBool("isChangeState", false);

    }
    public override void Execute()
    {
        //Debug.Log("일단 idle");
        Idle();
    }
}
