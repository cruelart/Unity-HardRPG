using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestExplainUI : UIBase
{
    [Header("퀘스트 이름")]
    [SerializeField]
    private TextMeshProUGUI questNameText;

    [Header("퀘스트 제공 Npc")]
    [SerializeField]
    private Image StartNpcImage;

    [SerializeField]
    private TextMeshProUGUI StartNpcNameText;

    [SerializeField]
    private TextMeshProUGUI StartNpcTransformText;

    [Header("퀘스트 완료 Npc")]
    [SerializeField]
    private Image EndNpcImage;

    [SerializeField]
    private TextMeshProUGUI EndNpcNameText;

    [SerializeField]
    private TextMeshProUGUI EndNpcTransformText;

    [Header("퀘스트 설명란")]
    [SerializeField]
    private TextMeshProUGUI questDescriptionText;

    [Header("보상")]
    [SerializeField]
    private TextMeshProUGUI rewardMoneyText;

    [SerializeField]
    private TextMeshProUGUI rewardExpText;

    [SerializeField]
    private TextMeshProUGUI rewardItemNameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(QuestData _questData, QuestProgressData _questProgressData)
    {
        questNameText.text = _questData.questName;

        StartNpcImage.sprite = _questData.StartNpcImage;
        StartNpcNameText.text = $"{_questData.StartNpcName}";
        StartNpcTransformText.text = $"{_questData.StartNpcTransform}";

        EndNpcImage.sprite = _questData.EndNpcImage;
        EndNpcNameText.text = $"{_questData.EndNpcName}";
        EndNpcTransformText.text = $"{_questData.EndNpcTransform}";

        questDescriptionText.text = _questData.questDescription;

        rewardMoneyText.text = $"루비: {_questData.ClearGold}";
        rewardExpText.text = $"경험치: {_questData.ClearExp}";
        rewardItemNameText.text = _questData.ClearItem != null ? $"아이템: {_questData.ClearItem.itemName}" : "없음";
    }
}
