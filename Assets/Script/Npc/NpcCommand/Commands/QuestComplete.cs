using UnityEngine;

public class QuestComplete : INpcCommand
{
    public void Execute()
    {
        TalkUI talkUI = UIManager.Instance.TalkUI;
        talkUI.UIHide();
        QuestManager.Instance.CompleteQuest(talkUI.questID);
    }
}
