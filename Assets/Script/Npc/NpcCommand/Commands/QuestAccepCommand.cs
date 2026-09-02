using UnityEngine;

public class QuestAcceptCommand : INpcCommand
{
    public void Execute()
    {
       TalkUI talkUI = UIManager.Instance.TalkUI;
        talkUI.UIHide();
       QuestManager.Instance.AcceptQuest(talkUI.questID);
    }
}
