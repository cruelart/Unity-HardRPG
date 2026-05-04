using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    float fillAmount = 1.0f;
    Image staminaBar;
    PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<Image>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        fillAmount = fillAmount - playerMove.staminaGage / 1000;
        staminaBar.fillAmount = fillAmount;
        //Debug.Log(fillAmount);
        //Debug.Log(playerMove.hungryGage);
    }
}
