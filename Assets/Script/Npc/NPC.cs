using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    protected NPCData npcData;

    public Transform npcTransform { get; private set; }
    
    protected void NpcInit()
    {
        npcTransform = this.transform;
        NpcManager.Instance.AddNpc(npcData.npcName, this);
    }
}
