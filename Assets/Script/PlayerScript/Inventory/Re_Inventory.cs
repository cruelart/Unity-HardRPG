using System.Collections.Generic;
using UnityEngine;

public class Re_Inventory : MonoBehaviour
{
    //장비 아이템
    public List<EquipmentItemInstance> equipmentItemList = new List<EquipmentItemInstance>(); // 인벤토리 나열용
    public Dictionary<int, EquipmentItemInstance> equipmentItemMap = new Dictionary<int, EquipmentItemInstance>(); // 검색, 탐색용 ex) 퀘스트 아이템확인용? 

    //소비 아이템
    public List<ConsumerItemInstance> consumerItemList = new List<ConsumerItemInstance>(); // 인벤토리 나열용
    public Dictionary<int, ConsumerItemInstance> consumerItemMap = new Dictionary<int, ConsumerItemInstance>(); // 아이템 고유id, 실제 아이템

    //기타아이템

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddEquipmentItem(ItemDataManager.Instance.CreateEquipmentItemInstance(10001)); // 테스트용 나무검 생성
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("구매 금액:" + equipmentItemList[0].data.buy_gold);
        Debug.Log("판매 금액:" + equipmentItemList[0].data.sell_gold);
    }

    //장비아이템 인벤토리 추가 함수
    public void AddEquipmentItem(EquipmentItemInstance _item)
    {
        equipmentItemList.Add(_item);
        equipmentItemMap.Add(_item.data.id, _item);
    }

    //소비아이템 인벤토리 추가 함수
    public void AddConsumerItem(ConsumerItemInstance _item)
    {
        consumerItemList.Add(_item);
        consumerItemMap.Add(_item.data.id, _item);
    }

    //장비아이템 정렬
    public void SortEquipmentItem()
    {
        equipmentItemList.Sort((a, b) => a.data.id.CompareTo(b.data.id)); // 아이템 고유 ID 순서대로 정렬 
    }

    //소비아이템 정렬
    public void SortConsumerItem()
    {
        consumerItemList.Sort((a, b) => a.data.id.CompareTo(b.data.id)); // 아이템 고유 ID 순서대로 정렬 
    }
}
