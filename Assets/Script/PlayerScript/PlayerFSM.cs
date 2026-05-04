using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFSM : MonoBehaviour
{

    [SerializeField] GameObject StaminaBar; // 플레이어의 스테미나
    [SerializeField] GameObject PlayerSword; // 플레이어 검
    [SerializeField] GameObject MainCam; // 카메라
    [SerializeField] GameObject AttackArea; // 공격처리 오브젝트
    Image Staminaimage;
    float StaminaFillAmount = 1.0f; // 플레이어 스테미나 게이지 상태

    public enum States { Idle, Walk, Run, Jump, ThrowAttack, SetSword, SwordAttack, ReleaseSword } // 플레이어의 FSM 상태

    float Dashtime = 0.15f; // time for Dash 
    private float doubleClickTimeL = -0.5f;
    private float doubleClickTimeR = -0.5f;
    private float doubleClickTimeU = -0.5f;
    private float doubleClickTimeD = -0.5f;
    private float SwordAttackTime;

    public float playerHp = 40;
    public float PlayerSpeed = 0.0f;
    private float PlayerRotationSpeed = 10.0f;
    public float hungryGage = 0.0f; // 
    public float totalhungryGage = 0.0f; // 총 배고픔상태
    public short hungrySpeed = 2; // 배고픔
    public float staminaGage = 0.0f; // 스태미나
    private float attackTimer = 0.0f;

    public int playerLv = 2;
    public int playerAttackValue = 4;
    public double playerExp = 1;

    private bool isjumping;
    private bool isThrowAttack;
    private bool isSword = false;
    private bool isSwordAttack = false;
    private bool haveSword = false;

    private bool isSlimeAttack = false;

    public float jumpdelay = 1.0f; // 점프 딜레이

    
    private Vector3 dir = Vector3.zero; // 플레이어의 방향 초기 설정
    private Vector3 Cam_dir = Vector3.zero; // 플레이어의 방향 초기 설정
    private Vector3 LerpBugStop = new Vector3(1, 0, 1);
    private Vector3 pastDir = new Vector3(0, 0, 0);
    private Vector3 futureDir = new Vector3(0, 0, 0);

    public bool TFclick = false; // Dash On is True, Dash off is false  // 대쉬중 다른 방향키를 눌렀을 때 대쉬가 지속되게 만드는 변수

    States playerstate;
    Animator anime;
    PlayerMove playermove;
    Rigidbody PlayerRigid;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        playermove = GetComponent<PlayerMove>();
        PlayerRigid = GetComponent<Rigidbody>();
        playerstate = States.Idle;
        Staminaimage = StaminaBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        StaminaOnOff();
        playMove();
        PlayerLvUp();
        //PlayerAttackValue();
        Debug.Log(playerLv);
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            PlayerDir();
            if (TFclick&& !isjumping && !isSword && !isThrowAttack) // 달리는 중
            {
                if (StaminaFillAmount > 0.0f)
                {
                    SwapState(States.Run);
                    PlayerRigid.MovePosition(transform.position + dir * 3 * Time.deltaTime * hungrySpeed);
                }

                else if (StaminaFillAmount <= 0.001f)
                {
                    PlayerRigid.MovePosition(transform.position + dir * 1 * Time.deltaTime * hungrySpeed);
                    StaminaFillAmount = 0.0f;
                }
                
            }
            else if(!TFclick && !isjumping&& !isThrowAttack&& !isSword) // 걷는 중
            {
                SwapState(States.Walk);
                PlayerRigid.MovePosition(transform.position + dir * 1 * Time.deltaTime * hungrySpeed);
            }
            else if(isSword)
            {
                anime.SetBool("isSwordRun", true);
                anime.SetBool("isSwordIdle", false);
                PlayerRigid.MovePosition(transform.position + dir * 1 * Time.deltaTime * hungrySpeed);
            }
        }
        //플레이어 Idle 상태
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && !isjumping && !isThrowAttack)
        {
            if (!isSword)
            {
                SwapState(States.Idle);
            }
            else
            {
                anime.SetBool("isSwordIdle", true);
                anime.SetBool("isSwordRun", false);
            }
        }
        
        //Debug.Log("현재 플레이어의 상태는 " + playerstate);
        if (Input.GetKeyDown(KeyCode.Space)&&isjumping == false&& isThrowAttack == false) // 점프
        {
            isjumping = true;
            SwapState(States.Jump);
        }

        if (Input.GetMouseButtonDown(1)&&!isjumping) // 던지기 공격
        {
            isThrowAttack = true;
            SwapState(States.ThrowAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)&&!isjumping && !isThrowAttack&& haveSword) // 칼을 꺼내는 상태
        {
            PlayerSword.SetActive(true);
            isSword = true;
            anime.SetTrigger("SetSword");
            anime.SetBool("NoSword", false);
        }

        if (Input.GetMouseButtonDown(0)&&isSword&&!isSwordAttack)// 칼로 공격하는 상태
        {
            AttackArea.SetActive(true);
            SwordAttackTime = Time.time;
            anime.SetBool("isSwordAttack", true);
            isSwordAttack = true;
        }
        if (Time.time - SwordAttackTime >1.0f)// 칼 애니메이션 조정
        {
            AttackArea.SetActive(false);
            anime.SetBool("isSwordAttack", false);
            isSwordAttack = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha0)&&isSword)// 칼을 집어넣는 상태
        {
            Debug.Log("0번을 누름");
            anime.SetBool("NoSword", true);
            isSword = false;
            PlayerSword.SetActive(false);
        }


        Staminaimage.fillAmount = StaminaFillAmount;
        StaminaBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1.5f, 3.8f, 0));
        //Debug.Log(StaminaBar.transform.position);
        //Debug.Log(transform.position);
        //Debug.Log(StaminaFillAmount);

    }

    private void SwapState(States currentState) // 플레이어의 상태를 변환시키는 함수
    {
        StopCoroutine(playerstate.ToString());
        playerstate = currentState;
        StartCoroutine(playerstate.ToString());
    }

    void playMove()
    {
        if (Input.GetKeyDown(KeyCode.A)) // Enter leff dash
        {
            if (Time.time -doubleClickTimeL < Dashtime)// 더블클릭시 실행
            {
                TFclick = true;
            }

            else
            {
                doubleClickTimeL = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // Enter right dash
        {
            if (Time.time - doubleClickTimeR < Dashtime)// 더블클릭시 실행
            {
                TFclick = true;
            }

            else
            {
                doubleClickTimeR = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // Enter right dash
        {
            if (Time.time - doubleClickTimeU < Dashtime)// 더블클릭시 실행
            {
                TFclick = true;
            }

            else
            {
                doubleClickTimeU = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // Enter right dash
        {
            if (Time.time - doubleClickTimeD < Dashtime)// 더블클릭시 실행
            {
                TFclick = true;
            }

            else
            {
                doubleClickTimeD = Time.time;
            }
        }

    }

    void PlayerDirMove(float ad, float sw)
    {


        if (ad < 0) // 왼쪽을 눌렀을때
        {
            if (sw == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
            else if(sw > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if(sw < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z + -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
            }
        }
        else if (ad > 0)
        {
            if (sw == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, +Cam_dir.z, PlayerRotationSpeed*Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x, PlayerRotationSpeed*Time.deltaTime);
            }
            else if (sw > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.z + Cam_dir.x, PlayerRotationSpeed*Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x + Cam_dir.z, PlayerRotationSpeed*Time.deltaTime);
            }
            else if (sw < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.z + -Cam_dir.x, PlayerRotationSpeed*Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed*Time.deltaTime);
            }
        }

        else if (sw < 0)
        {
            if (ad == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z + (-Cam_dir.x), PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
        }
        else if (sw > 0)
        {
            if (ad == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z + (-Cam_dir.x), PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
        }

        dir = futureDir;

    }

    void PlayerDir()
    {
        Cam_dir.x = Mathf.Sin(MainCam.transform.eulerAngles.y * Mathf.Deg2Rad);
        Cam_dir.z = Mathf.Cos(MainCam.transform.eulerAngles.y * Mathf.Deg2Rad);
        float AD = Input.GetAxis("Horizontal"); 
        float SW = Input.GetAxis("Vertical");

        PlayerDirMove(AD,SW);




        dir.Normalize();

        if (dir != Vector3.zero)
        {
            if (transform.forward.normalized == -dir) // 정반대 방향 선형보간법 버그를 위한 코드
            {
                transform.forward += LerpBugStop;
            }


            transform.forward = Vector3.Lerp(transform.forward, dir, 50 * Time.deltaTime);
        }
    }


    void StaminaOnOff()
    {
        if(StaminaFillAmount == 1.0f)
        {
            StaminaBar.SetActive(false);
        }
        else
        {
            StaminaBar.SetActive(true);
        }
    }

    void PlayerLvUp()
    {
        if(playerExp >= playerLv * 100)
        {
            playerLv += 1;
            PlayerAttackValue();
            playerExp = 0;
        }
    }
    void PlayerAttackValue()
    {
        playerAttackValue = playerLv * 2;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {
            haveSword = true;
        }

        if (col.tag == "Slime")
        {
            if (attackTimer <= 0.0f)
            {
                playerHp -= 10;
            }
        }

        if(col.tag == "BossAttack")
        {
            playerHp -= 30;
        }
    }



    IEnumerator Idle()
    {
        anime.SetBool("isIdle", true);
        anime.SetBool("isWalk", false);
        anime.SetBool("isRun", false);
        Debug.Log("정지합니다");
        TFclick = false;
        hungryGage = 0.0f;

        while (true)
        {
            if(StaminaFillAmount<=1.0f)
            {
                StaminaFillAmount += 0.002f;
            }
            if(StaminaFillAmount >1.0f)
            {
                StaminaFillAmount = 1.0f;
            }
            Debug.Log("정지중입니다");
            yield return null;
        }
    }
    IEnumerator Walk()
    {
        Debug.Log("걷습니다");
        anime.SetBool("isWalk", true);
        anime.SetBool("isIdle", false);
        while(true)
        {
            hungryGage = Time.deltaTime * 3;
            totalhungryGage += hungryGage;
            Debug.Log("걷는 중입니다");
            yield return null;
        }
    }

    IEnumerator Jump()
    {
        anime.SetBool("isJump", true);
        anime.SetBool("isRun", false);
        anime.SetBool("isWalk", false);
        anime.SetBool("isIdle", false);
        Debug.Log("점프합니다");
        yield return new WaitForSeconds(0.7f);
        anime.SetBool("isJump", false);
        Debug.Log("점프룰 멈춥니다");
        isjumping = false;

    }

    IEnumerator ThrowAttack()
    {
        Debug.Log("공격합니다.");
        anime.SetBool("isThrowAttack", true);
        anime.SetBool("isWalk", false);
        anime.SetBool("isIdle", false);

        yield return new WaitForSeconds(1.0f);

        anime.SetBool("isThrowAttack", false);
        isThrowAttack = false;
        Debug.Log("표창던지기 취소");
    }

    IEnumerator Run()
    {
        StaminaFillAmount -= 0.003f;
        Debug.Log("달립니다.");
        anime.SetBool("isRun", true);
        anime.SetBool("isIdle", false);
        anime.SetBool("isWalk", false);


        while (true)
        {
            hungryGage = Time.deltaTime * 10;
            totalhungryGage += hungryGage;
            Debug.Log("달리는 중입니다");
            yield return null;
        }
    }



    
    
}