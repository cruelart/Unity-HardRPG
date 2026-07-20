using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUIScript : MonoBehaviour
{
    List<Action> orderUIList = new List<Action>();

    bool onInventoryPanel = false;
    bool onEquipSpacePanel = false;
    bool onStatusPanel = false;

    int totalUINum = 0; // 현재 켜져잇는 모든 UI창의 갯수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            switch(onInventoryPanel)
            {
                case true:
                    onInventoryPanel = false;
                    HideMouseButton();
                    UIManager.Instance.HideInventoryUI();
                    break;

                case false:
                    onInventoryPanel = true;
                    ShowMouseButton();
                    UIManager.Instance.ShowInventoryUI();
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (onEquipSpacePanel)
            {
                case true:
                    onEquipSpacePanel = false;
                    HideMouseButton();
                    UIManager.Instance.HideEquipSpaceUI();
                    break;

                case false:
                    onEquipSpacePanel = true;
                    ShowMouseButton();
                    UIManager.Instance.ShowEquipSpaceUI();
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (onStatusPanel)
            {
                case true:
                    onStatusPanel = false;
                    HideMouseButton();
                    UIManager.Instance.HidePlayerStatusUI();
                    break;

                case false:
                    onStatusPanel = true;
                    ShowMouseButton();
                    UIManager.Instance.ShowPlayerStatusUI();
                    break;

            }
        }
    }

    private void ShowMouseButton()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideMouseButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
