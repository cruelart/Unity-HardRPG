using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

using UnityEngine.UI;
using TMPro;

public class MonsterSlimeScript : MonoBehaviour
{
    SelectorNode rootNode;
    SelectorNode isFindPlayer;
    SelectorNode inVisibleRangePlayer;

    ActionNode attackPlayer;
    ActionNode moveTowardPlayer;
    ActionNode randomStroll;
    ActionNode exeReactMotion;
    ActionNode deathState;

    [SerializeField]
    GameObject AttackSection;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject normalSword;

    [SerializeField]
    GameObject epicSword;

    [SerializeField]
    GameObject legendSword;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject message_prefab;

    [SerializeField]
    private GameObject newuiCanvas;

    //[SerializeField]
    //GameObject SlimeHpbar


    float attackTime = 0.0f;
    float reactTime = 0.0f;

    private bool isSlimeAttack = false;
    private bool isNormalReact = false;
    private bool growSlime = true;
    private Vector3 slimeScale;

    Animator anime;
    Rigidbody rigid;
    //PlayerFSM player;
    StopWatch stopWatch;
    public SlimeData slimeData;

    // Start is called before the first frame update

    private void Awake()
    {
        slimeData = new SlimeData();
    }
    void Start()
    {
        slimeScale = this.transform.localScale;
        //행동트리 구성 및 실행------------------------
        rootNode = new SelectorNode();
        isFindPlayer = new SelectorNode();
        inVisibleRangePlayer = new SelectorNode();

        //attackPlayer = new ActionNode();
        //moveTowardPlayer = new ActionNode();
        //randomStroll = new ActionNode();
        //exeReactMotion = new ActionNode();
        //deathState = new ActionNode();

        rootNode.ListAdd(isFindPlayer);
        isFindPlayer.ListAdd(inVisibleRangePlayer);
        isFindPlayer.ListAdd(randomStroll);
        inVisibleRangePlayer.ListAdd(deathState);
        inVisibleRangePlayer.ListAdd(exeReactMotion);
        inVisibleRangePlayer.ListAdd(attackPlayer);
        inVisibleRangePlayer.ListAdd(moveTowardPlayer);

        //---------------------------------------------

        //attackPlayer.action += AttackPlayer;
        //moveTowardPlayer.action += MoveTowardPlayer;
        //randomStroll.action += RandomStroll;
        //exeReactMotion.action += MonsterReact;
        //deathState.action += MonsterDeath;

        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        //player = Player.GetComponent<PlayerFSM>();

        stopWatch = new StopWatch();

        //---------------------------------------------

    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Evaluate();
        ViewAngle.View(slimeData.viewAngle, this.transform);

    }
    
    //void SlimeHpUI()
    //{
    //    SlimeHpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.8f, 0));
    //}

    void MonsterViewDir(float _rotationSpeed) // 몬스터가 플레이어를 따라가는 함수
    {
        //this.transform.forward = (Player.transform.position - this.transform.position).normalized;
        Vector3 dir = new Vector3(Player.transform.position.x - this.transform.position.x, 0.0f, Player.transform.position.z - this.transform.position.z).normalized;
        Quaternion targetPlayer = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetPlayer, _rotationSpeed * Time.deltaTime);
    }
    tasks MoveTowardPlayer()
    {
        //Debug.Log("이동중");
        if (ViewAngle.isFindPlayer(slimeData.visibleRange, slimeData.viewAngle, Player.transform, this.transform)) // 플레이어가 몬스터의 시야각에 들어온다면
        {
            if ((Player.transform.position - this.transform.position).magnitude > slimeData.attackRange)
            {
                MonsterViewDir(2.0f);
                Debug.Log("슬라임이 플레이어쪽으로 이동중");
                anime.SetBool("isAttack", false);
                slimeData.visibleRange = 60.0f; // 인지범위를 넓혀서 발견 즉시 플레이어가 도망갈 때 발견 취소되는 현상 방지
                this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position - 5 * (this.transform.position).normalized, Time.deltaTime * 5.0f);
                //moveTowardPlayer.action
                //moveTowardPlayer.GetEvaluate = states.Success;
                return tasks.Success;
            }
        }
        //moveTowardPlayer.GetEvaluate = states.Failure;

        return tasks.Failure;
    }
    void Action_MoveTowardPlayer()
    {
        Debug.Log("슬라임이 플레이어쪽으로 이동중");
        anime.SetBool("isAttack", false);
        attackTime = Time.time;
        slimeData.visibleRange = 60.0f; // 인지범위를 넓혀서 발견 즉시 플레이어가 도망갈 때 발견 취소되는 현상 방지
        this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position - 5 * (this.transform.position).normalized, Time.deltaTime * 5.0f);
    }
    tasks AttackPlayer()
    {
        if (!ViewAngle.isFindPlayer(slimeData.attackRange, 50.0f, Player.transform, this.transform) && !isSlimeAttack)
        {
            if ((Player.transform.position - this.transform.position).magnitude < slimeData.attackRange)
            {
                MonsterViewDir(3.0f);
                return tasks.Failure;
            }
        }
        if (ViewAngle.isFindPlayer(slimeData.attackRange, 50.0f, Player.transform, this.transform))
        {
            //Debug.Log("공격중");
            if ((Player.transform.position - this.transform.position).magnitude < slimeData.attackRange)
            {
                isSlimeAttack = true;
                //MonsterViewDir();
                Debug.Log("슬라임이 플레이어를 공격");
                anime.SetBool("isAttack", true);

                //SlimeAttackSection.SetActive(true); // 공격전용 영역 오브젝트 On
                //attackPlayer.GetEvaluate = states.Success;

                return tasks.Success;
            }
        }

        if(isSlimeAttack)
        {
            return tasks.Success;
        }
          
        //attackPlayer.GetEvaluate = states.Failure;

        anime.SetTrigger("Exit");
        return tasks.Failure;
    }
    tasks RandomStroll()
    {
        if(!ViewAngle.isFindPlayer(slimeData.visibleRange, slimeData.viewAngle, Player.transform, this.transform))
        {
            //MonsterViewDir();
            //Debug.Log("슬라임이 플레이어를 찾지 못해 배회중");
            anime.SetBool("isAttack", false);
            slimeData.visibleRange = 90.0f;
            //randomStroll.GetEvaluate = states.Success;
            return tasks.Success;
        }
        //randomStroll.GetEvaluate = states.Failure;
        return tasks.Failure;
    }

    tasks MonsterReact()
    {
        if(isNormalReact)
        {
            anime.SetTrigger("beShot");
            slimeData.monster_hp -= (PlayerData.playerData.Attack_value - slimeData.defense_value); // 슬라임의 데미지가 플레이어 공격력에 따라 달라진다.
            this.rigid.AddForce(100 * (Player.transform.position - this.transform.position).normalized);
            isNormalReact = false;

            Debug.Log("React호출");
            return tasks.Success;
        }
        return tasks.Failure;
    }

    tasks MonsterDeath()
    {
        if(slimeData.monster_hp <= 0)
        {
            PlayerData.playerData.player_exp += 5;
           this.transform.localScale = this.transform.localScale - new Vector3(3, 3, 3);
            if(this.transform.localScale.x <= 0)
            {
                PlayerData.playerData.isCallInventory = true;
                if (PlayerData.playerData.weapon_inventory.Count < 9)
                {
                    itemRandomGet();
                }
                else
                {
                    ErrorMessage();
                }
            }
            return tasks.Success;
        }
        return tasks.Failure;
    }

    private void itemRandomGet()
    {
        int random_num = Random.Range(1, 100);
        Debug.Log(random_num);

        if(random_num < 5)
        {
            EpicSword normalSword = new EpicSword();
            PlayerData.playerData.weapon_inventory.Add(normalSword);
            ShowMessage(normalSword);
        }
        else if(random_num >=5 && random_num < 10)
        {
            NormalSword normalSword = new NormalSword();
            PlayerData.playerData.weapon_inventory.Add(normalSword);
            ShowMessage(normalSword);
        }
        else if(random_num >= 10 && random_num < 13)
        {
            Katana katana = new Katana();
            PlayerData.playerData.weapon_inventory.Add(katana);
            ShowMessage(katana);

        }
        else if(random_num >= 13 && random_num < 20)
        {
            HpPotion hpPotion = new HpPotion();
            PlayerData.playerData.consume_inventory.Add(hpPotion);
            ShowMessage(hpPotion);
        }

        this.gameObject.SetActive(false);
    }

    private void ShowMessage(Item _weaponItem)
    {
        GameObject errorMessage_image = Instantiate<GameObject>(message_prefab, canvas.transform);
        PlayerData.playerData.player_message.Add(errorMessage_image);
        GameObject errorMessage_text = errorMessage_image.transform.GetChild(0).gameObject;
        TextMeshProUGUI text = errorMessage_text.GetComponent<TextMeshProUGUI>();
        text.text = "아이템 " + _weaponItem.ItemName + "을 획득하셨습니다";
        RectTransform rectTransform = errorMessage_image.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = new Vector2(0, -130);

        this.gameObject.SetActive(false);
    }

    private void ErrorMessage()
    {
        GameObject errorMessage_image = Instantiate<GameObject>(message_prefab, canvas.transform);
        PlayerData.playerData.player_message.Add(errorMessage_image);
        GameObject errorMessage_text = errorMessage_image.transform.GetChild(0).gameObject;
        TextMeshProUGUI text = errorMessage_text.GetComponent<TextMeshProUGUI>();
        text.text = "장비아이템이 꽉 찼습니다.";
        RectTransform rectTransform = errorMessage_image.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
        rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = new Vector2(0, -130);

        this.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("NormalAttackArea") && !isNormalReact) // 슬라임과 무기류가 충돌한다면
        {
            isNormalReact = true;
            Debug.Log("슬라임공격에 성공하였습니다"); 
            //anime.SetTrigger("beShot");
            //Debug.Log(player.playerAttackValue);
        }
    }

    private void OnTriggerExit(Collider col)
    {
    }

    public void OnAttack()
    {
        AttackSection.SetActive(true);
    }
    public void OffAttack()
    {
        AttackSection.SetActive(false);
    }
    public void ESCAttack()
    {
        isSlimeAttack = false;
    }

    public void OffReact()
    {
        isNormalReact = false;
    }
}
