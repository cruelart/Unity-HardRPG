using System.Collections.Generic;
using UnityEngine;

//플레이어 관련 모든 것을 관리하는 스크립트
public class PlayerToTalManager : MonoBehaviour
{
    private PlayerDB playerDB;
    private PlayerStatManager playerStatManager;
    private PlayerAnimationEvents playerAnimationEvents;
    private PlayerAttackManager playerAttackManager;
    private PlayerReactManager playerReactManager;
    private void Awake()
    {

    }

    void Start()
    {
        //데이터 로드
        LoadData();

        //게임 로드
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {
        playerDB = PlayerDBManager.instance.playerDB; // 플레이어 데이터 관리 매니저에게서 데이터 받아옴
        playerStatManager = GetComponent<PlayerStatManager>();
        playerAnimationEvents = GetComponent<PlayerAnimationEvents>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerReactManager = GetComponent<PlayerReactManager>();


        playerStatManager.Init(playerDB); // 값 넣어줌
        playerAnimationEvents.Init(playerAttackManager);
        playerAttackManager.Init(playerStatManager);
        playerReactManager.Init(playerStatManager);
    }

    public void LoadGame()
    {
        //playerDB = PlayerDBManager.Load(); // 데이터 로드
        //playerStatManager.Init(playerDB);
    }

    ////플레이어가 데미지를 입는다면
    //public void OnDamaged(int _damage)
    //{
    //    playerStatManager.OnDamaged(_damage); // 스텟관리자 함수 호출로 처리
    //    Debug.Log("플레이어가 데미지를 입었다" + _damage);

    //    //if(playerHp <= 0)
    //    //{
    //    //    Die();
    //    //}
    //}

}
