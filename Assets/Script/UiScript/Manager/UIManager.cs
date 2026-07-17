using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private EquipSpaceUI equipSpaceUI;

    [SerializeField]
    private EquipToolTip equipToolTip;

    [SerializeField]
    private ConsumerToolTip consumerToolTip;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowInventoryUI()
    {
        inventoryUI.gameObject.transform.SetAsLastSibling();
        inventoryUI.gameObject.SetActive(true);
    }

    public void HideInventoryUI()
    {
        inventoryUI.gameObject.SetActive(false);
    }

    public void ShowEquipSpaceUI()
    {
        equipSpaceUI.gameObject.transform.SetAsLastSibling();
        equipSpaceUI.gameObject.SetActive(true);
    }

    public void HideEquipSpaceUI()
    {
        equipSpaceUI.gameObject.SetActive(false);
    }

    public void ShowEquipToolTip(EquipmentItemInstance _item, RectTransform _slotRect)
    {
        equipToolTip.transform.SetAsLastSibling();
        equipToolTip.Show(_item, _slotRect);
    }
    
    public void HideEquipToolTip()
    {
        equipToolTip.Hide();
    }

    public void ShowConsumerToolTip(ConsumerItemInstance _item, RectTransform _slotRect)
    {
        consumerToolTip.transform.SetAsLastSibling();
        consumerToolTip.Show(_item, _slotRect);
    }

    public void HideConsumerToolTip()
    {
        consumerToolTip.Hide();
    }

}
