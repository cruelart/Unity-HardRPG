using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDrawOff : PlayerSwordState
{
    private float DrawOffTime;
    private int exeNum; // ½ÇÇàÈ½¼ö

    private StopWatch stopWatch;
    public PlayerSwordDrawOff(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
        stopWatch = new StopWatch();
        DrawOffTime = 0.0f;
        exeNum = 1;
    }

    public override PlayerState InputHandler()
    {
        if (stopWatch.stop_watch(DrawOffTime, 1.0f))
        {
            return new Idle(playerTransform, playerRigid, anime, MainCam);
        }
        return null;
    }

    public override void Enter()
    {
        DrawOffTime = Time.time;
        command = new SwordDrawOffCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void DoAction()
    {
        if (exeNum == 1)
        {
            command_manager.ExeCommand();
            exeNum--;
        }
    }

    public override void Exit()
    {
        exeNum = 1;
    }
}
