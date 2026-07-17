using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Re_Inventory inventoryDB;
    private EquipSpace equipSpace;
    private PlayerDropItemInteraction playerDropItemInteraction;

    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private EquipSpaceUI equipSpaceUI;

    private void Awake()
    {
        inventoryDB = GetComponent<Re_Inventory>();
        inventoryDB.Init();

        InventoryManager.Instance.Init(inventoryDB);

        equipSpace = GetComponent<EquipSpace>();

        EquipSpaceManager.Instance.Init(equipSpace);

        playerDropItemInteraction = GetComponent<PlayerDropItemInteraction>();
        inventoryUI.init(inventoryDB);
        playerDropItemInteraction.Init(inventoryDB);
        equipSpaceUI.Init(equipSpace);
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
