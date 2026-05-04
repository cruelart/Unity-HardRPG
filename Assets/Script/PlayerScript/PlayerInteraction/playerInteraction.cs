using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject player_sword;

    [SerializeField]
    GameObject swordAttack_section;

    [SerializeField]
    GameObject hitParticlePrefab;

    [SerializeField]
    GameObject player_leftLeg;

    [SerializeField]
    GameObject player_rightLeg;

    [SerializeField]
    GameObject player_leftArm;

    [SerializeField]
    GameObject player_rightArm;

    StopWatch stopwatch;
    PlayerData playerData;

    private float swordDrawOffTime;
    private float hungryDamage = 0.0f;
    private float superModeTime;

    private bool isKeyDownAlpha1;
    private bool isKeyDownEsc;
    private bool ishungry;
    private bool superModeTimeCheck;

    void Awake()
    {
        playerData = new PlayerData();
    }
    void Start()
    {
        superModeTimeCheck = true;
        superModeTime = 0.0f;
        swordDrawOffTime = 0.0f;
        isKeyDownAlpha1 = false;
        isKeyDownEsc = false;

        stopwatch = new StopWatch();
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveSword();
        ViewSwordSection();
        //OnOffSuperMode();
        LevelUp();
        ApplyItemStat_forPlayer();
        MessageManage();
        SetActiveBodyAttackSection();
    }

    private void SetActiveBodyAttackSection()
    {
        if (Attack.attack != null)
        {
            switch (Attack.attack.normalAttackNum)
            {
                case 0:
                    player_leftArm.SetActive(false);
                    player_rightArm.SetActive(false);
                    player_leftLeg.SetActive(false);
                    player_rightLeg.SetActive(false);
                    return;
                case 1:
                    player_leftArm.SetActive(true);
                    return;
                case 2:
                    player_rightArm.SetActive(true);
                    player_leftArm.SetActive(false);
                    return;
                case 3:
                    player_leftLeg.SetActive(true);
                    player_rightArm.SetActive(false);
                    return;
                case 4:
                    player_rightLeg.SetActive(true);
                    return;
            }
        }
    }

    private void SetActiveSword()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && PlayerData.playerData.equip_weapon != null)
        {
            player_sword.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            isKeyDownEsc = true;
            swordDrawOffTime = Time.time;
        }

        if (isKeyDownEsc)
        {
            if (stopwatch.stop_watch(swordDrawOffTime, 0.75f))
            {
                player_sword.SetActive(false);
                isKeyDownEsc = false;
            }
        }
    }

    private void HungryAdmin()
    {
        if (Input.anyKey)
        {
            //if (playerFSM.totalhungryGage > 1000)
            if (PlayerData.playerData.Player_hungry == 0)
            {
                ishungry = true;
                hungryDamage = Time.deltaTime * 10;
            }
        }
        else
        {
            hungryDamage = 0;
        }
        if (ishungry) // 배고픈 상태일때 추가적인 정보
        {
            if (PlayerData.playerData.Speed_value >= 4.0f)
            {
                PlayerData.playerData.Detail_modifyValue(0, 0, 0, 0, 0, 0, -0.5f, 0); // 움직임 감소
            }
        }
        else // 배고픈 상태가 아닐 때 정보
        {
            if (PlayerData.playerData.Speed_value <= 6.0f)
            {
                PlayerData.playerData.Detail_modifyValue(0, 0, 0, 0, 0, 0, +0.5f, 0);
            }
        }
        PlayerData.playerData.Detail_modifyValue(0, 0, -hungryDamage, 0, 0, 0, 0, 0);
        //HpBar.fillAmount = hpFillAmount = playerFSM.playerHp / 100;
    }

    private void OnOffSuperMode()
    {
        if(PlayerData.playerData.isSuperMode && superModeTimeCheck)
        {
            superModeTime = Time.time;
            superModeTimeCheck = false; // 검사를 진행중 입니다
        }
        if (!superModeTimeCheck)
        {
            if (stopwatch.stop_watch(superModeTime,0.8f)) // 0.8초동안 무적상태
            {
                //Debug.Log("나는야 무적");
                //PlayerData.playerData.isSuperMode = false;
                superModeTimeCheck = true; // 검사를 완료했습니다.
            }
        }
    }

    private void ViewSwordSection()
    {
        if(SwordAttack.sword_attack != null && SwordAttack.sword_attack.isSwordAttack == true)
        {
            swordAttack_section.SetActive(true);
            return;
        }
        swordAttack_section.SetActive(false);

    }

    private void LevelUp()
    {
        if(PlayerData.playerData.player_maxExp < PlayerData.playerData.player_exp)
        {
            PlayerData.playerData.LevelUp();
        }
    }

    private void ApplyItemStat_forPlayer()
    {
        if(PlayerData.playerData.isChangeItem)
        {
            PlayerData.playerData.isChangeItem = false;
            PlayerData.playerData.attack_value += PlayerData.playerData.equip_weapon.Attack_value;
            PlayerData.playerData.defense_value += PlayerData.playerData.equip_weapon.Deffense_value;
            PlayerData.playerData.speed_value += PlayerData.playerData.equip_weapon.Speed_value;
        }
    }

    public void OffEquip_forPlayer()
    {
        PlayerData.playerData.isChangeItem = true;
        if (PlayerData.playerData.isChangeItem)
        {
            PlayerData.playerData.isChangeItem = false;
            PlayerData.playerData.attack_value -= PlayerData.playerData.equip_weapon.Attack_value;
            PlayerData.playerData.defense_value -= PlayerData.playerData.equip_weapon.Deffense_value;
            PlayerData.playerData.speed_value -= PlayerData.playerData.equip_weapon.Speed_value;

            PlayerData.playerData.equip_weapon = null;
        }
    }

    private void MessageManage()
    {
        //Debug.Log("가짓수:" + PlayerData.playerData.player_message.Count);
        for (int count = 0; count < PlayerData.playerData.player_message.Count; count++)
        {
            RectTransform rectTransform = PlayerData.playerData.player_message[count].GetComponent<RectTransform>();

            rectTransform.anchoredPosition += new Vector2(0, 0.3f);

            if (rectTransform.anchoredPosition.y >= -50.0f)
            {
                GameObject wantDestory = PlayerData.playerData.player_message[count];
                PlayerData.playerData.player_message.RemoveAt(count);
                Destroy(wantDestory);
                count--;
            }
        }
    }

    //아이템 획득 충돌 확인
    private void OnTriggerEnter(Collider col)
    {

    }
}
