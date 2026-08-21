using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuestProgressDataList
{
    public List<QuestProgressData> questProgressDatas;
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField]
    private List<QuestData> questDataList; // 모든 퀘스트의 목록

    private Dictionary<int, QuestProgressData> questProgressDataTable = new(); // 퀘스트 아이디(키), 해당 퀘스트 진행 현황 -> 플레이어 전용

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        InitQuest();
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitQuest()
    {
        foreach(var questData in questDataList)
        {
            QuestProgressData questProgressData = new QuestProgressData();

            questProgressData.questID = questData.questID;
            questProgressData.questState = QuestState.Available; // 처음에는 전부 시작가능
            questProgressData.currentCount = 0; // 무조건 0으로 시작

            questProgressDataTable.Add(questData.questID, questProgressData);
        }
    }

    public void InitLoadData(List<QuestProgressData> _questProgressDataList)
    {
        questProgressDataTable = _questProgressDataList.ToDictionary(x => x.questID);
    }

    public QuestProgressDataList GetQuestProgressDataList()
    {
        QuestProgressDataList data = new QuestProgressDataList();

        data.questProgressDatas =
            new List<QuestProgressData>(questProgressDataTable.Values);

        return data;
    }
}
