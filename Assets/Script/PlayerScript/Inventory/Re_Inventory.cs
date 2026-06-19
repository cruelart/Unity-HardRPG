using System.Collections.Generic;
using UnityEngine;

public class Re_Inventory : MonoBehaviour
{
    //장비 아이템
    public List<EquipmentItemInstance> equipmentItemList = new List<EquipmentItemInstance>(); // 인벤토리 나열용
    //public Dictionary<long, EquipmentItemInstance> equipmentItemMap = new Dictionary<long, EquipmentItemInstance>(); // 검색, 탐색용 ex) 퀘스트 아이템확인용? 

    //소비 아이템
    public List<ConsumerItemInstance> consumerItemList = new List<ConsumerItemInstance>(); // 인벤토리 나열용
    //public Dictionary<long, ConsumerItemInstance> consumerItemMap = new Dictionary<long, ConsumerItemInstance>(); // 아이템 id, 실제 아이템
    public Dictionary<long, int> consumerItemMap = new Dictionary<long, int>(); // 아이템 고유id가 인벤토리내에 총 몇개가 있는지 확인하는 용도(퀘스트 체크용도 등등)

    //소비아이템 합치는 용도 -> ex) (하얀포션 키값, 하얀포션들의 집합체) -> 여기서 하얀포션이 99개가 아닌것을 찾고 전부 99개다? 그럼 새롭게 추가하는 형식
    public Dictionary<long, List<ConsumerItemInstance>> consumerItemSearchItemID = new Dictionary<long, List<ConsumerItemInstance>>();

    //기타아이템


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AddEquipmentItem(ItemDataManager.Instance.CreateEquipmentItemInstance(10001)); // 테스트용 나무검 생성
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("구매 금액:" + equipmentItemList[0].settings.buy_gold);
        //Debug.Log("판매 금액:" + equipmentItemList[0].settings.sell_gold);
    }

    //장비아이템 인벤토리 추가 함수
    public void AddEquipmentItem(int _itemID, int _amount)
    {
        //equipmentItemList.Add(_item);
        //equipmentItemMap.Add(_item.instanceID, _item);

    }

    //소비아이템 인벤토리 추가 함수
    public void AddConsumerItem(int _itemID, int _amount)
    {
        //consumerItemList.Add(_item);
        //consumerItemMap.Add(_item.instanceID, _item);
        ConsumerItemDB new_consumerItemDB = ItemDataManager.Instance.GetConsumerItemDB(_itemID);

        //기존 소비아이템 채우기 -> 하얀포션 30개, 50개, 98개, 99개, 97개인데 addconsumeritem(?, 1000)을 하면 99개, 99개, 99개, 99개, 99개 ... 이렇게 채우기용
        if(consumerItemSearchItemID.TryGetValue(_itemID, out var list))
        {
            foreach(var item in list)
            {
                if(item.count >= new_consumerItemDB.maxNum) // 해당 아이템이 이미 최대 수치라면 패스
                {
                    continue;
                }

                int maxAddNum = new_consumerItemDB.maxNum - item.count; // 최대 증가가능 갯수

                int realAddNum = Mathf.Min(maxAddNum, _amount); //실제 증가시킬 갯수

                item.AddCount(realAddNum);

                _amount -= realAddNum;

                if(_amount <= 0)
                {
                    return;
                }
            }
        }
        
        //채울거 다 채웠으면
        while (_amount > 0) // 설정한 값 전부 생성할때까지 무한 생성
        {
            int count = Mathf.Min(_amount, new_consumerItemDB.maxNum); // 생성시도 갯수 vs 최대 갯수

            ConsumerItemInstance newConsumerItem = ItemDataManager.Instance.CreateConsumerItemInstance(_itemID); // 아이템 생성

            //Debug.Log("아이템생성중입니다 생성 아이템이름: ");
            newConsumerItem.AddCount(count);

            //consumerItemList.Add(newConsumerItem);
            //consumerItemMap.Add(newConsumerItem.setting.item, newConsumerItem);

            RegisterConsumerItem(newConsumerItem, count);
            _amount -= count;
        }
    }

    //아이템 추가시 등록 함수
    private void RegisterConsumerItem(ConsumerItemInstance _item , int _count)
    {
        consumerItemList.Add(_item);

        int itemID = _item.setting.itemID; // 고유아이디 받아오자

        if (!consumerItemMap.TryGetValue(itemID, out var itemNum))// 만약 인벤토리에 하나도 없던 상태라면
        {
            consumerItemMap.Add(itemID, _count); // 갯수넣기
        }
        else
        {
            consumerItemMap[itemID] += _count;
        }

        if(!consumerItemSearchItemID.TryGetValue(itemID, out var list)) // 인벤토리에 포션저장리스트같은게 하나도 없엇다?
        {
            list = new List<ConsumerItemInstance>(); // 그럼 만들어주자

            consumerItemSearchItemID.Add(itemID, list);
        }

        list.Add(_item);
    }

    //장비아이템 정렬
    public void SortEquipmentItem()
    {
        equipmentItemList.Sort((a, b) => a.settings.itemID.CompareTo(b.settings.itemID)); // 아이템 고유 ID 순서대로 정렬 
    }

    //소비아이템 정렬
    public void SortConsumerItem()
    {
        consumerItemList.Sort((a, b) => a.setting.itemID.CompareTo(b.setting.itemID)); // 아이템 고유 ID 순서대로 정렬 
    }
}
