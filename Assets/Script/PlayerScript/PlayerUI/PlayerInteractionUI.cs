using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInteractionUI : MonoBehaviour
{
    bool lockCamera = false;
    IInteractable target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.TalkUI.OnHide += HandleTalkHide;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            GameEventChannel.OnLockCamera?.Invoke(!lockCamera);
            lockCamera = !lockCamera;
        }

        //----------------------------------------------------UI On - Off
        if(Input.GetKeyDown(KeyCode.I))
        {
            switch(UIManager.Instance.IsOpenUI<InventoryUIManager>())
            {
                case true:
                    HideMouseButton();
                    UIManager.Instance.HideInventoryUI();
                    break;

                case false:
                    ShowMouseButton();
                    UIManager.Instance.ShowInventoryUI();
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (UIManager.Instance.IsOpenUI<EquipSpaceUIManager>())
            {
                case true:
                    HideMouseButton();
                    UIManager.Instance.HideEquipSpaceUI();
                    break;

                case false:
                    ShowMouseButton();
                    UIManager.Instance.ShowEquipSpaceUI();
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (UIManager.Instance.IsOpenUI<PlayerStatusUIManager>())
            {
                case true:
                    HideMouseButton();
                    UIManager.Instance.HidePlayerStatusUI();
                    break;

                case false:
                    ShowMouseButton();
                    UIManager.Instance.ShowPlayerStatusUI();
                    break;

            }
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            target?.Interact(transform.parent.gameObject);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.Instance.ShowQuestUI();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.InOrderUIHide();
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

    private void OnTriggerEnter(Collider other)
    {
        target = other.GetComponent<IInteractable>();
    }

    private void HandleTalkHide()
    {
        target.ExitInteraction(transform.parent.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
