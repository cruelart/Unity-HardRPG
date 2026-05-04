using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReact : PlayerState
{

    StopWatch stopWatch;
    float animeTime;
    public PlayerReact(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
       : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        stopWatch = new StopWatch();
        animeTime = 0.0f;
    }
    public override void DoAction()
    {
        Debug.Log("피격상태로 진입");
        //Debug.Log("Idle상태로 변경하겠다");
        command_manager.ExeCommand();
    }

    public override void Enter()
    {
        PlayerData.playerData.isSuperMode = true;
        animeTime = Time.time;
        command = new ReactCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        //PlayerData.playerData.isSuperMode = false;
        anime.SetTrigger("Exit");
    }

    public override PlayerState InputHandler()
    {
        if (stopWatch.stop_watch(animeTime, 0.8f))
        {
            if (PlayerData.playerData.OnSword == true && PlayerData.playerData.equip_weapon != null)
            {
                return new SwordIdle(playerTransform, playerRigid, anime, MainCam);
            }
            return new Idle(playerTransform, playerRigid, anime, MainCam);
        }
        return null;
    }
}
