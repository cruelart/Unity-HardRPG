using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : Item
{
    public ConsumeItem()
    {

    }
}

public class HpPotion : ConsumeItem
{
    public HpPotion()
    {
        itemName = "체력 포션";
        itemExplain = "체력을 무려 30만큼 회복시켜준다.";
        itemOption = "소비"; //
        attack_value = 0;
        deffense_value = 0;
        speed_value = 0;
        item_image = Resources.Load<Sprite>("HpPotion");
    }

    public override string ItemName
    {
        get => itemName;
    }

    public override string ItemExplain
    {
        get => itemExplain;
    }

    public override string ItemOption
    {
        get => itemOption;
    }

    public override int Attack_value
    {
        get => attack_value;
    }

    public override int Deffense_value
    {
        get => deffense_value;
    }

    public override int Speed_value
    {
        get => speed_value;
    }
    public override Sprite Item_image
    {
        get => item_image;
    }
}
