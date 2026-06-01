using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : PlayerNormalState
{
    public static Attack attack; // 싱글톤 패턴 사용

    StopWatch stopwatch;

    private enum normalAttack_num
    {
        comboAttack1 = 1, //-------------
        comboAttack2 = 2, //일반공격 콤보1,2,3
        comboAttack3 = 3, //-------------
        flyingKick = 4
    }

    public  int normalAttackNum;
    private bool  inputLock;
    private float animeTime;

    private float normal_attackTime = 0.0f;
    public Attack(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
        stopwatch = new StopWatch();

        if (attack == null) // 싱글톤
        {
            attack = this;
        }

}
public override void DoAction()
    {
        CheckAttack();
        command_manager.ExeCommand();
    }
    public override void Enter()
    {
        normalAttackNum = 0;
        inputLock = false;
        normal_attackTime = Time.time;

        Debug.Log("Attack 상태로 진입");

        command = new AttackCommand(anime); // 공격 명령어
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        anime.SetBool("isAttack", false);
        normalAttackNum = 0;
    }

    public override PlayerState InputHandler()
    {
        if (!inputLock)
        {
            //노멀상태의 Idle 전환
            if(stopwatch.stop_watch(normal_attackTime, animeTime + 0.15f))
            {
                return new Idle(playerTransform, playerRigid, anime, MainCam);
            }

            //노멀상태의 달리기 전환
            if (stopwatch.stop_watch(normal_attackTime, animeTime + 0.15f))
            {
                foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
                {
                    if (Input.GetKey(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
                    {
                        if (Move.move == null)
                        {
                            return new Move(playerTransform, playerRigid, anime, MainCam);
                        }
                        return Move.move;

                    }
                }

                ////검ON상태의 State로 전환
                //if(Input.GetKeyDown(KeyCode.Alpha1)) // 키보드 입력 숫자 1을 입력했을 경우
                //{
                //    return new SwordIdle(playerTransform, playerRigid, anime, MainCam);
                //}
            }


        }
        return null;
    }
    private void AnimationExeTime(float _time)
    {
        if (stopwatch.stop_watch(normal_attackTime, _time))
        {
            inputLock = false;
            if(normalAttackNum >= 3)
            {
                normalAttackNum = 0;
            }
        }
        if (stopwatch.stop_watch(normal_attackTime, _time + 0.15f))
        {
            normalAttackNum = 0;
        }
    }
    private void CheckAttack()
    {
        //Debug.Log(normalAttackNum);

        switch(normalAttackNum)
        {
            case 1:
                animeTime = 0.4f;
                AnimationExeTime(animeTime);
                break;
            case 2:
                animeTime = 0.4f;
                AnimationExeTime(animeTime);
                break;
            case 3:
                animeTime = 0.85f;
                AnimationExeTime(animeTime);
                break;
            case 4:
                animeTime = 0.9f;
                AnimationExeTime(animeTime);
                break;

        }

        if(Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            if (!inputLock)
            {
                //달리기 상태에서 공격을 하게되면 플라잉킥
                if (Move.move!= null && Move.move.moveNum == 2)
                {
                    Move.move.moveNum = 1;
                    inputLock = true;
                    normalAttackNum = 4;
                    return;
                }
        
                inputLock = true;
                if (normalAttackNum < 3)
                {
                    normalAttackNum++;
                }
        
                normal_attackTime = Time.time;
            }
        }
        

        
    }

}
