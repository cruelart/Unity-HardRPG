using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentItemDB", menuName = "Scriptable Objects/Item/Equipment")]
public class EquipmentItemDB : BaseitemDB
{
    public enum EquipmentType { Weapon, Armor, Glove, Boots, Shield, Earring } // 장비부위 종류

    [Header("Equipment Specific")]
    public EquipmentType type; // 장비 부위
}
