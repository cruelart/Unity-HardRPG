using System.Xml.Linq;
using UnityEngine;

public class WanderingTrader : NPC
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        NpcInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DecideTextIndex();
            UIManager.Instance.ShowNpcTalkUI();
            UIManager.Instance.NpcTalkUI.Init(npcData, textIndex, questIndex);
        }
    }

    protected override void DecideTextIndex()
    {
        questIndex = 0;
        QuestState questState0 = QuestManager.Instance.playerQuestData.PlayerQuestProgressTable[npcData.npcQuestIDs[0]].questState;

        if(questState0 == QuestState.Available)
        {
            textIndex = 0; // 아직 퀘스트를 받기 전이니까 0으로
        }
        else if(questState0 == QuestState.InProgress)
        {
            textIndex = 1; //받은 상태니까 1로
        }
        else if(questState0 == QuestState.Completed)
        {
            textIndex = 2; // 완료했으니까 2로
        }
    }
}
