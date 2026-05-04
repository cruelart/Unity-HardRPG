using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiClickDontMovePlayer : MonoBehaviour
{
    // 패널이 마우스 입력을 받지 않도록 설정
    public void DisableInput()
    {
        Image panelImage = this.GetComponent<Image>();
        panelImage.raycastTarget = false;
    }

    // 패널이 마우스 입력을 받도록 설정
    public void EnableInput()
    {
        Image panelImage = this.GetComponent<Image>();
        panelImage.raycastTarget = true;
    }
}
