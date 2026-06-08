using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Re_Inventory Inventory { get; private set; }

    [SerializeField]
    private InventoryUI inventoryUI;

    private void Awake()
    {
        Instance = this;

        Inventory = new Re_Inventory();

        inventoryUI.init(Inventory);
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
