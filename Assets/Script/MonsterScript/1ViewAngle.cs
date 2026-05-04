using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemViewAngle : MonoBehaviour
{
    [SerializeField] private float viewDistance; // 몬스터의 시야거리
    [SerializeField] private float viewAngle; // 몬스터의 시야각
    [SerializeField] private LayerMask targetMask; // 플레이어인지 확인하는 레이어마스크

    NavMeshAgent agent;
    Rigidbody rigid;

    [SerializeField] private Transform GolemGoal1;
    [SerializeField] private Transform GolemGoal2;
    [SerializeField] private Transform GolemGoal3;
    [SerializeField] private Transform GolemGoal4;

    private float CHandMONdistance; // 플레이어와 몬스터의 거리 (CH)캐릭터 and (Mon)몬스터
    private float CharacterAndMonsterAngle; // 플레이어와 몬스터의 각도
    private float GolemAndGoal1_Distance = 0.0f;
    private float GolemAndGoal2_Distance = 0.0f;
    private float GolemAndGoal3_Distance = 0.0f;
    private float GolemAndGoal4_Distance = 0.0f;

    private bool isfindPlayer = false;


    Transform PlayerTF; // 플레이어 위치

    // Start is called before the first frame update
    void Start()
    {
        PlayerTF = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        View();
        FoundPlayer();
        //MoveGolem();
        //Debug.Log(CHandMONdistance);
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y; //몸이 회전하는 각도조정
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    private void View() // 몬스터의 시야각도표시함수
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);
    }
    private void FoundPlayer() // 몬스터가 플레이어를 찾는 함수
    {
        Debug.Log("몬스터가 플레이어를 찾는 중입니다.");
        //Debug.Log(transform.position);
        Vector3 CHandMONdirection = (PlayerTF.position - transform.position).normalized;
        //Debug.Log(CHandMONdirection);
        CharacterAndMonsterAngle = Vector3.Angle(CHandMONdirection, transform.forward);
        //Debug.Log(CHandMONangle);
        CHandMONdistance = (PlayerTF.position - transform.position).magnitude;
        //Debug.Log(CHandMONdistance);

        MoveGolem();

        if (CharacterAndMonsterAngle < viewAngle * 0.5) // 몬스터의 시야각에 들어오고
        {
            if (CHandMONdistance < viewDistance && CHandMONdistance>10) // 몬스터가 인지하는 거리내에 들어올 경우
            {
                Debug.Log("인지했습니다");
                RaycastHit Rayhit;
                if (Physics.Raycast(transform.position, CHandMONdirection, out Rayhit, viewDistance))
                {
                    if (Rayhit.transform.tag == "Wall") // 플레이어가 벽에 막혀있다면
                    {
                        Debug.Log("플레이어가 벽에 가로막혀 있습니다.");
                    }
                    else // 장애물에 막혀있지 않을 경우
                    {
                        Debug.Log("몬스터가 플레이어를 발견했습니다.");
                        agent.SetDestination(PlayerTF.position);
                        isfindPlayer = true;
                    }
                }
            }
            else if (CHandMONdistance<= 10)
            {
                //몬스터 공격 애니메이션 작동
            }
            else if(isfindPlayer == true && CHandMONdistance > viewDistance) // 몬스터가 플레이어를 발견했지만 거리가 멀어져 시야에 잡히지 않을 때
            {
                isfindPlayer = false;
                agent.SetDestination(Min_GolemAndGoal_Distance().position);
                
            }
        }
    }
    private void MoveGolem()
    {
        GolemAndGoal1_Distance = (transform.position - GolemGoal1.position).magnitude;// 골렘과 목표좌표1과의 거리
        GolemAndGoal2_Distance = (transform.position - GolemGoal2.position).magnitude;// 골렘과 목표좌표2과의 거리
        GolemAndGoal3_Distance = (transform.position - GolemGoal3.position).magnitude;// 골렘과 목표좌표3과의 거리
        GolemAndGoal4_Distance = (transform.position - GolemGoal4.position).magnitude;// 골렘과 목표좌표4과의 거리

        //Debug.Log(GolemAndGoal3_Distance);

        //Debug.Log(GolemAndGoal1_Distance);

        if (GolemAndGoal1_Distance < 1.0f)
        {
            Debug.Log("골렘이 1번 지역으로 다가가려함");
            agent.SetDestination(GolemGoal2.position);
        }
        else if (GolemAndGoal2_Distance < 1.0f)
        {
            agent.SetDestination(GolemGoal3.position);
        }
        else if (GolemAndGoal3_Distance < 1.0f)
        {
            agent.SetDestination(GolemGoal4.position);
        }
        else if (GolemAndGoal4_Distance < 1.0f)
        {
            agent.SetDestination(GolemGoal1.position);
        }
        else
        {

        }
    }
    private Transform Min_GolemAndGoal_Distance() // 몬스터가 돌아갈 지점을 정하는 함수
    {
        float[] GolemAndGoal_Distance = new float[] {GolemAndGoal1_Distance, GolemAndGoal2_Distance, GolemAndGoal3_Distance, GolemAndGoal4_Distance};
        float min = GolemAndGoal1_Distance;

        for(int i = 0; i<4; i++)
        {
            if(min>GolemAndGoal_Distance[i])
            {
                min = GolemAndGoal_Distance[i];
            }
        }

        if(min == GolemAndGoal_Distance[0])
        {
            return GolemGoal1;
        }
        else if (min == GolemAndGoal_Distance[1])
        {
            return GolemGoal2;
        }
        else if (min == GolemAndGoal_Distance[2])
        {
            return GolemGoal3;
        }
        else
        {
            return GolemGoal4;
        }
    }
}
