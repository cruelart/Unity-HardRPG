using UnityEngine;
public class NPC : MonoBehaviour
{
    protected WanderingTraderAIController npcController;
    protected NpcTalkInteraction npcTalkInteraction;

    protected virtual void Awake()
    {
        npcController = GetComponent<WanderingTraderAIController>();
        npcTalkInteraction = GetComponent<NpcTalkInteraction>();
    }

    public Transform npcTransform { get; private set; }
    
    protected void NpcInit()
    {
        npcTransform = this.transform;
        //NpcManager.Instance.AddNpc(npcData.npcName, this);
    }

}
