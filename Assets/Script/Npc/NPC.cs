using UnityEngine;

public interface ITalkInteractable
{
    Transform InteractionTransform { get; }
    void Interact(Transform _interactor);
    void ExitInteraction();
}

public class NPC : MonoBehaviour , ITalkInteractable
{
    [SerializeField]
    protected NPCData npcData;

    public Transform InteractionTransform => transform;

    protected int textIndex = 0;
    protected int questIndex = 0;

    public Transform npcTransform { get; private set; }
    
    protected void NpcInit()
    {
        npcTransform = this.transform;
        //NpcManager.Instance.AddNpc(npcData.npcName, this);
    }

    protected virtual void DecideTextIndex()
    {
        // 기본 구현은 없음, 필요에 따라 서브클래스에서 오버라이드
    }

    public virtual void Interact(Transform _interactor)
    {
        DecideTextIndex();

        UIManager.Instance.ShowNpcTalkUI();
        UIManager.Instance.NpcTalkUI.Init(npcData, textIndex, questIndex);
    }

    public virtual void ExitInteraction()
    {
        
    }


}
