using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    float fillAmount = 1.0f;
    Image HungryBar;

    // Start is called before the first frame update
    void Start()
    {
        HungryBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fillAmount = PlayerData.playerData.player_hungry / PlayerData.playerData.player_maxHungry;
        HungryBar.fillAmount = fillAmount;
        if(PlayerData.playerData.player_hungry <= 0)
        {
            PlayerData.playerData.player_hp -= 0.005f;
        }
        //Debug.Log(fillAmount);
        //Debug.Log(playerMove.hungryGage);
    }
}
