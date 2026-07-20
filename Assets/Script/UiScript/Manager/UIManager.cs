using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private PlayerStatusUI playerStatusUI;
    public PlayerStatusUI StatusUI => playerStatusUI; // 받아오고 읽기전용으로 변경(식 본문 프로펄티)

    [SerializeField]
    private PlayerStateUI playerStateUI;
    public PlayerStateUI StateUI => playerStateUI; // 받아오고 읽기전용으로 변경(식 본문 프로펄티)

    [SerializeField]
    private InventoryUIManager playerInventoryUI;
    public InventoryUIManager InventoryUI => playerInventoryUI;

    [SerializeField]
    private EquipSpaceUIManager playerEquipSpaceUI;
    public EquipSpaceUIManager EquipSpaceUI => playerEquipSpaceUI;

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
        playerInventoryUI.gameObject.transform.SetAsLastSibling();
        playerInventoryUI.gameObject.SetActive(true);
    }

    public void HideInventoryUI()
    {
        playerInventoryUI.gameObject.SetActive(false);
    }

    public void ShowEquipSpaceUI()
    {
        playerEquipSpaceUI.gameObject.transform.SetAsLastSibling();
        playerEquipSpaceUI.gameObject.SetActive(true);
    }

    public void HideEquipSpaceUI()
    {
        playerEquipSpaceUI.gameObject.SetActive(false);
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

    public void ShowPlayerStatusUI()
    {
        playerStatusUI.transform.SetAsLastSibling();
        playerStatusUI.gameObject.SetActive(true);
    }

    public void HidePlayerStatusUI()
    {
        playerStatusUI.gameObject.SetActive(false);
    }

}
