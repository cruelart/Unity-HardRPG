using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemClick : MonoBehaviour
{
    private GameObject item_information;

    Image image;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        sprite = image.sprite;

        Transform parent_transform = this.transform.parent;

        parent_transform = parent_transform.parent;
        parent_transform = parent_transform.parent;
        parent_transform = parent_transform.parent;
        //최상위 오브젝트인 inventory에 도착

        GameObject inventory = parent_transform.gameObject;
        item_information = inventory.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //item_information.SetActive(true);
    }

    public void Item_onClick()
    {
        item_information.SetActive(true); // 아이템 정보창을 활성화 시키고
        //Debug.Log("item_information" + item_information);
    }

    public void LoadWeaponInformation()
    {
        if(sprite.name == "Chobo_sword")
        {
            NormalSword chobo_sword = new NormalSword();

            setWeaponInformation(chobo_sword);
        }

        if (sprite.name == "Epic_sword")
        {
            EpicSword epic_sword = new EpicSword();

            setWeaponInformation(epic_sword);
        }

        if (sprite.name == "Katana")
        {
            Katana katana = new Katana();

            setWeaponInformation(katana);
        }
    }

    public void LoadConsuumeInformation()
    {
        if (sprite.name == "HpPotion")
        {
            HpPotion hpPotion = new HpPotion();
            setConsumeInformation(hpPotion);
        }
    }

    public void setWeaponInformation(WeaponItem _weaponItem)
    {
        GameObject gameObject = item_information.transform.GetChild(0).gameObject;
        Image item_image = gameObject.GetComponent<Image>();
        item_image.sprite = _weaponItem.Item_image;

        gameObject = item_information.transform.GetChild(1).gameObject;
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = _weaponItem.ItemName;

        gameObject = item_information.transform.GetChild(2).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = _weaponItem.ItemExplain;

        gameObject = item_information.transform.GetChild(3).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "공격력 : "+ _weaponItem.Attack_value.ToString();

        gameObject = item_information.transform.GetChild(4).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "방어력 : " + _weaponItem.Deffense_value.ToString();

        gameObject = item_information.transform.GetChild(5).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "스피드 : " + _weaponItem.Speed_value.ToString();

        PlayerData.playerData.ready_weapon = _weaponItem;
    }

    public void setConsumeInformation(ConsumeItem _consumeItem)
    {
        GameObject gameObject = item_information.transform.GetChild(0).gameObject;
        Image item_image = gameObject.GetComponent<Image>();
        item_image.sprite = _consumeItem.Item_image;

        gameObject = item_information.transform.GetChild(1).gameObject;
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = _consumeItem.ItemName;

        gameObject = item_information.transform.GetChild(2).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = _consumeItem.ItemExplain;

        gameObject = item_information.transform.GetChild(3).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "공격력 : " + _consumeItem.Attack_value.ToString();

        gameObject = item_information.transform.GetChild(4).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "방어력 : " + _consumeItem.Deffense_value.ToString();

        gameObject = item_information.transform.GetChild(5).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "스피드 : " + _consumeItem.Speed_value.ToString();

        gameObject = item_information.transform.GetChild(7).GetChild(0).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "사용";
    }
}
