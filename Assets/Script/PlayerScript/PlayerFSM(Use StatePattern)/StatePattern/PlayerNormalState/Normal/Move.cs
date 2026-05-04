using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : PlayerNormalState
{
    public enum move_num
    {
        IDLE = 0,
        WALK = 1,
        RUN = 2,
        Rolling = 3
    }
    public static Move move;
    private PlayerDirection player_dir;

    private float playerSpeed = 12.0f;
    private float Dashtime = 0.15f; // time for Dash 
    private float doubleClickTimeL;
    private float doubleClickTimeR;
    private float doubleClickTimeU;
    private float doubleClickTimeD;
    private float rollingDelay;


    public short moveNum; // Dash On is True, Dash off is false  // 대쉬중 다른 방향키를 눌렀을 때 대쉬가 지속되게 만드는 변수
    public short past_moveNum; // 구르기전단계 행동을 저장하는 변수

    private bool inputLock;

    public Move(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        if (move == null)
        {
            move = this;
        }

        keyboardCommand = new KeyboardCommand();
    }
    public override PlayerState InputHandler()
    {
        if (Input.GetMouseButtonDown(0) && !inputLock)
        {
            if (Attack.attack == null)
            {
                return new Attack(playerTransform, playerRigid, anime, MainCam);
            }
            return Attack.attack;
        }

        //공격전환
        if (Input.GetKeyDown(KeyCode.Alpha1) && !inputLock && PlayerData.playerData.equip_weapon != null)
        {
            return new PlayerSwordDraw(playerTransform, playerRigid, anime, MainCam);
        }

        //어떤 키도 누르고 있지 않다면
        if (!Input.anyKey && !inputLock)
        {
            moveNum = (short)move_num.IDLE;
            Debug.Log("Idle상태로 변환");
            return new Idle(playerTransform, playerRigid, anime, MainCam);//IDLE 상태로 변환
        }
        return null;
    }

    public override void Enter()
    {
        rollingDelay = 0.0f;
        inputLock = false;
        
        player_dir = new PlayerDirection(playerTransform, MainCam);
        command = new MoveCommand(playerTransform, playerRigid, anime); // 움직이게 하는 명령어
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        anime.SetBool("isMove", false);
    }
    public override void DoAction()
    {
        CheckMoveState();
        playerTransform = player_dir.PlayerDir();
        command_manager.ExeCommand();
    }

    bool isAnyKeyDown() // 어떤 버튼이라도 눌렸는가?
    {
        if(Input.anyKey)
        {
            return true;
        }
        return false;
    }

    


    private void CheckMoveState()
    {

        if (Time.time - rollingDelay > 0.7f && inputLock) // 구르기 시도후 행동잠금상태에서 일정시간 초가 지났다면
        {
            Debug.Log("구르기입력후 2초가 경과");
            PlayerData.playerData.isSuperMode = false;
            inputLock = false;
            rollingDelay = 0;
            moveNum = past_moveNum;
            return;
        }
        //Debug.Log(rollingDelay);
        if(!inputLock)
        {
            if (!Input.anyKey)
            {
                moveNum = (short)move_num.IDLE;
                return;
            }

            foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
            {
                if (Input.GetKeyDown(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
                {
                    switch (dic.Key)
                    {
                        //점프
                        case "Rolling":
                            past_moveNum = moveNum;
                            moveNum = (short)move_num.Rolling;
                            Debug.Log("스페이스바 입력");

                            if (rollingDelay == 0)
                            {
                                rollingDelay = Time.time;
                            }
                            PlayerData.playerData.isSuperMode = true;
                            inputLock = true;
                            return;

                        //방향키 이동
                        case "Left":
                            if (Time.time - doubleClickTimeL < Dashtime)// 더블클릭시 실행
                            {
                                moveNum = (short)move_num.RUN;
                            }
                            //Debug.Log(doubleClickTimeL);
                            doubleClickTimeL = Time.time;
                            return;

                        case "Right":
                            if (Time.time - doubleClickTimeR < Dashtime)// 더블클릭시 실행
                            {
                                moveNum = (short)move_num.RUN;
                            }

                            doubleClickTimeR = Time.time;
                            return;

                        case "Forward":
                            if (Time.time - doubleClickTimeU < Dashtime)// 더블클릭시 실행
                            {
                                Debug.Log("더블클릭했습니다");
                                moveNum = (short)move_num.RUN;
                            }
                            Debug.Log("앞으로갑니다");
                            doubleClickTimeU = Time.time;
                            return;

                        case "Back":

                            if (Time.time - doubleClickTimeD < Dashtime)// 더블클릭시 실행
                            {
                                moveNum = (short)move_num.RUN;
                            }

                            doubleClickTimeD = Time.time;
                            return;
                    }
                }

                if (moveNum == (short)move_num.RUN && Input.GetKey(dic.Value)) // 키를 누르면서 달리는 중이라면 계속 달리기 유지
                {
                    Debug.Log("달리기 유지");
                    return;
                }
            }

            moveNum = (short)move_num.WALK;
        }


    }

    
}

