using UnityEngine;

public class WanderingTrader : MonoBehaviour
{
    [SerializeField]
    private NPCData npcData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(npcData.npcTexts[0].npcText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.ShowNpcTalkUI();
            UIManager.Instance.NpcTalkUI.Init(npcData);
        }
    }
}
