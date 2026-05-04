using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager
{
    Command command;

    public void SetCommand(Command _command) // 받아올 커맨드 설정
    {
        command = _command;
    }

    public void ExeCommand() // 최종 실행
    {
        command.Execute();
    }
}
