using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Re_Inventory inventoryDB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Re_Inventory _inventoryDB)
    {
        inventoryDB = _inventoryDB;
    }

    private BaseitemDB GetItemInformation(int _itemID)
    {
        return ItemDataManager.Instance.GetBaseitemDB(_itemID);
    }

    public bool isPossibleGetItem(int _itemID, int _amount)
    {
        BaseitemDB Item = GetItemInformation(_itemID); // 인벤토리에 집어넣을 아이템

        BaseitemDB.ItemType itemTypeName = Item.itemType;
        //Debug.Log("BaseitemDB.ItemType" + itemTypeName);

        //타입에 따라 아이템넣기
        switch (itemTypeName)
        {
            //장비
            case BaseitemDB.ItemType.Equipment:
                return !inventoryDB.isFullEquipItem();

            //소비
            case BaseitemDB.ItemType.Consumable:
                return !inventoryDB.isFullConsumerItem(_itemID, _amount);
            //기타
            case BaseitemDB.ItemType.Etc:
                break;
        }

        return true;
    }

    public void RemoveItemInInventory(int _slotIndex)
    {
        inventoryDB.RemoveEquipmentItem(_slotIndex);
    }

    public void AddItemInInventory(int _itemID, int _amount)
    {
        BaseitemDB Item = GetItemInformation(_itemID); // 인벤토리에 집어넣을 아이템

        BaseitemDB.ItemType itemTypeName = Item.itemType;
        Debug.Log("BaseitemDB.ItemType" + itemTypeName);
        
        //타입에 따라 아이템넣기
        switch(itemTypeName)
        {
            //장비
            case BaseitemDB.ItemType.Equipment:
                inventoryDB.AddEquipmentItem(_itemID, _amount);
                break;

            //소비
            case BaseitemDB.ItemType.Consumable:
                Debug.Log("InventoryManager : MonoBehaviour : 소비아이템을 획득하였습니다");
                inventoryDB.AddConsumerItem(_itemID, _amount);
                break;

            //기타
            case BaseitemDB.ItemType.Etc:
                break;
        }
    }

    public void EquipItem(int _slotIndex)
    {
        EquipmentItemInstance item = inventoryDB.equipmentItemSlotList[_slotIndex].item;

        //인벤토리에서 장비 지우고
        RemoveItemInInventory(_slotIndex);

        EquipSpaceManager.Instance.EquipItem(item);
    }
}
