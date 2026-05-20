using System.Collections.Generic;
using UnityEngine;

//플레이어 관련 모든 것을 관리하는 스크립트
public class PlayerToTalManager : MonoBehaviour
{
    public PlayerStatManager playerStatManager { get; private set; }

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
        playerStatManager = GetComponent<PlayerStatManager>();

        PlayerDB playerDB = PlayerDBManager.instance.playerDB; // 플레이어 데이터 관리 매니저에게서 데이터 받아옴
        Debug.Log("플레이어의 맥스 체력은" + playerDB.MaxHp);
        playerStatManager.Init(playerDB); // 값 넣어줌
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
