using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDrawCommand : Command
{
    private Animator anime;

    public SwordDrawCommand(Animator _anime)
    {
        anime = _anime;
    }
    public void DrawSword()
    {   

        anime.SetBool("isNormalState", false);
        anime.SetBool("isSwordState", true);
        anime.SetBool("isWalk", false);
        anime.SetBool("isRun", false);
    }
    public override void Execute()
    {
        DrawSword();
    }
}
