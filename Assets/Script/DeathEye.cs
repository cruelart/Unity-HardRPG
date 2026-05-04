using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathEye : MonoBehaviour
{
    DeathMonster deathMonster;
    Image deatheye;

    // Start is called before the first frame update
    void Start()
    {
        deatheye = GetComponent<Image>();
        deathMonster = GameObject.Find("DeathGolem").GetComponent<DeathMonster>();
        deatheye.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(deathMonster.vertorLength < 60)
        {
            if (deatheye.fillAmount < 1.0f)
            {
                deatheye.fillAmount += 0.02f;
            }
            else
            {
                deatheye.fillAmount = 1.0f;
            }

        }
        else
        {
            if (deatheye.fillAmount > 0.0f)
            {
                deatheye.fillAmount -= 0.02f;
            }
            else
            {
                deatheye.fillAmount = 0.0f;
            }
        }
    }
}
