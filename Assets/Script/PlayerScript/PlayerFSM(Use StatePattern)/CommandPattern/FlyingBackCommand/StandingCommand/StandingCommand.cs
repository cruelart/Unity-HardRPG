using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCommand : Command
{
    private Animator anime;
    private bool isExe; // 1번이라도 실행했는가?
    // Start is called before the first frame update
    public StandingCommand(Animator _anime)
    {
        anime = _anime;
        isExe = false;
    }

    public override void Execute()
    {

    }
}
