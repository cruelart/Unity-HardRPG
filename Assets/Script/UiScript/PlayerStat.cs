using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowPlayerStat();
    }

    private void ShowPlayerStat()
    {
        GameObject gameObject = this.transform.GetChild(0).gameObject;
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "공격력 : " + PlayerData.playerData.Attack_value.ToString();
        gameObject = this.transform.GetChild(1).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "방어력 : " + PlayerData.playerData.Defense_value.ToString();
        gameObject = this.transform.GetChild(2).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "스피드 : " + PlayerData.playerData.Speed_value.ToString();
        gameObject = this.transform.GetChild(4).gameObject;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = PlayerData.playerData.can_useStatPoint.ToString();

    }

    public void AttackUp()
    {
        if (PlayerData.playerData.can_useStatPoint > 0)
        {
            PlayerData.playerData.attack_value += 1;
            PlayerData.playerData.can_useStatPoint -= 1;
        }
    }

    public void DeffenseUp()
    {
        if (PlayerData.playerData.can_useStatPoint > 0)
        {
            PlayerData.playerData.defense_value += 1;
            PlayerData.playerData.can_useStatPoint -= 1;
        }
    }
}
