using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCommand
{
    public Dictionary<string, KeyCode> command_keyboard = new Dictionary<string, KeyCode>
    {
        //행동키
        {"Rolling", KeyCode.Space },

        //방향키
        {"Forward", KeyCode.W },
        {"Back", KeyCode.S },
        {"Left", KeyCode.A },
        {"Right", KeyCode.D },

    };

    public Dictionary<string, KeyCode> change_weapon = new Dictionary<string, KeyCode>
    {
        //무기 교체 키
        {"SwordReadyMotion", KeyCode.Alpha1}, // 1번키를 눌렀을 때 검 준비 모션
        {"ArrowReadyMotion", KeyCode.Alpha2 } // 2번키를 누르면 활 준비 모션
    };

    // 마우스 클릭 관련 명령어 설정 -> 공격 키
    public Dictionary<string, int> command_mouse = new Dictionary<string, int>
    {
        {"MouseLeft", 0 },
        {"MouseRight", 1 }
    };
}
