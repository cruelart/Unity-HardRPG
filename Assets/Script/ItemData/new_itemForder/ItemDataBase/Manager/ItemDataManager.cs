using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance; // 싱글톤 선언

    [Header("Asset Database")]
    private Dictionary<int, ItemRawData> itemRawDataMap = new Dictionary<int, ItemRawData>();

    void Awake()
    {
        Instance = this;
        LoadAllJsonData();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadAllJsonData()
    {
        Debug.Log($"제이슨 데이터를 로드하는 중입니다.");
        //장비아이템 로드
        LoadJson("EquipmentItemData");
        //소비아이템 로드
        //LoadJson("ConsumerItemData");
        //기타아이템 로드
        //LoadJson("EtcItemData");

        Debug.Log($"모든 데이터 로드 완료: {itemRawDataMap.Count}개의 항목");
    }

    //Json에 저장된 데이터들을 해시테이블에 저장하는  함수
    //Why? -> 그대로 List로 받아오게되면 반복문으로 하나하나 찾아야되는데 비효율적이므로 단순 탐색 검색등은 해시테이블이 빠르므로 설정
    void LoadJson(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName + ".json"); // 파일 경로
        if (File.Exists(path)) // 파일 존재시
        {
            string jsonText = File.ReadAllText(path); // 제이슨에 있는 파일 내용 받아오고
            Debug.Log(jsonText);
            ItemDataWrapper wrapper = JsonUtility.FromJson<ItemDataWrapper>(jsonText);

            foreach (ItemRawData data in wrapper.items)
            {
                itemRawDataMap[data.id] = data; // id를 키값으로 두고 id포함 데이터 저장
            }
        }
    }

    public EquipmentItemInstance CreateEquipmentItemInstance(int id)
    {
        // 데이터 찾기
        if (!itemRawDataMap.TryGetValue(id, out ItemRawData raw)) return null;

        // 일치하는 이름을 가진 ScriptableObject 에셋 찾기
        //EquipmentItemDB asset = itemAssets.Find(a => a.itemName == raw.itemName); -> 이거보단 아래가 더 나을듯 리스트에 하나하나 등록하는걸 방지하기위함
        EquipmentItemDB asset = Resources.Load<EquipmentItemDB>($"Items/{raw.itemName}");

        if (asset != null)
        {
            return new EquipmentItemInstance(asset, raw);
        }

        Debug.LogError($"{raw.itemName}에 해당하는 SO 에셋을 찾을 수 없습니다!");
        return null;
    }

    public ConsumerItemInstance CreateConsumerItemInstance(int id)
    {
        // 데이터 찾기
        if (!itemRawDataMap.TryGetValue(id, out ItemRawData raw)) return null;

        // 일치하는 이름을 가진 ScriptableObject 에셋 찾기
        //EquipmentItemDB asset = itemAssets.Find(a => a.itemName == raw.itemName); -> 이거보단 아래가 더 나을듯 리스트에 하나하나 등록하는걸 방지하기위함
        ConsumerItemDB asset = Resources.Load<ConsumerItemDB>($"Items/{raw.itemName}");

        if (asset != null)
        {
            return new ConsumerItemInstance(asset, raw);
        }

        Debug.LogError($"{raw.itemName}에 해당하는 SO 에셋을 찾을 수 없습니다!");
        return null;
    }


}
