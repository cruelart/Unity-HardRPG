using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Image HpBar;

    // Start is called before the first frame update
    void Start()
    {
        HpBar = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = PlayerData.playerData.Player_hp / PlayerData.playerData.Player_maxHp;
    }
}
