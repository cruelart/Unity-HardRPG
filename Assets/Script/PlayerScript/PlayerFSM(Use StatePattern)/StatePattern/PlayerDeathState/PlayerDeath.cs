using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    public PlayerDeath(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {

    }
    public override void DoAction()
    {
        //Debug.Log("Idle상태로 변경하겠다");
        command_manager.ExeCommand();
    }

    public override void Enter()
    {
        command = new PlayerDeathCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {

    }

    public override PlayerState InputHandler()
    {
        return null;
    }
}