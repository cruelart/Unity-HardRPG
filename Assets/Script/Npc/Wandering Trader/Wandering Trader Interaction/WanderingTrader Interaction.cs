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

    public override void ExitInteraction()
    {

    }

    protected override void DecideTextIndex()
    {
        //1. 어떤 퀘스트도 받은 적이 없다면
    }
}
