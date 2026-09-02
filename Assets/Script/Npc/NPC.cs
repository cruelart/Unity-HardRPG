using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    protected NPCData npcData;

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
}
