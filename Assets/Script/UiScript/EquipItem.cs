using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipItem : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    GameObject message_prefab;

    [SerializeField]
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyUseItem()
    {
        TextMeshProUGUI text = this.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        if(text.text == "체력 포션")
        {
            if (PlayerData.playerData.player_hp + 30 >= PlayerData.playerData.player_maxHp)
            {
                PlayerData.playerData.player_hp = PlayerData.playerData.player_maxHp;
            }
            else
            {
                PlayerData.playerData.player_hp += 30;
            }
            
            for (int count = 0; count < PlayerData.playerData.consume_inventory.Count; count++)
            {
                if(PlayerData.playerData.consume_inventory[count].ItemName == "체력 포션")
                {
                    PlayerData.playerData.consume_inventory.RemoveAt(count);
                    PlayerData.playerData.isCallInventory = true;
                }
            }

            for(int count = 0; count < PlayerData.playerData.gameObject_inventory.Count; count++)
            {
                Image image = PlayerData.playerData.gameObject_inventory[count].transform.GetChild(1).GetComponent<Image>();
                if (image.sprite.name == "HpPotion")
                {
                    GameObject wantDestroy = PlayerData.playerData.gameObject_inventory[count];
                    PlayerData.playerData.gameObject_inventory.RemoveAt(count);
                    Destroy(wantDestroy);
                    PlayerData.playerData.isCallInventory = true;
                    return;
                }
            }
            return;
        }
        //소비아이템 사용

        //장비아이템 착용
        if (PlayerData.playerData.equip_weapon == null)
        {
            if (PlayerData.playerData.ready_weapon.ItemOption == "무기")
            {
                GameObject item_information = this.transform.parent.gameObject;
                Image weaponImage = weapon.GetComponent<Image>();
                Sprite copyImage = item_information.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                weaponImage.sprite = copyImage;
                PlayerData.playerData.equip_weapon = PlayerData.playerData.ready_weapon;
                PlayerData.playerData.ready_weapon = null;
                PlayerData.playerData.isChangeItem = true;
                weapon.SetActive(true);
            }
            return;
        }
        else if (PlayerData.playerData.equip_weapon != null) // 이미 아이템을 장착중이라면
        {
            GameObject errorMessage_image = Instantiate<GameObject>(message_prefab, canvas.transform);
            PlayerData.playerData.player_message.Add(errorMessage_image);
            GameObject errorMessage_text = errorMessage_image.transform.GetChild(0).gameObject;
            text = errorMessage_text.GetComponent<TextMeshProUGUI>();
            text.text = "이미 아이템을 장착중이므로 장착해제해주세요";
            RectTransform rectTransform = errorMessage_image.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
            rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = new Vector2(0, - 130);

            return;
        }
    }

}
