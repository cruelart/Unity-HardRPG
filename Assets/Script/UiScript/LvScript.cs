using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvScript : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    public TextMeshProUGUI ttext;
    string splayerLv;
    // Start is called before the first frame update

    void Start()
    {
        ttext = ttext.GetComponent<TextMeshProUGUI>();

        if (PlayerData.playerData != null)
        {
            splayerLv = "Lv." + PlayerData.playerData.Player_level.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("splayerLv:" + splayerLv);
        //Debug.Log(player.playerLv);
        ttext.text = splayerLv;

        splayerLv = "Lv." + PlayerData.playerData.Player_level.ToString();
    }
}
