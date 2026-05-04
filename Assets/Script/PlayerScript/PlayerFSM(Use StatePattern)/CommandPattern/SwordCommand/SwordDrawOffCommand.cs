using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDrawOffCommand : Command
{
    private Animator anime;
    public SwordDrawOffCommand(Animator _anime)
    {
        anime = _anime;
    }
    public void DrawOffSword()
    {
        anime.SetBool("isSwordIdle", false);
        //anime.SetBool("isWalk", false);
        //anime.SetBool("isRun", false);
    }
    public override void Execute()
    {
        DrawOffSword();
    }
}
