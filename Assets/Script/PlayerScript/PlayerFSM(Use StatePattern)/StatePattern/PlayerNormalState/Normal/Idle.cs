using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerNormalState
{
    private bool isDash;
    public Idle(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam) 
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
    }

    public override void DoAction()
    {
        //Debug.Log("Idle상태로 변경하겠다");
        command_manager.ExeCommand();
    }

    public override void Enter()
    {
        command = new IdleCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        
    }

    public override PlayerState InputHandler()
    {
        if (PlayerData.playerData.player_hp <= 0)
        {
            return new PlayerDeath(playerTransform, playerRigid, anime, MainCam);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(Attack.attack == null)
            {
                return new Attack(playerTransform, playerRigid, anime, MainCam);
            }
            return Attack.attack;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerData.playerData.equip_weapon != null)
        {
            return new PlayerSwordDraw(playerTransform, playerRigid, anime, MainCam);
        }

        foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
        {
            if (Input.GetKey(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
            {
                if (Move.move == null)
                {
                    return new Move(playerTransform, playerRigid, anime, MainCam);
                }
                Move.move.playerTransform = playerTransform;
                return Move.move;
                
            }
        }
        return null;
    }
}
