using System.Xml.Linq;
using UnityEngine;

public class WanderingTrader : NPC
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        NpcInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowNpcTalkUI();
            UIManager.Instance.NpcTalkUI.Init(npcData);
        }
    }
}
