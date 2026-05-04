using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*클래스템플릿generic 사용도 고려
public class SwordIdle : PlayerSwordState
{
    private bool isDash;

    private float sheatingTime;

    private StopWatch stopWatch;
    public SwordIdle(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
        stopWatch = new StopWatch();
        sheatingTime = 0.0f;
    }

    public override void DoAction()
    {
        //Debug.Log("SwordIdle상태로 변경하겠다");
        command_manager.ExeCommand();
    }

    public override void Enter()
    {
        command = new SwordIdleCommand(anime);
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {

    }

    public override PlayerState InputHandler()
    {
        //검을 집어넣음
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return new PlayerSwordDrawOff(playerTransform, playerRigid, anime, MainCam);
        }
        foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
        {
            if (Input.GetKey(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
            {
                if (SwordMove.swordMove == null)
                {
                    return new SwordMove(playerTransform, playerRigid, anime, MainCam);
                }
                SwordMove.swordMove.playerTransform = playerTransform;
                return SwordMove.swordMove;

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (SwordAttack.sword_attack == null)
            {
                return new SwordAttack(playerTransform, playerRigid, anime, MainCam);
            }
            return SwordAttack.sword_attack;
        }
        return null;
    }
}
