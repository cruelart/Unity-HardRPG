using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class informationUi : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerEquip;

    [SerializeField]
    GameObject PlayerInventory;

    List<GameObject> UI_list;

    void Start()
    {
        UI_list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerEquip.SetActive(true);
            UI_list.Add(PlayerEquip);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            PlayerInventory.SetActive(true);
            UI_list.Add(PlayerInventory);
        }

        OffUI();
    }

    void OffUI()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(UI_list.Count > 0)
            {
                UI_list[UI_list.Count - 1].SetActive(false);
                UI_list.RemoveAt(UI_list.Count - 1);
            }
        }
    }
}
