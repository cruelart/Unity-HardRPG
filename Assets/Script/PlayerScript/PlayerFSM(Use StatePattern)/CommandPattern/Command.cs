using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected KeyboardCommand keyboardCommand;

    //protected Vector3 playerDir;

    public Command()
    {

    }
    public abstract void Execute(); // 실행시킬 함수를 추상함수로 설정
}
