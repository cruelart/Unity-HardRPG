using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class QuestContent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI questContentText; // 퀘스트 내용 텍스트

    private QuestData questData;
    private QuestProgressData questProgressData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(QuestData _questData, QuestProgressData _questProgressData )
    {
        questData = _questData;
        questProgressData = _questProgressData;
        questContentText.text = _questData.questName;
    }
    public void OnClick()
    {

    }
}
