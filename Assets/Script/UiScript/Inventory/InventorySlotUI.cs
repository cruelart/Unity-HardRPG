using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TMP_Text countText;

    private ConsumerItemInstance item;

    public void SetItem(ConsumerItemInstance _item)
    {
        item = _item;
        //Debug.Log("InventorySlotUI" + item.count);

        itemIcon.sprite = item.setting.itemIcon;

        if (item.count > 1)
        {
            Debug.Log("item.count" + item.count);
            countText.text = item.count.ToString();
        }
        else
        {
            countText.text = "";
        }
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
}