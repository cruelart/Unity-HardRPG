using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected string itemName; // 아이템 이름
    protected string itemExplain; // 아이템 설명
    protected string itemOption; // 아이템의 속성(장비,소비,기타)
    protected int  attack_value; // 아이템의 공격력
    protected int  deffense_value; // 아이템의 방어력
    protected int  speed_value; // 아이템의 스피드

    protected Sprite item_image; // 아이템의 이미지
    public Item()
    {

    }

    public virtual string ItemName
    {
        get => itemName;
        private set => itemName = value;
    }

    public virtual string ItemExplain
    {
        get => itemExplain;
        private set => itemExplain = value;
    }

    public virtual string ItemOption
    {
        get => itemOption;
        private set => itemOption = value;
    }

    public virtual int Attack_value
    {
        get => attack_value;
        private set => attack_value = value;
    }

    public virtual int Deffense_value
    {
        get => deffense_value;
        private set => deffense_value = value;
    }

    public virtual int Speed_value
    {
        get => speed_value;
        private set => speed_value = value;
    }

    public virtual Sprite Item_image
    {
        get => item_image;
        private set => item_image = value;
    }
}
