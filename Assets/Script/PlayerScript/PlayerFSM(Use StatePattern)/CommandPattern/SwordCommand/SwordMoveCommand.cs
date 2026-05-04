using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMoveCommand : Command
{
    Transform playerTransform; // 플레이어의 좌표
    Rigidbody playerRigid;
    Animator playerAnime;

    float playerSpeed = 12.0f;

    //public bool isDash;

    public SwordMoveCommand(Transform _playerTransform, Rigidbody _playerRigid, Animator _playerAnime) // 각 변수 초기화
    {
        playerTransform = _playerTransform;
        playerRigid = _playerRigid;
        playerAnime = _playerAnime;

        playerAnime.SetBool("isSwordChangeState", false);

        //keyboardCommand = new KeyboardCommand();
    }
    private void Walk()
    {
        playerRigid.MovePosition(playerTransform.position + playerTransform.forward * playerSpeed * Time.deltaTime);

        playerAnime.SetBool("isSwordWalk",true);
        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isRun", false);
        playerAnime.SetBool("isSwordRolling", false);
        playerAnime.SetBool("isAttack", false);
        //playerAnime.SetBool("isWalk", true);
        playerAnime.SetInteger("isCombo", 0);
    }

    private void Run()
    {
        playerRigid.MovePosition(playerTransform.position + playerTransform.forward * 2 * playerSpeed * Time.deltaTime);

        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isWalk", false);
        playerAnime.SetBool("isSwordRolling", false);
        playerAnime.SetBool("isAttack", false);
        playerAnime.SetBool("isRun", true);
        playerAnime.SetInteger("isCombo", 0);
    }

    private void Rolling()
    {
        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isRun", false);
        playerAnime.SetBool("isSwordWalk", false);
        playerAnime.SetBool("isSwordRolling", true);
        //playerAnime.SetBool("isMove", false);
        playerAnime.SetBool("isJump", true);
        playerAnime.SetBool("isSwordAttack", false);
        playerAnime.SetInteger("isCombo", 0);

    }

    //Dash On is True, Dash off is false  -> 대쉬중 다른 방향키를 눌렀을 때 대쉬가 지속되게 만드는 변수


    public override void Execute() // 최종 실행
    {
        
        playerAnime.SetBool("isSwordMove", true);
        //Debug.Log("칼들고 달리는 명령어 실행중");
        //Debug.Log(SwordMove.swordMove.swordMoveNum);

        if (SwordMove.swordMove.swordMoveNum == (int)SwordMove.swordMove_num.RUN)
        {
            Run();
        }
        if (SwordMove.swordMove.swordMoveNum == (int)SwordMove.swordMove_num.WALK)
        {
            Walk();
            PlayerData.playerData.player_hungry -= 0.012f;
            Debug.Log("걷는다");
        }
        if (SwordMove.swordMove.swordMoveNum == (int)SwordMove.swordMove_num.Rolling)
        {
            Rolling();
            Debug.Log("구른다");
        }
    }
}
