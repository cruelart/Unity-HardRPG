using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : Item
{
    public WeaponItem()
    {

    }
}

public class NormalSword : WeaponItem
{
    public NormalSword()
    {
        itemName = "초보자 검";
        itemExplain = "흔하게 볼 수 있는 초보자의 검이다."; 
        itemOption = "무기"; //
        attack_value = 5;
        deffense_value = 0;
        speed_value = 0;
        item_image = Resources.Load<Sprite>("Chobo_sword");
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
public class EpicSword : WeaponItem
{
    public EpicSword()
    {
        itemName = "톱날 검";
        itemExplain = "톱날로된 강력한 검이다..";
        itemOption = "무기"; //
        attack_value = 7;
        deffense_value = 3;
        speed_value = 0;
        item_image = Resources.Load<Sprite>("Epic_sword");
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

public class Katana : WeaponItem
{
    public Katana()
    {
        itemName = "카타나";
        itemExplain = "약간 낡았지만 날이 서있는 카타나이다.";
        itemOption = "무기"; //
        attack_value = 12;
        deffense_value = 5;
        speed_value = 0;
        item_image = Resources.Load<Sprite>("Katana");
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
