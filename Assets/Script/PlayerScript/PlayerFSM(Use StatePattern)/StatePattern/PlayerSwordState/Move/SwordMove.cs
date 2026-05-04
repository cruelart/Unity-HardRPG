using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : PlayerSwordState
{
    public static SwordMove swordMove;
    public enum swordMove_num
    {
        IDLE = 0,
        WALK = 1,
        RUN = 2,
        Rolling = 3
    }

    public int swordMoveNum;
    private int pastSwordMoveNum;

    private float playerSpeed = 12.0f;
    private float Dashtime = 0.15f; // time for Dash 
    private float doubleClickTimeL;
    private float doubleClickTimeR;
    private float doubleClickTimeU;
    private float doubleClickTimeD;
    private float rollingDelay;

    private bool inputLock;

    private PlayerDirection player_dir;


    public SwordMove(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        :base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();

        inputLock = false;
        swordMoveNum = -1;
        pastSwordMoveNum = 0;

        if(swordMove == null)
        {
            swordMove = this;
        }
    }

    public override PlayerState InputHandler()
    {
        if (Input.GetMouseButtonDown(0) && !inputLock)
        {
            if (SwordAttack.sword_attack == null)
            {
                return new SwordAttack(playerTransform, playerRigid, anime, MainCam);
            }
            return SwordAttack.sword_attack;
        }
        //어떤 키도 누르고 있지 않다면
        if (!Input.anyKey && !inputLock)
        {
            swordMoveNum = (short)swordMove_num.IDLE;
            //Debug.Log("Idle상태로 변환");
            return new SwordIdle(playerTransform, playerRigid, anime, MainCam);//IDLE 상태로 변환
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !inputLock)
        {
            //Debug.Log("esc버튼 클릭");
            return new PlayerSwordDrawOff(playerTransform, playerRigid, anime, MainCam);
        }
        return null;
    }

    public override void Enter()
    {
        rollingDelay = 0.0f;
        //Debug.Log("현재 검을 든 상태에서 걷는 중입니다");
        player_dir = new PlayerDirection(playerTransform, MainCam);
        command = new SwordMoveCommand(playerTransform, playerRigid, anime); // 공격 명령어
        command_manager.SetCommand(command);
    }

    public override void DoAction()
    {
        CheckMoveState();
        playerTransform = player_dir.PlayerDir();
        command_manager.ExeCommand();
    }

    public override void Exit()
    {
        //base.Exit();
        rollingDelay = 0.0f;
    }


    private void CheckMoveState()
    {

        if (Time.time - rollingDelay > 0.7f && inputLock) // 구르기 시도후 행동잠금상태에서 2초가 지났다면
        {
            Debug.Log("구르기입력후 2초가 경과");
            inputLock = false;
            rollingDelay = 0;
            swordMoveNum = pastSwordMoveNum;
            return;
        }
        //Debug.Log(rollingDelay);
        if (!inputLock)
        {
            if (!Input.anyKey)
            {
                swordMoveNum = (int)swordMove_num.IDLE;
                return;
            }

            foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
            {
                if (Input.GetKey(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
                {
                    if (dic.Key == "Rolling")
                    {
                        pastSwordMoveNum = swordMoveNum;
                        swordMoveNum = (int)swordMove_num.Rolling;
                        Debug.Log("스페이스바 입력");

                        if (rollingDelay == 0)
                        {
                            rollingDelay = Time.time;
                        }

                        inputLock = true;
                        return;
                    }

                    swordMoveNum = (int)swordMove_num.WALK;
                    return;
                }
            }

            
        }


    }
}
