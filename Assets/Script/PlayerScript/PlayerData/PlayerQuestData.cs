using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuestData
{
    private Dictionary<int, QuestProgressData> playerQuestProgressTable = new(); // 모든 퀘스트id에 대한 데이터

    private HashSet<int> available_quests = new(); // 플레이어가 시작가능한 퀘스트들
    private HashSet<int> inProgress_quests = new(); // 플레이어가 진행중인 퀘스트들
    private HashSet<int> completed_quests = new(); // 플레이어가 완료한 퀘스트들

    //읽는 것만 허용
    public IReadOnlyDictionary<int, QuestProgressData> PlayerQuestProgressTable => playerQuestProgressTable;
    public IReadOnlyCollection<int> AvailableQuests => available_quests;
    public IReadOnlyCollection<int> InProgressQuests => inProgress_quests;  
    public IReadOnlyCollection<int> CompleteQuests => completed_quests;

    public void Init(IEnumerable<int> _allQuestIDs)
    {
        //전체적으로 비우고 (어쩌피 초기화할거니까)
        playerQuestProgressTable.Clear();
        available_quests.Clear();
        inProgress_quests.Clear();
        completed_quests.Clear();

        foreach (int questID in _allQuestIDs)
        {
            QuestProgressData progress = new QuestProgressData
                {
                    questID = questID,
                    questState = QuestState.Available,
                    currentCount = 0
                };

            playerQuestProgressTable.Add(questID,progress);

            available_quests.Add(questID);
        }
    }

    //퀘스트 로드
    public void LoadQuestProgressData(List<QuestProgressData> _questProgressDataList)
    {
        playerQuestProgressTable = _questProgressDataList.ToDictionary(x => x.questID);

        available_quests.Clear();
        inProgress_quests.Clear();
        completed_quests.Clear();

        foreach(var questProgressData in _questProgressDataList)
        {
            switch(questProgressData.questState)
            {
                case QuestState.Available:
                    available_quests.Add(questProgressData.questID);
                    break;
                case QuestState.InProgress:
                    inProgress_quests.Add(questProgressData.questID);
                    break;
                case QuestState.Completed:
                    completed_quests.Add(questProgressData.questID);
                    break;
            }
        }
    }

    //퀘스트 수락
    public void AcceptQuest(int _questID)
    {
        if(!playerQuestProgressTable.TryGetValue(_questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"퀘스트 ID {_questID}에 대한 진행 데이터가 없습니다.");
            return;
        }

        if(questProgressData.questState != QuestState.Available)
        {
            Debug.LogError($"해당 퀘스트는 시작가능한 퀘스트가 아니기 때문에 퀘스트를 수락할 수 없습니다,");
            return;
        }

        available_quests.Remove(_questID); // 시작가능한 퀘스트에서 제거 처리

        questProgressData.questState = QuestState.InProgress; // 퀘스트 상태를 진행중으로 변경
        questProgressData.currentCount = 0; // 진행상태 0으로  초기화

        inProgress_quests.Add(_questID); // 진행중인 퀘스트에 추가 처리
    }

    //퀘스트 포기
    public void GiveUpQuest(int _questID)
    {
        if(!playerQuestProgressTable.TryGetValue(_questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"퀘스트 ID {_questID}에 대한 진행 데이터가 없습니다.");
            return;
        }
        if(questProgressData.questState != QuestState.InProgress)
        {
            Debug.LogError($"해당 퀘스트는 진행중이 아니기 때문에 포기할 수 없습니다.");
            return;
        }
        inProgress_quests.Remove(_questID); // 진행중인 퀘스트에서 제거 처리

        questProgressData.questState = QuestState.Available; // 퀘스트 상태를 시작가능으로 변경
        questProgressData.currentCount = 0; // 진행상태 0으로 초기화

        available_quests.Add(_questID); // 시작가능한 퀘스트에 추가 처리 -> 와.. ㅋㅋ
    }

    //퀘스트 완료
    public void CompleteQuest(int _questID)
    {
        if(!playerQuestProgressTable.TryGetValue(_questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"퀘스트 ID {_questID}에 대한 진행 데이터가 없습니다.");
            return;
        }
        if(questProgressData.questState != QuestState.InProgress)
        {
            Debug.LogError($"해당 퀘스트는 진행중이 아니기 때문에 완료할 수 없습니다.");
            return;
        }
        inProgress_quests.Remove(_questID); // 진행중인 퀘스트에서 제거 처리

        questProgressData.questState = QuestState.Completed; // 퀘스트 상태를 완료로 변경
        questProgressData.currentCount = 0; // 진행상태 0으로 초기화

        completed_quests.Add(_questID); // 완료한 퀘스트에 추가 처리
    }
}
