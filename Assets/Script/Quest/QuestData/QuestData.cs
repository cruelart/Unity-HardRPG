using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
public class QuestData : ScriptableObject
{
    public int questID; // 퀘스트의 고유 아이디

    [Header("퀘스트 정보")]
    public string questName; // 퀘스트 이름
    [TextArea]
    public string questDescription; // 퀘스트 설명

    public int goalCount;

    [Header("퀘스트 제공 NPC")]
    public string StartNpcName;
    public Sprite StartNpcImage;
    public Vector3 StartNpcTransform;

    [Header("퀘스트 마감 NPC")]
    public string EndNpcName;
    public Sprite EndNpcImage;
    public Vector3 EndNpcTransform;

    [Header("클리어 보상")]
    public BaseitemDB ClearItem;
    public long ClearGold;
    public long ClearExp;
}

//퀘스트 진행 상태
public enum QuestState
{
    Available,
    InProgress,
    Completed
}

public class QuestProgressData
{
    public int questID; // 어떤 퀘스트?

    public QuestState questState; // 진행 상태

    public int currentCount; // 진행상황
}
