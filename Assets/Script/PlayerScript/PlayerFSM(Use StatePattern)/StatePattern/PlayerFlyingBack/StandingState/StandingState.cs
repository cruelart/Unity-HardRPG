using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : PlayerState
{

    StopWatch stopWatch;
    float animeTime;
    public StandingState(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
       : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        stopWatch = new StopWatch();
        animeTime = 0.0f;
    }
    public override void DoAction()
    {
        Debug.Log("일어나기상태로 진입");
        //Debug.Log("Idle상태로 변경하겠다");
        command_manager.ExeCommand();
    }

    public override void Enter()
    {
        animeTime = Time.time;
        command = new StandingCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        PlayerData.playerData.isSuperMode = false;
        anime.SetTrigger("Exit");
    }

    public override PlayerState InputHandler()
    {
        if (stopWatch.stop_watch(animeTime, 1.4f))
        {
            return new Idle(playerTransform, playerRigid, anime, MainCam);
        }
        return null;
    }

}
