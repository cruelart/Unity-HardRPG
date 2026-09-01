using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuestData
{
    private Dictionary<int, QuestProgressData> playerQuestProgressTable = new(); // ¸ğµç Äù½ºÆ®¿¡ ´ëÇÑ ÇÃ·¹ÀÌ¾îÀÇ ÁøÇà»óÈ² µ¥ÀÌÅÍ

    private HashSet<int> available_quests = new(); // ÇÃ·¹ÀÌ¾î°¡ ½ÃÀÛ°¡´ÉÇÑ Äù½ºÆ®µé
    private HashSet<int> inProgress_quests = new(); // ÇÃ·¹ÀÌ¾î°¡ ÁøÇàÁßÀÎ Äù½ºÆ®µé
    private HashSet<int> completed_quests = new(); // ÇÃ·¹ÀÌ¾î°¡ ¿Ï·áÇÑ Äù½ºÆ®µé

    //ÀĞ´Â °Í¸¸ Çã¿ë
    public IReadOnlyDictionary<int, QuestProgressData> PlayerQuestProgressTable => playerQuestProgressTable;
    public IReadOnlyCollection<int> AvailableQuests => available_quests;
    public IReadOnlyCollection<int> InProgressQuests => inProgress_quests;  
    public IReadOnlyCollection<int> CompleteQuests => completed_quests;

    //Äù½ºÆ® ¿ä±¸»çÇ× ºü¸¥ Á¢±ÙÀ» À§ÇÑ º¯¼ö¼±¾ğ
    private Dictionary<int, List<QuestRequirementRef>> monsterKillQuestMap = new(); // (¸ó½ºÅÍid, ±× ¸ó½ºÅÍ¸¦ ÀâÀ¸¶ó´Â Äù½ºÆ® id + ´ëÀÀÇÏ´Â ÀÎµ¦½º°ª  ¸ğÀ½Áı)
    private Dictionary<int, List<QuestRequirementRef>> itemCollectQuestMap = new(); // (¾ÆÀÌÅÛid, ÇØ´ç ¾ÆÀÌÅÛÀ» °¡Á®¿À¶ó´Â Äù½ºÆ® id ¸ğÀ½Áı)

    //ÀúÀåµÈ µ¥ÀÌÅÍ°¡ ¾ø´Ù¸é ÀÌ ÇÔ¼ö¸¦ È£Ãâ¤¡
    public void Init(IEnumerable<QuestData> _allQuestDatas)
    {
        //ÀüÃ¼ÀûÀ¸·Î ºñ¿ì°í (¾îÂ¼ÇÇ ÃÊ±âÈ­ÇÒ°Å´Ï±î)
        playerQuestProgressTable.Clear();
        available_quests.Clear();
        inProgress_quests.Clear();
        completed_quests.Clear();

        foreach (QuestData questData in _allQuestDatas)
        {
            QuestProgressData progress = new QuestProgressData(questData);
            playerQuestProgressTable.Add(questData.questID,progress);

            available_quests.Add(questData.questID);
        }
    }

    //Äù½ºÆ® ·Îµå
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

    public List<QuestRequirementRef> UpdateQuestInProgress(QuestRequirementType _requireType, QuestTargetType _targetType, int _targetID, int _value)
    {
        switch (_requireType)
        {
            case QuestRequirementType.Kill:
                switch (_targetType)
                {
                    case QuestTargetType.Monster:
                        UpdateInProgress(monsterKillQuestMap, _targetID, _value);
                        return monsterKillQuestMap[_targetID];

                    case QuestTargetType.Npc:
                        break;
                }
                break;

            case QuestRequirementType.CollectItem:
                UpdateInProgress(itemCollectQuestMap, _targetID, _value);
                return itemCollectQuestMap[_targetID];

        }

        return null;
    }

    private void UpdateInProgress(Dictionary<int, List<QuestRequirementRef>> _map, int _targetID, int _value)
    {
        foreach (var questRequirementRef in _map[_targetID])
        {
            int requireCount = playerQuestProgressTable[questRequirementRef.questID].requirementProgresses[questRequirementRef.requirementIndex].requireCount;
            int currentCount = playerQuestProgressTable[questRequirementRef.questID].requirementProgresses[questRequirementRef.requirementIndex].currentCount;
            bool isCompleted = playerQuestProgressTable[questRequirementRef.questID].requirementProgresses[questRequirementRef.requirementIndex].isCompleted;

            int extraCount = currentCount + _value;

            if (extraCount >= requireCount)
            {
                extraCount = requireCount;
                isCompleted = true;
            }
            else
            {
                isCompleted = false;
            }

            playerQuestProgressTable[questRequirementRef.questID].requirementProgresses[questRequirementRef.requirementIndex].currentCount = extraCount;
        }
    }

    //-> ÀÌ ÇÔ¼ö°¡ monsterkill, npc´ëÈ­µî Á¢±ÙÀ» ºü¸£°Ô ÇÏ±â À§ÇÑ ÇØ½Ã¸¦ Ã¤¿ö³ÖÀ» ¿¹Á¤(Accept Quest·Î Äù½ºÆ®¸¦ ¼ö¶ôÇÏ°ÔµÇ¸é µî·ÏÇÏ±âÀ§ÇÔ)
    private void RegisterQuestRequirements(QuestData _questData)
    {
        //Äù½ºÆ®°¡ ¿ä±¸ÇÏ´Â °ÍµéÀ» ÀüºÎ ¼øÈ¸
        for(int i = 0; i < _questData.requirements.Count; i++)
        {
            QuestRequirement questRequirement = _questData.requirements[i];

            switch(questRequirement.requireType)
            {
                case QuestRequirementType.Kill:
                    //if(questRequirement.targetType == QuestTargetType.Monster) // Á×ÀÌ°íÀÚ ÇÏ´Â °ÍÀÌ ¸ó½ºÅÍ¶ó¸é
                    //{
                    //    if (!monsterKillQuestMap.TryGetValue(questRequirement.targetID, out List<QuestRequirementRef> refs))
                    //    {
                    //        refs = new List<QuestRequirementRef>(); // ¾øÀ¸¸é »õ·Ó°Ô ÇÒ´çÇÏ°í

                    //        monsterKillQuestMap[questRequirement.targetID] = refs;
                    //    }

                    //    refs.Add(new QuestRequirementRef(_questData.questID, i)); // ÇÒ´çÇÑ °÷¿¡´Ù°¡ Äù½ºÆ® ¾ÆÀÌµğ¿Í, ÇØ´ç ¿ä±¸»çÇ× ÀÎµ¦½º ³Ö¾îÁÖ±â
                    //} -> caseº°·Î È£­ŒÇÒ °ÍÀÌ¹Ç·Î RegisterQuestRequirement ÇÔ¼ö¸¦ »ı¼ºÇØ¼­ È£ÃâÇÒ ¿¹Á¤
                    if (questRequirement.targetType == QuestTargetType.Monster) // Á×ÀÌ°íÀÚ ÇÏ´Â °ÍÀÌ ¸ó½ºÅÍ¶ó¸é
                    {
                        RegisterQuestRequirement(monsterKillQuestMap, questRequirement.targetID, _questData.questID, i);
                    }
                    break;

                case QuestRequirementType.CollectItem:
                    if(questRequirement.targetType == QuestTargetType.Item) // Äù½ºÆ®°¡ ¿ä±¸ÇÏ´Â Å¸°ÙÀÌ ¾ÆÀÌÅÛÀÌ¶ó¸é
                    {
                        RegisterQuestRequirement(itemCollectQuestMap, questRequirement.targetID, _questData.questID, i);
                    }
                    break;
            }
        }
    }

    private void RegisterQuestRequirement(Dictionary<int,List<QuestRequirementRef>> _map, int _targetID, int _questID, int _requireIndex)
    {
        if(!_map.TryGetValue(_targetID, out List<QuestRequirementRef> _questRequirements))
        {
            _questRequirements = new List<QuestRequirementRef>(); // ¾øÀ¸¸é »õ·Î ÇÒ´ç ÈÄ

            _map.Add(_targetID, _questRequirements); // ³Ö¾îÁÜ
        }

        _questRequirements.Add(new QuestRequirementRef(_questID, _requireIndex)); // ÇØ´ç Äù½ºÆ®¿¡ index¿¡ ÇÊ¿äÇÑ°ÍÀÌ ÀÖ´Ù°í Ãß°¡
    }

    //-> ÀÌ ÇÔ¼ö°¡ monsterkill, npc´ëÈ­µî Á¢±ÙÀ» ºü¸£°Ô ÇÏ±â À§ÇÑ ÇØ½Ã¿¡¼­ ºü¸£°Ô Á¦°Å ¿¹Á¤(Complete Quest¿Í GiveUp Quest(Äù½ºÆ® ¿Ï·á, Äù½ºÆ® Æ÷±â)·Î Äù½ºÆ®¸¦ ¼ö¶ôÇÏ°ÔµÇ¸é µî·ÏÇÏ±âÀ§ÇÔ)
    private void RemoveQuestRequirements(QuestData _questData)
    {
        for (int i = 0; i < _questData.requirements.Count; i++)
        {
            QuestRequirement requirement = _questData.requirements[i];

            switch (requirement.requireType)
            {
                case QuestRequirementType.Kill:
                    if (requirement.targetType == QuestTargetType.Monster)
                    {
                        UnregisterRequirement(monsterKillQuestMap, requirement.targetID,_questData.questID,i);
                    }
                    break;

                case QuestRequirementType.CollectItem:
                    UnregisterRequirement(itemCollectQuestMap,requirement.targetID,_questData.questID,i);
                    break;

            }
        }

    }

    private void UnregisterRequirement(Dictionary<int, List<QuestRequirementRef>> map,int targetID,int questID,int requirementIndex)
    {
        if (!map.TryGetValue(targetID,out List<QuestRequirementRef> refs))
        {
            Debug.LogError($"ÁøÇàÁßÀÎ Äù½ºÆ®°¡ Á¸ÀçÇÏÁö ¾Ê½À´Ï´Ù ¿À·ù¹ß»ı");
            return; // ¾ÖÃÊ¿¡ ÀÖÁöµµ ¾ÊÀº °ÍÀ» Áö¿ì·Á ÇßÀ¸´Ï returnÇÏ°í ¿À·ù¹ß»ı ¸Ş¼¼Áö ¶ç¿ìÀÚ
        }

        refs.RemoveAll(r =>r.questID == questID && r.requirementIndex == requirementIndex);

        // ÇØ´ç targetID¸¦ »ç¿ëÇÏ´Â Äù½ºÆ®°¡ ÀÌÁ¦ ÇÏ³ªµµ ¾øÀ¸¸é
        // Dictionary Key ÀÚÃ¼µµ Á¦°Å
        if (refs.Count == 0)
        {
            map.Remove(targetID);
        }
    }

    //Äù½ºÆ® ¼ö¶ô
    public void AcceptQuest(QuestData _questData)
    {
        int questID = _questData.questID;

        if (!playerQuestProgressTable.TryGetValue(questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"Äù½ºÆ® ID {questID}¿¡ ´ëÇÑ ÁøÇà µ¥ÀÌÅÍ°¡ ¾ø½À´Ï´Ù.");
            return;
        }

        if(questProgressData.questState != QuestState.Available)
        {
            Debug.LogError($"ÇØ´ç Äù½ºÆ®´Â ½ÃÀÛ°¡´ÉÇÑ Äù½ºÆ®°¡ ¾Æ´Ï±â ¶§¹®¿¡ Äù½ºÆ®¸¦ ¼ö¶ôÇÒ ¼ö ¾ø½À´Ï´Ù,");
            return;
        }

        available_quests.Remove(questID); // ½ÃÀÛ°¡´ÉÇÑ Äù½ºÆ®¿¡¼­ Á¦°Å Ã³¸®

        questProgressData.questState = QuestState.InProgress; // Äù½ºÆ® »óÅÂ¸¦ ÁøÇàÁßÀ¸·Î º¯°æ
        //³ª¸ÓÁö´Â ±×´ë·Î Àü´Ş¹ŞÀ½ -> ÁøÇàµµ´Â 0À¸·Î ½ÃÀÛÇß±â¶§¹®

        inProgress_quests.Add(questID); // ÁøÇàÁßÀÎ Äù½ºÆ®¿¡ Ãß°¡ Ã³¸®

        RegisterQuestRequirements(_questData);

    }

    //Äù½ºÆ® Æ÷±â
    public void GiveUpQuest(QuestData _questData)
    {
        int questID = _questData.questID;

        if(!playerQuestProgressTable.TryGetValue(questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"Äù½ºÆ® ID {questID}¿¡ ´ëÇÑ ÁøÇà µ¥ÀÌÅÍ°¡ ¾ø½À´Ï´Ù.");
            return;
        }
        if(questProgressData.questState != QuestState.InProgress)
        {
            Debug.LogError($"ÇØ´ç Äù½ºÆ®´Â ÁøÇàÁßÀÌ ¾Æ´Ï±â ¶§¹®¿¡ Æ÷±âÇÒ ¼ö ¾ø½À´Ï´Ù.");
            return;
        }
        inProgress_quests.Remove(questID); // ÁøÇàÁßÀÎ Äù½ºÆ®¿¡¼­ Á¦°Å Ã³¸®

        questProgressData.questState = QuestState.Available; // Äù½ºÆ® »óÅÂ¸¦ ½ÃÀÛ°¡´ÉÀ¸·Î º¯°æ

        foreach(var questRequireProgress in questProgressData.requirementProgresses)
        {
            questRequireProgress.isCompleted = false; // ¿Ï·á°¡´ÉÇØµµ Æ÷±âÇÑ´Ù´Â°Å´Ï±î Ãë¼Ò
            questRequireProgress.currentCount = 0; // ´Ù½Ã ¿ø»ó º¹±¸
        }
        //questProgressData.currentCount = 0; // ÁøÇà»óÅÂ 0À¸·Î ÃÊ±âÈ­

        available_quests.Add(questID); // ½ÃÀÛ°¡´ÉÇÑ Äù½ºÆ®¿¡ Ãß°¡ Ã³¸® -> ¿Í.. ¤»¤»

        RemoveQuestRequirements(_questData);
    }

    //Äù½ºÆ® ¿Ï·á
    public void CompleteQuest(QuestData _questData)
    {
        int questID = _questData.questID;

        if (!playerQuestProgressTable.TryGetValue(questID, out QuestProgressData questProgressData))
        {
            Debug.LogError($"Äù½ºÆ® ID {questID}¿¡ ´ëÇÑ ÁøÇà µ¥ÀÌÅÍ°¡ ¾ø½À´Ï´Ù.");
            return;
        }
        if(questProgressData.questState != QuestState.InProgress)
        {
            Debug.LogError($"ÇØ´ç Äù½ºÆ®´Â ÁøÇàÁßÀÌ ¾Æ´Ï±â ¶§¹®¿¡ ¿Ï·áÇÒ ¼ö ¾ø½À´Ï´Ù.");
            return;
        }
        inProgress_quests.Remove(questID); // ÁøÇàÁßÀÎ Äù½ºÆ®¿¡¼­ Á¦°Å Ã³¸®

        questProgressData.questState = QuestState.Completed; // Äù½ºÆ® »óÅÂ¸¦ ¿Ï·á·Î º¯°æ
        //questProgressData.currentCount = 0; // ÁøÇà»óÅÂ 0À¸·Î ÃÊ±âÈ­

        completed_quests.Add(questID); // ¿Ï·áÇÑ Äù½ºÆ®¿¡ Ãß°¡ Ã³¸®

        RemoveQuestRequirements(_questData);
    }

    ////Äù½ºÆ® ¿ä±¸»çÇ× °ü·Ã ÇÔ¼ö (°ÔÀÓ È®¼º±â Ã¤³Î¿¡¼­ ¶ç¿ï ¸ñÀû)
    //public IReadOnlyCollection<int> GetQuestByMonsterID(int _monsterID)
    //{
    //    if(monsterKillQuestMap.TryGetValue(_monsterID, out var questIDs))
    //    {
    //        return questIDs;
    //    }

    //    return Array.Empty<int>();
    //}
}
