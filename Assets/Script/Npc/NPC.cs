using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    protected NpcAIController npcAIController;
    protected NpcTalkInteraction npcTalkInteraction;

    protected virtual void Awake()
    {
        npcAIController = GetComponent<NpcAIController>();
        npcTalkInteraction = GetComponent<NpcTalkInteraction>();
    }

    protected virtual void OnEnable()
    {
        npcTalkInteraction.OnNpcInteraction += npcAIController.NpcInteraction;
    }

    protected virtual void OnDisable()
    {
        npcTalkInteraction.OnNpcInteraction -= npcAIController.NpcInteraction;
    }

    public Transform npcTransform { get; private set; }
    
    protected void NpcInit()
    {
        npcTransform = this.transform;
        //NpcManager.Instance.AddNpc(npcData.npcName, this);
    }

}
