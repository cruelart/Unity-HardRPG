using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestAvailable : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI availableText; // 진행중 텍스트 찐하게 만들예정

    [SerializeField]
    private GameObject available_panel; 

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
}
