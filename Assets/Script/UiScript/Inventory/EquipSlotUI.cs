using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TMP_Text countText;

    private EquipmentItemInstance equip_item;

    [SerializeField]
    private RectTransform rectTansform;

    public bool isSettingItem { get; private set; }

    public void SetItem(EquipmentItemInstance _item)
    {
        isSettingItem = true;

        itemIcon.enabled = true;
        equip_item = _item;
        //Debug.Log("InventorySlotUI" + item.count);

        itemIcon.sprite = equip_item.setting.itemIcon;

        countText.text = "";
    }

    public void ClearItem()
    {
        isSettingItem = false;

        itemIcon.sprite = null;
        itemIcon.enabled = false;
        countText.text = "";
    }

    //아이템 정보를 보여주는 UI 호출시키기
    public void ShowItemInformation()
    {

    }

    //public void SetItem(EquipmentItemInstance _item)
    //{
    //    item = _item;

    //    itemIcon.sprite = item.setting.itemIcon;

    //    if (item.count > 1)
    //    {
    //        countText.text = item.count.ToString();
    //    }
    //    else
    //    {
    //        countText.text = "";
    //    }
    //}


    //마우스 드래그 아이템 정보관련 함수

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(equip_item == null)
        {
            Debug.Log("EquipSlotUI에 있는 equip_item이 비어있어");
            return;
        }
        UIManager.Instance.ShowEquipToolTip(equip_item, rectTansform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideEquipToolTip();
    }
}