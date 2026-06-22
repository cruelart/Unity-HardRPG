using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("상위 부모 설정")]
    [SerializeField]
    private Transform content;

    [Header("인벤토리 슬롯 프리팹 투입")]
    [SerializeField]
    private InventorySlotUI slotPrefab;

    private Re_Inventory inventory;

    private List<InventorySlotUI> slotList = new List<InventorySlotUI>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        inventory.AddConsumerItem(2, 300);
        ShowInventory();
    }

    // Update is called once per frame
    void Update()
    {

        //ShowInventory();
    }

    public void init(Re_Inventory _inventory)
    {
        inventory = _inventory;
        
        //ShowInventory();
    }

    private void ClearSlots()
    {
        foreach(var slot in slotList) // 남아있는 ui찌꺼기들 싹다 제거
        {
            Destroy(slot);
        }

        slotList.Clear(); // 다 비워버리기
    }

    public void ShowInventory()
    {
        ClearSlots();
        //Debug.Log("ShowInventory가 호출됐습니다");
        foreach (var item in inventory.consumerItemList)
        {
            InventorySlotUI slot = Instantiate(slotPrefab, content);

            slot.SetItem(item);

            slotList.Add(slot);
        }
    }
}
