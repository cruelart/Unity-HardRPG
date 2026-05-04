using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float Dashtime = 0.15f; // time for Dash
    private float doubleClickTimeL = -0.5f;
    private float doubleClickTimeR = -0.5f;
    private float doubleClickTimeU = -0.5f;
    private float doubleClickTimeD = -0.5f;


    public float hungryGage = 0.0f;
    public float totalhungryGage = 0.0f; 
    public short hungrySpeed = 2; // 배고픔
    public float staminaGage = 0.0f; // 스태미나

    bool UseClickLeft = false;
    bool UseClickRight = false;
    bool UseClickUp = false;
    bool UseClickDown = false;
    //bool JumpOX = true;
    bool isfloor = false;
    //bool isBackFlip = false;

    

    Rigidbody PlayerRigid;

    public bool TFclick = false; // Dash On is True, Dash off is false

    private Vector3 dir = Vector3.zero;
    private Vector3 LerpBugStop = new Vector3(1, 0, 1);

    Animator anime;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();


    }

    

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();
        //->Playermove vector option
        Vector3 vec1 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        // dash vector script
        // Input.GetButton으로 하면 왼쪽 방향키와 오른쪽 방향키를 동시에 눌러도 대쉬가 발동하게 되어버림
        if (Input.GetKeyDown(KeyCode.A))// Enter leff dash
        {
            if (Time.time - doubleClickTimeL < Dashtime)// 더블클릭시 실행
            {
                UseClickLeft = true;
                TFclick = true;
            }

            else
            {
                UseClickLeft = false;
                doubleClickTimeL = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // Enter right dash
        {
            
            if (Time.time - doubleClickTimeR < Dashtime)// 더블클릭시 실행
            {
                UseClickRight = true;
                TFclick = true;
            }

            else
            {
                UseClickRight = false;
                doubleClickTimeR = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // Enter right dash
        {
            
            if (Time.time - doubleClickTimeU < Dashtime)// 더블클릭시 실행
            {
                UseClickUp = true;
                TFclick = true;
            }

            else
            {
                UseClickUp = false;
                doubleClickTimeU = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // Enter right dash
        {
            if (Time.time - doubleClickTimeD < Dashtime)// 더블클릭시 실행
            {
                UseClickDown = true;
                TFclick = true;
            }

            else
            {
                UseClickDown = false;
                doubleClickTimeD = Time.time;
            }
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        //Move Character
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if(TFclick)
            {
                //transform.Translate(vec1 * 10 * Time.deltaTime * hungrySpeed);
                PlayerRigid.MovePosition(transform.position + vec1 * 10 * Time.deltaTime * hungrySpeed+ dir*Time.deltaTime);
                //anime.SetBool("isRun", true);
                hungryGage = Time.deltaTime*100;
                //Debug.Log(hungryGage);
                staminaGage = Time.deltaTime * 40;
                //Debug.Log("달리는중");

                totalhungryGage += hungryGage;
            }
            else if(TFclick == false)
            {
                //transform.Translate(vec1 * 3 * Time.deltaTime * hungrySpeed);
                PlayerRigid.MovePosition(transform.position + dir* Time.deltaTime + vec1 * 3 * Time.deltaTime * hungrySpeed);
               // anime.SetBool("isWalk", true);
                hungryGage = Time.deltaTime * 1;
                //Debug.Log("달리기취소");
                totalhungryGage += hungryGage;
            }
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        
        if (TFclick) // 아래 코드의 무한반복을 방지하기 위한 이중if문 작성 -> TFclick == ture && 이런식으로 하면 오류
        {
            if (UseClickLeft == false && UseClickRight == false && UseClickUp == false && UseClickDown == false) // stop dash part
            {
                Debug.Log("방향키를 모두 땠습니다.");
                TFclick = false;
                //anime.SetBool("isRun", false);
                hungryGage = 0.0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)&&isfloor)
        {
            isfloor = false;
            PlayerRigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            anime.SetBool("isBackFlip", true);
        }

        if (vec1.magnitude == 0)
        {
            anime.SetBool("isWalk", false);
        }

        if(dir != Vector3.zero) 
        {
            if (transform.forward.normalized == -dir) // 정반대 방향 선형보간법 버그를 위한 코드
            {
                transform.forward += LerpBugStop;
                //Debug.Log(transform.forward);
               // Debug.Log("실행되었습니다.");
            }
            
            
            transform.forward = Vector3.Lerp(transform.forward, dir, 50 * Time.deltaTime);
        }

        
    }
}
