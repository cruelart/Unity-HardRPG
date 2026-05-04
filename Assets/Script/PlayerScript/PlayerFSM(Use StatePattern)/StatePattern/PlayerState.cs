using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected KeyboardCommand keyboardCommand;
    protected Command command;
    protected CommandManager command_manager = new CommandManager();

    //기본 제공 맴버변수
    public Transform playerTransform;
    public Rigidbody playerRigid;
    public Animator anime;
    public GameObject MainCam;

    public PlayerState(Transform _playerTransform, Rigidbody _playerRigid, Animator _anime, GameObject _MainCam)
    {
        playerTransform = _playerTransform;
        playerRigid = _playerRigid;
        anime = _anime;
        MainCam = _MainCam;
    }

    //PlayerState구성이라면 무조건 구현해야되는 진입(Enter), 해제(Exit), 실행(DoAction)
    public abstract PlayerState InputHandler();
    public abstract void DoAction();
    public virtual void Enter() { }
    public virtual void Exit() { }


}
