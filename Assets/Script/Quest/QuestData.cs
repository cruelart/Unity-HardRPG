using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public int questID; // 퀘스트의 고유 아이디

    [Header("퀘스트 정보")]
    public string questName; // 퀘스트 이름
    [TextArea]
    public string questDescription; // 퀘스트 설명

    public NPCData questNPC; // 퀘스트를 제공한 NPC
}
