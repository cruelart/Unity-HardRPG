using UnityEngine;

public class QuestUI : UIBase
{
    //퀘스트 설명 패널 적는 곳

    //시작가능 퀘스트
    [SerializeField]
    private QuestAvailable questAvailable;

    //진행중 퀘스트

    //완료 퀘스트
    private void Awake()
    {
        Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        foreach(var questID in QuestManager.Instance.playerQuestData.AvailableQuests)
        {
            questAvailable.AddAvailableQuest(QuestManager.Instance.questDB.QuestDataTable[questID], QuestManager.Instance.playerQuestData.PlayerQuestProgressTable[questID]);
        }
    }
}
