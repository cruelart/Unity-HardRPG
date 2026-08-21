using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //플레이어 기본 정보 저장(스텟, 이름 등등)
    public PlayerStatData playerSaveData;

    //해당 플레이어의 퀘스트 진행 상황 저장
    public List<QuestProgressData> playerProgressSaveData;

    //해당 플레이어의 인벤토리 저장데이터
    public Re_Inventory playerInventorySaveData;

    //해당 플레이어의 장비창 저장 데이터
    public EquipSpace equipSpaceSaveData;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string savePath; // 저장 경로

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject); // 파괴 방지

        savePath = Path.Combine(Application.persistentDataPath, "PlayerSave.json");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData();
        QuestProgressDataList questProgressDataList = new QuestProgressDataList();

        //플레이어의 정보들을 저장하는 코드 작성 예정
        saveData.playerSaveData = PlayerStatManager.Instance.GetPlayerSaveStatData();
        questProgressDataList = QuestManager.Instance.GetQuestProgressDataList();
    }

}
