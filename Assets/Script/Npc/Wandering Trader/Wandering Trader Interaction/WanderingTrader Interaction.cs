using UnityEngine;

public class WanderingTraderInteraction : NpcTalkInteraction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DecideTextIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(Transform _targetTransform)
    {
        base.Interact(_targetTransform);
    }

    public override void ExitInteraction()
    {

    }

    protected override void DecideTextIndex()
    {
        //
    }
}
