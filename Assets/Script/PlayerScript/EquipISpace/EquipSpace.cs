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
    private EquipmentItemInstance shoulder;

    //귀걸이
    private EquipmentItemInstance earring;

    public void Setweapon(EquipmentItemInstance _weapon)
    {
        weapon = _weapon;
        //PlayerDBManager.instance.playerDB
    }

}
