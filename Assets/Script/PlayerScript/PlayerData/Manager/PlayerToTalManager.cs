using System.Collections.Generic;
using UnityEngine;

//플레이어 관련 모든 것을 관리하는 스크립트
public class PlayerToTalManager : MonoBehaviour
{
    private PlayerStatData playerDB;
    private PlayerAnimationEvents playerAnimationEvents;
    private PlayerAttackManager playerAttackManager;
    private PlayerReactManager playerReactManager;

    private Re_Inventory playerInventoryDB;
    private EquipSpace playerEquipSpaceDB;
    private PlayerDropItemInteraction playerDropItemInteraction;

    private void Awake()
    {
        //데이터 로드
        InitPlayer();

        //게임 로드
        LoadGame();
    }

    void Start()
    {
        UIManager.Instance.StatusUI.Init(PlayerStatManager.Instance);
        UIManager.Instance.StateUI.Init(PlayerStatManager.Instance);
        UIManager.Instance.EquipSpaceUI.Init(playerEquipSpaceDB);
        UIManager.Instance.InventoryUI.Init(playerInventoryDB);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitPlayer()
    {
        playerAnimationEvents = GetComponent<PlayerAnimationEvents>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerReactManager = GetComponent<PlayerReactManager>();
        playerDropItemInteraction = GetComponent<PlayerDropItemInteraction>();
        playerInventoryDB = GetComponent<Re_Inventory>();
        playerEquipSpaceDB = GetComponent<EquipSpace>();

        if(playerEquipSpaceDB == null)
        {
            Debug.Log("뭉뭉탱탱이이");
        }

        playerAnimationEvents.Init(playerAttackManager);
        playerAttackManager.Init(PlayerStatManager.Instance);
        playerReactManager.Init(PlayerStatManager.Instance);
        playerInventoryDB.Init();
        playerEquipSpaceDB.Init(PlayerStatManager.Instance);
        playerDropItemInteraction.Init(playerInventoryDB);

        InventoryManager.Instance.Init(playerInventoryDB);
        EquipSpaceManager.Instance.Init(playerEquipSpaceDB);

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
