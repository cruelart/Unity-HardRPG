using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    bool onInventoryPanel = false;

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
