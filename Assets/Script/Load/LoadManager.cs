using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    //플레이어 스탯 데이터 저장 경로
    private static string SavePlayerStatDBPath => Path.Combine(Application.persistentDataPath, "playerDB.json"); // 저장경로는 playerDB.json 파일

    //플레이어 퀘스트 데이터 저장 경로
    private static string SavePlayerQuestPath => Path.Combine(Application.persistentDataPath, "playerQuest.json");

    //플레이어 

    public static LoadManager Instance { get; private set; }

    //불러올 데이터 목록들
    public PlayerStatData playerDB { get; private set; } // 플레이중인 플레이어의 데이터
    public List<QuestProgressData> playerQuestList { get; private set; } // 플레이어의 현 퀘스트 상황 목록

    private void Awake()
    {

        //퀘스트 로드

        //장비창 로드

        //인벤토리 로드

        DontDestroyOnLoad(gameObject); // 씬 전환때 오브젝트 파괴 방지
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerStatManager.Instance.Init(PlayerStatDataLoad()); // 플레이어 스탯 매니저쪽에 로드한 데이터를 넣어줌
        QuestManager.Instance.InitLoadData(PlayerQuestDataLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerStatData PlayerStatDataLoad()
    {
        if (File.Exists(SavePlayerStatDBPath))
        {
            string json = File.ReadAllText(SavePlayerStatDBPath); // 저장된 파일경로에서 파일 읽고
            PlayerStatData data = JsonUtility.FromJson<PlayerStatData>(json); // json을 PlayerDB클래스에 맞게 변환
            return data;
        }
        Debug.Log("PlayerDBManager에서 제이슨파일을 찾지 못해서 새롭게 PlayeDB를 생성합니다");
        return new PlayerStatData(); // 파일 없으면 기본값으로 세팅
    }

    public List<QuestProgressData> PlayerQuestDataLoad()
    {
        if (File.Exists(SavePlayerQuestPath))
        {
            string json = File.ReadAllText(SavePlayerQuestPath);
            QuestProgressDataList data = JsonUtility.FromJson<QuestProgressDataList>(json); 
            return data.questProgressDatas;
        }
        Debug.Log("PlayerDBManager에서 제이슨파일을 찾지 못해서 새롭게 PlayeDB를 생성합니다");
        return new List<QuestProgressData>(); // 파일 없으면 기본값으로 세팅
    }
}
