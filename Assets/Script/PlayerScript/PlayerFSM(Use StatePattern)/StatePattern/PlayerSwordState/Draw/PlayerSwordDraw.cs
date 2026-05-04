using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDraw : PlayerSwordState
{
    private float DrawTime;
    private int exeNum; // ½ÇÇàÈ½¼ö

    private StopWatch stopWatch;
    public PlayerSwordDraw(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
        stopWatch = new StopWatch();
        DrawTime = 0.0f;
        exeNum = 1;
    }

    public override PlayerState InputHandler()
    {
        if(stopWatch.stop_watch(DrawTime, 0.6f))
        {
            return new SwordIdle(playerTransform, playerRigid, anime, MainCam);
        }
        return null;
    }

    public override void Enter()
    {
        DrawTime = Time.time;
        command = new SwordDrawCommand(anime);
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
