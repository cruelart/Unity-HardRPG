using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    GameObject item_prefab;

    [SerializeField]
    GameObject parentObject;

    [SerializeField]
    GameObject parentObject1;

    private int weapon_count;
    private int consume_count;

    // Start is called before the first frame update
    void Start()
    {
        weapon_count = 0;
        consume_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OnInventoryUI();
    }

    private void OnInventoryUI()
    {
        if (PlayerData.playerData.weapon_inventory != null)
        {
            if (PlayerData.playerData.weapon_inventory.Count > weapon_count)
            {
                GameObject item_panel = Instantiate(item_prefab);
                Image[] itemImages = item_panel.GetComponentsInChildren<Image>();
                itemImages[2].sprite = PlayerData.playerData.weapon_inventory[weapon_count].Item_image;

                item_panel.transform.SetParent(parentObject.transform, false);

                weapon_count++;
                //Debug.Log("COUNT" + weapon_count);
            }
        }

        if(PlayerData.playerData.consume_inventory != null && PlayerData.playerData.isCallInventory)
        {
            
            for(int count = 0; count < PlayerData.playerData.consume_inventory.Count; count++)
            {
                GameObject item_panel = Instantiate(item_prefab);
                PlayerData.playerData.gameObject_inventory.Add(item_panel);
                Image[] itemImages = item_panel.GetComponentsInChildren<Image>();
                itemImages[2].sprite = PlayerData.playerData.consume_inventory[count].Item_image;

                item_panel.transform.SetParent(parentObject1.transform, false);
                PlayerData.playerData.isCallInventory = false;
                count++;
                //Debug.Log("COUNT" + count);
            }
        }
    }
}
