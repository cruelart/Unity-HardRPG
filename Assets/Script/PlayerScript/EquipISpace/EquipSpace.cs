using UnityEngine;
using System;

public class EquipSpace : MonoBehaviour
{
    public event Action<EquipmentItemInstance> OnChangeEquipSpaceUI;
    //무기
    private EquipmentItemInstance weapon;

    //방패
    private EquipmentItemInstance hat;

    //장갑
    private EquipmentItemInstance glove;

    //신발
    private EquipmentItemInstance shoes;

    //견장
    private EquipmentItemInstance armor;

    //귀걸이
    private EquipmentItemInstance shield;

    public void Setitem(EquipmentItemInstance _newItem, ref EquipmentItemInstance _usingItem)
    {
        //장착중인 장비가 있었다면
        if(_usingItem != null)
        {
            if(!InventoryManager.Instance.AddItemInInventory(_usingItem, 1)) // -> 어처피 여기서 공지 띄울거임
            {
                //GameEventChannel.OnNotify?.Invoke("장비 인벤토리가 꽉 찼습니다. 한칸이상 비워주세요");
                return;
            }

            MinusStatItem(_usingItem);
        }

        _usingItem = _newItem;
        OnChangeEquipSpaceUI?.Invoke(_usingItem);
        GameEventChannel.OnNotify?.Invoke(_usingItem.setting.itemName + "을 장착하였습니다");

        //무기에 담긴 모든 스텟을 더해주기
        PlusStatItem(_newItem);
    }

    void Update()
    {
    }

    public void EquipItem(EquipmentItemInstance _item)
    {
        EquipmentItemDB.EquipmentType item_type = _item.setting.type;
        Debug.Log("EquipSpace" + item_type);

        switch (item_type)
        {
            //장착부위 무기
            case EquipmentItemDB.EquipmentType.Weapon:
                //Debug.Log("무기를 장착하셨습니다");
                Setitem(_item, ref weapon);

                break;

            //방패
            case EquipmentItemDB.EquipmentType.Hat:

                Setitem(_item, ref hat);

                break;
            
            //장갑
            case EquipmentItemDB.EquipmentType.Glove:

                Setitem(_item, ref glove);

                break;

            //신발
            case EquipmentItemDB.EquipmentType.Boots:

                Setitem(_item, ref shoes);

                break;

            //귀걸이
            case EquipmentItemDB.EquipmentType.Shield:

                Setitem(_item, ref shield);

                break;

            //갑옷
            case EquipmentItemDB.EquipmentType.Armor:

                Setitem(_item, ref armor);

                break;
        }
    }

    public void TakeOffItem(EquipmentItemInstance _item)
    {
        EquipmentItemDB.EquipmentType item_type = _item.setting.type;
        Debug.Log("EquipSpace" + item_type);

        switch (item_type)
        {
            //장착부위 무기
            case EquipmentItemDB.EquipmentType.Weapon:
                //Debug.Log("무기를 장착하셨습니다");
                weapon = null;

                break;

            //방패
            case EquipmentItemDB.EquipmentType.Hat:

                hat = null;

                break;

            //장갑
            case EquipmentItemDB.EquipmentType.Glove:

                glove = null;

                break;

            //신발
            case EquipmentItemDB.EquipmentType.Boots:

                shoes = null;

                break;

            //귀걸이
            case EquipmentItemDB.EquipmentType.Shield:

                shield = null;

                break;

            //갑옷
            case EquipmentItemDB.EquipmentType.Armor:

                armor = null;

                break;
        }

        MinusStatItem(_item);
    }

    //스텟증가
    public void PlusStatItem(EquipmentItemInstance _item)
    {
        //무기에 담긴 모든 스텟을 더해주기
        foreach (var stats in _item.setting.stats)
        {
            PlayerDBManager.instance.playerDB.SetAddStat(stats.type, stats.value);
        }
    }

    //스텟감소
    public void MinusStatItem(EquipmentItemInstance _item)
    {
        //무기에 담긴 모든 스텟을 더해주기
        foreach (var stats in _item.setting.stats)
        {
            PlayerDBManager.instance.playerDB.SetRemoveStat(stats.type, stats.value);
        }
    }

}
