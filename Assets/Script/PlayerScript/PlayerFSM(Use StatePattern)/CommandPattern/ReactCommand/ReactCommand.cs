using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactCommand : Command
{
    private Animator anime;
    private bool isExe; // 1번이라도 실행했는가?
    // Start is called before the first frame update
    public ReactCommand(Animator _anime)
    {
        anime = _anime;
        isExe = false;
    }

    public override void Execute()
    {
        if (!isExe)
        {
            anime.SetTrigger("isReact");
            isExe = true; // 1번 실행했으니 true 변환
        }
    }
}
