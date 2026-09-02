using NUnit.Framework;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalkUI : UIBase
{
    private NPCData npcData; // 어떤 타입의 npc 대화창을 띄울건지 데이터 받아오기
    public int questID { get; private set; } = 0; // 

    [SerializeField]
    private TextMeshProUGUI npcName;

    [SerializeField]
    private NpcTalkText npcTalkText;

    [SerializeField]
    private NpcTalkButtonManager npcTalkButton;

    private List<NpcTextData> npcTexts; // 보여줄 대화 내용

    private int currentIndex; // 현재 실행중인 인덱스번호

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(NPCData _npcData, int _textIndex, int _questIndex)
    {

        currentIndex = 0;

        npcData = _npcData;
        npcTexts = npcData.npcTalkDatas[_textIndex].npcTexts; // 참조
        questID = npcData.npcQuestIDs[_questIndex];

        npcName.text = npcData.npcTalkDatas[_textIndex].npcTexts[currentIndex].npcName; // 수정예정 -> 따로 이름 변경하는 스크립트 짤까 고민중

        npcTalkText.ShowNpcTalkText(npcTexts, currentIndex);
        npcTalkButton.ShowButton(npcTexts[currentIndex].buttonTypes);
    }

    public void NextText()
    {
        npcTalkText.ShowNpcTalkText(npcTexts, ++currentIndex);
        npcName.text = npcTexts[currentIndex].npcName;
        npcTalkButton.ShowButton(npcTexts[currentIndex].buttonTypes);
        Debug.Log("다음");
    }

    public void OnClickButton(TalkButtonType _type)
    {
        switch (_type)
        {
            case TalkButtonType.OpenShop:
                break;
        }
    }

}
