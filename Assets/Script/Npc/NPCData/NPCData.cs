using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcTextData
{
    public string npcName;

    [TextArea]
    public string npcText;

    public List<TalkButtonType> buttonTypes; // 해당 텍스트가 어떤 버튼들을 들고있을 것인지
}

public enum NPCType
{
    Normal,
    WanderingTraderShop
}

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{

    public List<NpcTextData> npcTexts = new(); // ncp 대화내용

    public NPCType npcType; // 대화유형을 뭘로 할 것인가 정하기
}
