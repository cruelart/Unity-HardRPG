using System;
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

    //데이터 관련
    public QuestDataBase questDB { get; private set; } = new QuestDataBase(); // 전체 퀘스트 목록
    public PlayerQuestData playerQuestData { get; private set; } = new PlayerQuestData(); // 플레이어 퀘스트 진행 현황

    //private Dictionary<int, QuestProgressData> questProgressDataTable = new(); // (퀘스트 아이디(키), 모든 퀘스트 진행 현황) -> 플레이어 전용

    //private Dictionary<int, QuestData> questDataTable = new(); // 퀘스트 아이디(키), 해당 퀘스트 데이터 -> 모든 플레이어 공용 Complit성능괜찮네 ㄷ

    //이벤트
    public Action<QuestState, int> OnQuestChangeNotify; // 퀘스트 상태가 바뀌었을 때 알림 (퀘스트 상태, 퀘스트 아이디)

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        questDB.LoadData(); // 전체 퀘스트 목록 불러오기
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

    //LoadManager에서 퀘스트를 받아옴
    public void InitLoadData(List<QuestProgressData> _questProgressDataList)
    {
        if(_questProgressDataList.Count == 0) // 로드했는데 데이터가 비어있다면 -> 파일이 없다는거겠쬬?
        {
            playerQuestData.Init(questDB.QuestDataTable.Keys);
            return;
        }
        playerQuestData.LoadQuestProgressData(_questProgressDataList);
        //-> 여기까지 했으면 퀘스트의 전체적인 것들은 모두 받아온 셈
    }

    public QuestProgressDataList GetQuestProgressDataList()
    {
        QuestProgressDataList data = new QuestProgressDataList();

        data.questProgressDatas = new List<QuestProgressData>(playerQuestData.PlayerQuestProgressTable.Values);

        return data;
    }
}
