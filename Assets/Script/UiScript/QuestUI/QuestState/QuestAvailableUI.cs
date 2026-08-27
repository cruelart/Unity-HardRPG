using NUnit.Framework;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAvailableUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI availableText; // 진행중 텍스트 찐하게 만들예정

    [SerializeField]
    private GameObject available_prefab; // 진행중 프리팹

    private List<QuestContent> availableQuestList = new List<QuestContent>(); // 진행중 퀘스트 리스트

    [SerializeField]
    private Transform questListTransform; // 퀘스트 리스트ui의 위치(실제 보일 장소) -> content 받아오면 될듯

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAvailableQuest(QuestData _questData, QuestProgressData _questProgressData)
    {
        QuestContent availableQuestContent = Instantiate(available_prefab, questListTransform).GetComponent<QuestContent>();
        availableQuestContent.Init(_questData, _questProgressData);

        availableQuestList.Add(availableQuestContent);
    }
}
