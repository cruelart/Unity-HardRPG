using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    //public static MoveCommand moveCommand;

    Transform playerTransform; // 플레이어의 좌표
    Rigidbody playerRigid;
    Animator  playerAnime;

    //public bool isDash;

    public MoveCommand(Transform _playerTransform, Rigidbody _playerRigid, Animator _playerAnime) // 각 변수 초기화
    {
        //if (moveCommand == null)
        //{
        //    moveCommand = this;
        //}

        playerTransform = _playerTransform;
        playerRigid = _playerRigid;
        playerAnime = _playerAnime;

        playerAnime.SetBool("isChangeState", false);

        //keyboardCommand = new KeyboardCommand();
    }
    private void Walk()
    {
        playerRigid.MovePosition(playerTransform.position + playerTransform.forward * PlayerData.playerData.Speed_value * Time.deltaTime);

        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isRun", false);
        playerAnime.SetBool("isJump", false);
        playerAnime.SetBool("isAttack", false);
        playerAnime.SetBool("isWalk", true);
        playerAnime.SetInteger("isCombo", 0);
    }

    private void Run()
    {
        playerRigid.MovePosition(playerTransform.position + playerTransform.forward * 2 * PlayerData.playerData.Speed_value * Time.deltaTime);

        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isWalk", false);
        playerAnime.SetBool("isJump", false);
        playerAnime.SetBool("isAttack", false);
        playerAnime.SetBool("isRun", true);
        playerAnime.SetInteger("isCombo", 0);
    }

    private void Rolling()
    {
        playerAnime.SetBool("isIdle", false);
        playerAnime.SetBool("isRun", false);
        playerAnime.SetBool("isWalk", false);
        //playerAnime.SetBool("isMove", false);
        playerAnime.SetBool("isJump", true);
        playerAnime.SetBool("isAttack", false);
        playerAnime.SetInteger("isCombo", 0);

    }

    //Dash On is True, Dash off is false  -> 대쉬중 다른 방향키를 눌렀을 때 대쉬가 지속되게 만드는 변수
    

    public override void Execute() // 최종 실행
    {

        playerAnime.SetBool("isMove", true);
        //Debug.Log(checkDash);

        if (Move.move.moveNum == (short)Move.move_num.RUN)
       {
           Run();
            PlayerData.playerData.player_hungry -= 0.008f;
        }
       else if (Move.move.moveNum == (short)Move.move_num.WALK)
       {
           Walk();
            PlayerData.playerData.player_hungry -= 0.003f;
            Debug.Log("걷는다");
       }
       if (Move.move.moveNum == (short)Move.move_num.Rolling)
       {
           Rolling();
            PlayerData.playerData.player_hungry -= 0.005f;
            Debug.Log("구른다");
       }
    }
}
