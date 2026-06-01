using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
public class BossScript : MonoBehaviour
{
    //-----------SelectorNode

    SelectorNode isVisibleRange;
    SelectorNode isMonsterMaxHp;
    SelectorNode isNearDistance; // 근거리 공격 범위
    SelectorNode isMonsterHp30up1; // 근거리 공격 범위
    SelectorNode isFarDistance; // 원거리 공격 범위
    SelectorNode MonsterAngryMode;
    SelectorNode isMonsterHP10down;

    //-----------SequenceNode

    SequenceNode rootNode;
    SequenceNode isExeNode;
    SequenceNode MonsterRunOrWalk;
    SequenceNode Seq_isRunning;
    //SequenceNode Seq_ComboAttack;
    //SequenceNode Seq_JumpAttack;

    //-----------ActionNode

    ActionNode ifRunningNormalAttack;
    ActionNode ifRunningComboAttack;
    ActionNode ifRunningJumpAttack;
    ActionNode MonsterHealHp;
    ActionNode MonsterStroll;
    //ActionNode ifRunningNormalAttack;
    ActionNode MonsterNormalAttack;
    //ActionNode ifRunningComboAttack;
    ActionNode MonsterAttackCombo;
    ActionNode isPlayerHp50down;
    ActionNode MoveTowardPlayerWalk;
    ActionNode MoveTowardPlayerRun;
    //ActionNode ifRunningJumpAttack;
    ActionNode MonsterJumpAttack;

    //---------- 행동트리 노드 설정 완료

    float visibleRange = 150.0f;
    float nearAttackRange = 15.0f;
    float farAttackRange = 100.0f;
    int bossDefenceValue = 100;
    int bossMaxHp = 10000; // 보스 최대 체력
    int bossHp;
    float timer = 0.0f;
    float healtimer = 0.0f;
    float normalAttacktimer = 0.0f;
    float comboAttacktimer = 0.0f;
    float jumpAttacktimer = 0.0f;
    float bossSpeed = 3.0f;


    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject LeftArmAttack;

    [SerializeField]
    private GameObject RightArmAttack;

    Animator anime;
    Rigidbody rigid;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // 행동트리의 노드들을 생성
        isVisibleRange = new SelectorNode();
        isMonsterMaxHp = new SelectorNode();
        isNearDistance = new SelectorNode();
        isMonsterHp30up1 = new SelectorNode();
        isFarDistance = new SelectorNode();
        MonsterAngryMode = new SelectorNode();
        isMonsterHP10down = new SelectorNode();

        rootNode = new SequenceNode();
        isExeNode = new SequenceNode();
        MonsterRunOrWalk = new SequenceNode(); // 플레이어 레벨 10이상, 플레이어 hp 20% 미만이면 방어력증가효과
        Seq_isRunning = new SequenceNode();
        //Seq_ComboAttack = new SequenceNode();
        //Seq_JumpAttack = new SequenceNode();

        //ifRunningNormalAttack = new ActionNode();
        //ifRunningComboAttack = new ActionNode();
        //ifRunningJumpAttack = new ActionNode();
        //MonsterHealHp = new ActionNode();
        //MonsterStroll = new ActionNode();
        ////ifRunningNormalAttack = new ActionNode();
        //MonsterNormalAttack = new ActionNode();
        ////ifRunningComboAttack = new ActionNode();
        //MonsterAttackCombo = new ActionNode();
        //isPlayerHp50down = new ActionNode();
        //MoveTowardPlayerRun = new ActionNode();
        //MoveTowardPlayerWalk = new ActionNode();
        ////ifRunningJumpAttack = new ActionNode();
        //MonsterJumpAttack = new ActionNode();

        // 행동트리연결

        //rootNode.ListAdd(isVisibleRange);
        rootNode.ListAdd(isExeNode);

        isExeNode.ListAdd(Seq_isRunning);
        isExeNode.ListAdd(isVisibleRange);

        //isVisibleRange.ListAdd(Seq_isRunning);
        isVisibleRange.ListAdd(isMonsterMaxHp);
        isVisibleRange.ListAdd(isNearDistance);

        Seq_isRunning.ListAdd(ifRunningNormalAttack);
        Seq_isRunning.ListAdd(ifRunningComboAttack);
        Seq_isRunning.ListAdd(ifRunningJumpAttack);

        isMonsterMaxHp.ListAdd(MonsterHealHp);
        isMonsterMaxHp.ListAdd(MonsterStroll);

        isNearDistance.ListAdd(isMonsterHp30up1);
        isNearDistance.ListAdd(isFarDistance);

        isMonsterHp30up1.ListAdd(MonsterNormalAttack);
        //isMonsterHp30up1.ListAdd(Seq_NormalAttack);
        isMonsterHp30up1.ListAdd(MonsterAttackCombo);
        //isMonsterHp30up1.ListAdd(Seq_ComboAttack);

        //Seq_NormalAttack.ListAdd(ifRunningNormalAttack); // 조건을 따지는 액션노드
        //Seq_NormalAttack.ListAdd(MonsterNormalAttack); // 조건을 따지는 액션노드

        //Seq_ComboAttack.ListAdd(ifRunningComboAttack);
        //Seq_ComboAttack.ListAdd(MonsterAttackCombo);

        isFarDistance.ListAdd(MonsterAngryMode);
        isFarDistance.ListAdd(MonsterJumpAttack);
        //isFarDistance.ListAdd(Seq_JumpAttack);

        //Seq_JumpAttack.ListAdd(ifRunningJumpAttack);
        //Seq_JumpAttack.ListAdd(MonsterJumpAttack);

        MonsterAngryMode.ListAdd(MonsterRunOrWalk);
        MonsterAngryMode.ListAdd(MoveTowardPlayerWalk);

        MonsterRunOrWalk.ListAdd(isPlayerHp50down);
        MonsterRunOrWalk.ListAdd(MoveTowardPlayerRun);

        //----------------------------------------------------------------행동트리 노드연결 설정

        //MonsterHealHp.action += aMonsterHeal;
        //MonsterStroll.action += aMonsterStroll;
        //ifRunningNormalAttack.action += aIfRunningNormalAttack;
        //MonsterNormalAttack.action += aMonsterNormalAtttack;
        //ifRunningComboAttack.action += aIfRunningComboAttack;
        //MonsterAttackCombo.action += aMonsterAttackCombo;
        //isPlayerHp50down.action += aisPlayerHp50down;
        //MoveTowardPlayerRun.action += aMoveTowardPlayerRun;
        //MoveTowardPlayerWalk.action += aMoveTowardPlayerWalk;
        //ifRunningJumpAttack.action += aIfRunningJumpAttack;
        //MonsterJumpAttack.action += aMonsterJumpAttack;

        //----------------------------------------------------------------액션에 들어갈 delegate설정
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //----------------------------------------------------------------컴포넌트 받아옴

        //bossHp = bossMaxHp;
        bossHp = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHp > 0)
        {
            rootNode.Evaluate();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    void MonsterViewDir()
    {
        //this.transform.forward = (Player.transform.position - this.transform.position).normalized;
        Vector3 dir = new Vector3(Player.transform.position.x - this.transform.position.x, 0.0f, Player.transform.position.z - this.transform.position.z).normalized;

        this.transform.forward = Vector3.Lerp(transform.forward, dir, 10 * Time.deltaTime);
    }
    void MonsterJumpViewDir()
    {
        this.transform.forward = new Vector3(Player.transform.position.x - this.transform.position.x, 0.0f, Player.transform.position.z - this.transform.position.z).normalized;
    }
    
    bool s_isvisibleRange() // 인지범위내에 있지 않나? (범위내 -> false, 범위밖 ->true)
    {
        return !(Vector3.Distance(Player.transform.position, this.transform.position) <= visibleRange) ? true : false; // 삼항 연산자 사용
    }

    bool s_isMonsterHpMax()
    {
        return (bossHp == bossMaxHp) ? true : false;
    }
    bool s_isNearDistance()
    {
        return (Vector3.Distance(Player.transform.position, this.transform.position) <= nearAttackRange) ? true : false;
    }
    bool s_MonsterHp30up1()
    {
        return (bossHp >= (int)bossMaxHp * 0.3) ? true : false;
    }
    bool s_isFarDistance()
    {
        return (Vector3.Distance(Player.transform.position, this.transform.position) <= farAttackRange) ? true : false;
    }
    tasks aMonsterHeal()
    {
        if(s_isvisibleRange())
        {
            if(s_isMonsterHpMax())
            {
                this.bossHp += (int)Time.deltaTime;
                if (healtimer == 0.0f)
                {
                    anime.SetTrigger("isHeal"); // 회복애니메이션 재생
                }
                healtimer += Time.deltaTime;
                if(healtimer >=5.0f)
                {
                    healtimer = 0.0f;
                }
                return tasks.Success;
            }
            healtimer = 0.1f;
            return tasks.Failure;
        }
        healtimer = 0.1f;
        return tasks.Failure;
    }

    tasks aMonsterStroll()
    {
        if(s_isvisibleRange())
        {
            if(!s_isMonsterHpMax())
            {
                //Debug.Log("aMonsterStroll");
                anime.SetTrigger("isHeal");
                return tasks.Success;
            }
            return tasks.Failure;
        }
        return tasks.Failure;
    }
    tasks aIfRunningNormalAttack()
    {
        if(normalAttacktimer == 0.0f)
        {
            return tasks.Success; // 정상적으로 조건을 만족하였으니 다음 노드로 향하라
        }

        return tasks.Running;
    }
    tasks aMonsterNormalAtttack()
    {
        if(s_isNearDistance())
        {
            if(s_MonsterHp30up1())
            {
                LeftArmAttack.SetActive(true);
                RightArmAttack.SetActive(true);
                anime.SetTrigger("isNormalAttack");
                StartCoroutine(NormalAttack());
                //if (normalAttacktimer == 0.0f)
                //{
                //    anime.SetTrigger("isNormalAttack");
                //}
                //normalAttacktimer += Time.deltaTime;
                //if (normalAttacktimer >= 3.0f)
                //{
                //    Debug.Log(normalAttacktimer);
                //    normalAttacktimer = 0.0f;
                //}
                //팔에 두른 collider포함 오브젝트 SetActive
                //anime.SetTrigger("isNormalAttack"); // 일반공격 애니메이션 실행
                return tasks.Success;
            }
            //normalAttacktimer = 0.01f;
            LeftArmAttack.SetActive(false);
            RightArmAttack.SetActive(false);
            return tasks.Failure;
        }
        //normalAttacktimer = 0.01f;
        LeftArmAttack.SetActive(false);
        RightArmAttack.SetActive(false);
        return tasks.Failure;
    }
    tasks aIfRunningComboAttack()
    {
        if (comboAttacktimer == 0.0f)
        {
            return tasks.Success; // 정상적으로 조건을 만족하였으니 다음 노드로 향하라
        }

        return tasks.Running;
    }

    tasks aMonsterAttackCombo()
    {
        if(s_isNearDistance())
        {
            if (!s_MonsterHp30up1())
            {
                LeftArmAttack.SetActive(true);
                RightArmAttack.SetActive(true);
                anime.SetTrigger("isComboAttack");
                StartCoroutine(ComboAttack());
                //Debug.Log("작동은 되냐");
                //Debug.Log(comboAttacktimer);
                //if (comboAttacktimer == 0.0f)
                //{
                //    anime.SetTrigger("isComboAttack");
                //}
                //comboAttacktimer += Time.deltaTime;
                //if (comboAttacktimer >= 4.0f)
                //{
                //    //Debug.Log(normalAttacktimer);
                //    comboAttacktimer = 0.0f;
                //}
                //팔에 두른 collider포함 오브젝트 SetActive
                //anime.SetTrigger("isNormalAttack"); // 일반공격 애니메이션 실행
                return tasks.Success;
            }
            //comboAttacktimer = 3.01f;
            LeftArmAttack.SetActive(false);
            RightArmAttack.SetActive(false);
            return tasks.Failure;
        }
        //comboAttacktimer = 3.01f;
        LeftArmAttack.SetActive(false);
        RightArmAttack.SetActive(false);
        return tasks.Failure;
    }

    tasks aisPlayerHp50down()
    {
        PlayerFSM player = Player.GetComponent<PlayerFSM>();
        
        if (s_isFarDistance())
        {
            if (player.playerHp <= 50)
            {
                return tasks.Success;
            }
            return tasks.Failure;
        }
        return tasks.Failure;
    }

    tasks aMoveTowardPlayerRun()
    {
        //PlayerFSM player = Player.GetComponent<PlayerFSM>();

        if (s_isFarDistance())
        {
            MonsterViewDir();
            bossSpeed = 60.0f;
            rigid.MovePosition(transform.position + this.transform.forward * Time.deltaTime * bossSpeed);
            Debug.Log("aMoveTowardPlayerRun");
            anime.SetBool("isMonsterRun", true);
            anime.SetBool("isMonsterWalk", false);
            return tasks.Success;
        }
        return tasks.Failure;
    }

    tasks aMoveTowardPlayerWalk()
    {
        if (s_isFarDistance())
        {
            MonsterViewDir();
            Debug.Log("aMoveTowardPlayerWalk");
            bossSpeed = 30.0f;
            rigid.MovePosition(transform.position + this.transform.forward * Time.deltaTime * bossSpeed);
            anime.SetBool("isMonsterRun", false);
            anime.SetBool("isMonsterWalk", true);
            return tasks.Success;
        }
        return tasks.Failure;
    }

    tasks aIfRunningJumpAttack()
    {
        if (jumpAttacktimer == 0.0f)
        {
            return tasks.Success; // 정상적으로 조건을 만족하였으니 다음 노드로 향하라
        }

        return tasks.Running;
    }

    tasks aMonsterJumpAttack()
    {
        if(!s_isFarDistance())
        {
            LeftArmAttack.SetActive(true); 
            RightArmAttack.SetActive(true);
            anime.SetTrigger("isMonsterJumpAttack");
            MonsterJumpViewDir();
            Transform pastPlayerTransform = Player.transform;

            StartCoroutine(JumpAttack(pastPlayerTransform));
            //bossSpeed = 300.0f;
            //rigid.MovePosition(transform.position + this.transform.forward * Time.deltaTime * bossSpeed);
            //this.transform.position = Vector3.Lerp(this.transform.position, Player.transform.position, Time.deltaTime * 4);
            //Debug.Log("JumpAttack");
            //if (jumpAttacktimer == 0.0f)
            //{
            //    anime.SetTrigger("isMonsterJumpAttack");
            //}
            //jumpAttacktimer += Time.deltaTime;
            //if(jumpAttacktimer >= 5.0f)
            //{
            //    jumpAttacktimer = 0.0f;
            //}
            return tasks.Success;
        }
        //jumpAttacktimer = 2.0f;
        LeftArmAttack.SetActive(false);
        RightArmAttack.SetActive(false);
        return tasks.Failure;
    }

    IEnumerator NormalAttack()
    {
        while (normalAttacktimer < 2.8f)
        {
            normalAttacktimer += Time.deltaTime;
            yield return null;
        }
        normalAttacktimer = 0.0f;
    }

    IEnumerator ComboAttack()
    {
        while (comboAttacktimer < 4.0f)
        {
            //Debug.Log("콤보어택을 하지 못하게 막습니다");
            comboAttacktimer += Time.deltaTime;
            yield return null;
        }
        comboAttacktimer = 0.0f;
    }

    IEnumerator JumpAttack(Transform _playerTransform)
    {
        float plMoDistance = Vector3.Distance(_playerTransform.position, this.transform.position);
        //Transform pastPos = this.transform;
        //float nanotime = 1.1f / Time.deltaTime;
        //float movespeed = Vector3.Distance(_playerTransform.position, this.transform.position) / nanotime;
        while (jumpAttacktimer < 3.8f)
        {

            //Debug.Log("점프를 하지 못하게 막습니다");
            //Debug.Log(Vector3.Distance(_playerTransform.position, this.transform.position));
            //this.transform.position = Vector3.Lerp(this.transform.position, _playerTransform.position, Time.deltaTime * 4);
            if (jumpAttacktimer > 0.7f && jumpAttacktimer < 1.6f)
            {
                float nanotime = 0.9f / Time.deltaTime;
                float movespeed = plMoDistance / nanotime;

                //Debug.Log(movespeed);
                if (Vector3.Distance(_playerTransform.position, this.transform.position) > 0.1f)
                {
                    rigid.MovePosition(transform.position + 1.0f * this.transform.forward * movespeed);

                }
            }
            jumpAttacktimer += Time.deltaTime;
            yield return null;
        }
        jumpAttacktimer = 0.0f;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "NormalAttackArea")
        {
            bossHp -= 10000;
            Debug.Log("데미지 10000이 달았습니다");
        }
    }
}
