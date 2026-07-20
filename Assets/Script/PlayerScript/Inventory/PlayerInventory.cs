using UnityEngine;

//더미데이터로 남겨놓자
public class PlayerInventory : MonoBehaviour
{
    private Re_Inventory inventoryDB;
    private EquipSpace equipSpaceDB;
    private PlayerDropItemInteraction playerDropItemInteraction;

    [SerializeField]
    private InventoryUIManager inventoryUI;

    [SerializeField]
    private EquipSpaceUIManager equipSpaceUI;

    private void Awake()
    {
        inventoryDB = GetComponent<Re_Inventory>();
        inventoryDB.Init();

        InventoryManager.Instance.Init(inventoryDB);

        equipSpaceDB = GetComponent<EquipSpace>();

        EquipSpaceManager.Instance.Init(equipSpaceDB);

        playerDropItemInteraction = GetComponent<PlayerDropItemInteraction>();
        inventoryUI.Init(inventoryDB);
        playerDropItemInteraction.Init(inventoryDB);
        equipSpaceUI.Init(equipSpaceDB);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
