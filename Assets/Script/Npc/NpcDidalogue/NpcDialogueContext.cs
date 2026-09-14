using UnityEngine;

public sealed class NpcDialogueContext
{
    public IQuestStateReader questStateReader { get; private set; }

    public Transform npcTransform { get; private set; }

    public Transform targetTransform { get; private set; }

    public NpcDialogueContext(IQuestStateReader _questStateReader, Transform _npcTransform, Transform _targetTransform)
    {
        this.questStateReader = _questStateReader;
        this.npcTransform = _npcTransform;
        this.targetTransform = _targetTransform;
    }
}
