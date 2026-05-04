using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : PlayerSwordState
{
    public static SwordAttack sword_attack; // 싱글톤 패턴 사용

    StopWatch stopwatch;

    private enum swordAttack_num
    {
        comboAttack1 = 1, //-------------
        comboAttack2 = 2, //일반공격 콤보1,2,3
        comboAttack3 = 3, //-------------
    }

    public int swordAttackNum;
    private bool inputLock;
    private float animeTime;

    public bool isSwordAttack;

    private float sword_attackTime = 0.0f;
    public SwordAttack(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
        : base(_playerTransform, _playerRigid, _anime, _MainCam)
    {
        keyboardCommand = new KeyboardCommand();
        stopwatch = new StopWatch();

        if (sword_attack == null) // 싱글톤
        {
            sword_attack = this;
        }

    }
    public override void DoAction()
    {
        CheckAttack();
        command_manager.ExeCommand();
    }
    public override void Enter()
    {
        swordAttackNum = 0;
        inputLock = false;
        sword_attackTime = Time.time;
        isSwordAttack = true;

        Debug.Log("SwordAttack 상태로 진입");

        command = new SwordAttackCommand(anime); // 공격 명령어
        command_manager.SetCommand(command);
    }

    public override void Exit()
    {
        anime.SetBool("isSwordAttack", false);
        isSwordAttack = false;
    }

    public override PlayerState InputHandler()
    {
        if (!inputLock)
        {
            //노멀상태의 Idle 전환
            if (stopwatch.stop_watch(sword_attackTime, animeTime + 0.15f))
            {
                return new SwordIdle(playerTransform, playerRigid, anime, MainCam);
            }

            //노멀상태의 달리기 전환
            if (stopwatch.stop_watch(sword_attackTime, animeTime + 0.15f))
            {
                foreach (var dic in keyboardCommand.command_keyboard) // dictionary에 들어있는 것들을 하나하나 꺼냄
                {
                    if (Input.GetKey(dic.Value)) // 키보드 입력을 했고 그 키보드 입력이 dictionary안에 있는 코드라면
                    {
                        if (SwordMove.swordMove == null)
                        {
                            return new SwordMove(playerTransform, playerRigid, anime, MainCam);
                        }
                        return SwordMove.swordMove;

                    }
                }

            }


        }
        return null;
    }
    private void AnimationExeTime(float _time)
    {
        if (stopwatch.stop_watch(sword_attackTime, _time))
        {
            inputLock = false;
            if (swordAttackNum >= 3)
            {
                swordAttackNum = 0;
                //Debug.Log("콤보의 끝이 보입니다.");
            }
        }
        if (stopwatch.stop_watch(sword_attackTime, _time + 0.15f))
        {
            Debug.Log("실행");
            swordAttackNum = 0;
        }
    }
    private void CheckAttack()
    {
        //Debug.Log(swordAttackNum);

        switch (swordAttackNum)
        {
            case 1:
                animeTime = 0.6f;
                AnimationExeTime(animeTime);
                break;
            case 2:
                animeTime = 0.55f;
                AnimationExeTime(animeTime);
                break;
            case 3:
                animeTime = 1.2f;
                AnimationExeTime(animeTime);
                break;

        }

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            if (!inputLock)
            {
                inputLock = true;
                if (swordAttackNum < 3)
                {
                    swordAttackNum++;
                }

                sword_attackTime = Time.time;
            }
        }



    }

}
