using UnityEngine;

public class EquipSpace : MonoBehaviour
{
    //무기
    private EquipmentItemInstance weapon;

    //방패
    private EquipmentItemInstance shield;

    //장갑
    private EquipmentItemInstance glove;

    //신발
    private EquipmentItemInstance shoes;

    //견장
    private EquipmentItemInstance armor;

    //귀걸이
    private EquipmentItemInstance earring;

    public void Setitem(EquipmentItemInstance _newItem, EquipmentItemInstance _usingItem)
    {
        //장착중인 무기가 있었다면
        if(_usingItem != null)
        {
            MinusStatItem(_usingItem);
        }

        _newItem = _usingItem;

        //무기에 담긴 모든 스텟을 더해주기
        PlusStatItem(_newItem);
    }

    public void EquipItem(EquipmentItemInstance _item)
    {
        EquipmentItemDB.EquipmentType item_type = _item.setting.type;

        switch (item_type)
        {
            //장착부위 무기
            case EquipmentItemDB.EquipmentType.Weapon:

                Setitem(_item, weapon);

                break;

            //방패
            case EquipmentItemDB.EquipmentType.Shield:

                Setitem(_item, shield);

                break;
            
            //장갑
            case EquipmentItemDB.EquipmentType.Glove:

                Setitem(_item, glove);

                break;

            //신발
            case EquipmentItemDB.EquipmentType.Boots:

                Setitem(_item, shoes);

                break;

            //귀걸이
            case EquipmentItemDB.EquipmentType.Earring:

                Setitem(_item, earring);

                break;

            //갑옷
            case EquipmentItemDB.EquipmentType.Armor:

                Setitem(_item, armor);

                break;
        }
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
