using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdleCommand : Command
{
    private Animator anime;

    public SwordIdleCommand(Animator _anime)
    {
        anime = _anime;
    }
    public void SwordIdle()
    {
        anime.SetBool("isNormalState", false);
        anime.SetBool("isSwordIdle", true);
       anime.SetBool("isSwordMove", false);
        anime.SetBool("isSwordWalk", false);
        anime.SetBool("isSwordRolling", false);
        //대기상태의 애니메이션 재생
        //anime.SetBool("isIdle", true);
        anime.SetInteger("isSwordCombo", 0);

    }
    public override void Execute()
    {
        SwordIdle();
    }
}
