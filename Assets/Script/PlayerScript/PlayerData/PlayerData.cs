using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //싱글톤 사용
    public static PlayerData playerData = null;

    //인벤토리
    public List<WeaponItem> weapon_inventory = null;
    public List<ConsumeItem> consume_inventory = null;
    public List<GameObject> player_message = null;
    public List<GameObject> gameObject_inventory = null;

    //장착중인 아이템
    public WeaponItem equip_weapon; // 장착 중인 아이템
    public WeaponItem ready_weapon; // 장착 대기 아이템

    //스텟
    public int player_level; // 레벨
    public int attack_value; // 공격력
    public int defense_value; // 방어력
    public float speed_value; // 스피드
    
    public float player_maxHp; // 최대 체력
    public float player_hp; // 현재 체력
    public float player_hpHealingSpeed; // 체력 회복 속도
    
    public float player_maxMp; // 최대 마나
    public float player_mp; // 최대 마나
    public float player_mpHealingSpeed; // 마나 회복 속도
    
    public float player_maxHungry; // 최대 배고픔 수치
    public float player_hungry; // 현재 배고픔 수치
     
    public float player_maxStamina; // 최대 스태미나 수치
    public float player_stamina; // 현재 스태미나 수치
   
    public int player_maxExp; // 최대 경험치통
    public int player_exp; // 현재 경험치통

    public bool isSuperMode; // 무적상태
    public bool OnSword; // 검을 들고 있는 상태인가

    // 현재 보유중인 스텟 포인트
    public int can_useStatPoint;

    //장비착용 관련
    public bool isChangeItem; // 아이템을 바꿨나 확인 변수
    public bool isCallInventory; // 소비아이템 사용 변수

    //플레이어의 정보 초기화
    public PlayerData()
    {
        if(playerData == null)
        {
            playerData = this;
        }
        player_level = 1; // 첫 시작은 1레벨
        attack_value = 10; // 첫 시작은 공격력 10으로 시작
        defense_value = 5; // 방어력 5로 시작
        speed_value = 1.0f;

        player_maxHp = 100; // 최대hp는 100으로 시작
        player_hp = 100; // hp는 100으로 시작
        player_hpHealingSpeed = 1; // 회복속도 1

        player_maxMp = 100; // 최대mp는 100으로 시작
        player_mp = 100; // mp도 100으로 시작
        player_mpHealingSpeed = 1; // mp회복속도 1

        player_maxHungry = 100; // 최대 배고픔 100으로 시작
        player_hungry = 100; // 배고픔도 100으로 시작

        player_maxStamina = 100; // 최대 스태미나는 100으로 시작
        player_stamina = 100; // 스태미나도 100으로 시작

        player_maxExp = 10; // 플레이어의 레벨업 경험치는 10
        player_exp = 0; // 시작할 떄 첫 경험치양 0 

        can_useStatPoint = 5;

        isSuperMode = false;
        isChangeItem = false;

        weapon_inventory = new List<WeaponItem>();
        player_message = new List<GameObject>();
        consume_inventory = new List<ConsumeItem>();
        gameObject_inventory = new List<GameObject>();

        isCallInventory = false;
    }

    //플레이어의 정보를 수정하는 함수
    public void LevelUp()
    {
        player_level++; // 플레이어의 레벨을 1증가시킴

        //레벨이 1증가함으로써 얻게 되는 강화수치
        attack_value += 5;
        defense_value += 4;
        player_maxHp += 20;
        player_maxMp += 20;
        player_maxHungry += 20;
        player_maxStamina += 10;
        player_maxExp = player_maxExp + player_level * 10; //레벨에 따라 필요 경험치량 수정
        can_useStatPoint += 5;

        //모든 상태 재설정
        player_hp = player_maxHp;
        player_mp = player_maxMp;
        player_hungry = player_maxHungry;
        player_stamina = player_maxStamina;
        player_exp = 0;
    }
    //수치 추가함수
    public void Detail_modifyValue(int _attackValue, int _defanseValue, float _playerHp, float _playerMp, float _playerHungry, float _playerStamina, float _playerSpeed, int _playerExp) // 세부 강화수치 조정
    {
        attack_value += _attackValue;
        defense_value += _defanseValue;
        speed_value += _playerSpeed;
        player_hp += _playerHp;
        player_mp += _playerMp;
        player_hungry += _playerHungry;
        player_stamina += _playerStamina;
        //player_mpHealingSpeed += _playerSpeed;
        player_exp += _playerExp;
    }
    //수치 동일화 함수
    public void Detail_modifyEqualValue(int _attackValue, int _defanseValue, float _playerHp, float _playerMp, float _playerHungry, float _playerStamina, float _playerSpeed, int _playerExp) // 세부 강화수치 조정
    {
        attack_value = _attackValue;
        defense_value = _defanseValue;
        speed_value = _playerSpeed;
        player_hp = _playerHp;
        player_mp = _playerMp;
        player_hungry = _playerHungry;
        player_stamina = _playerStamina;
        //player_mpHealingSpeed += _playerSpeed;
        player_exp = _playerExp;
    }



    // 플레이어의 정보를 불러오는 함수
    public int Player_level
    {
        get => player_level;
        private set => player_level = value;
    }

    public int Attack_value
    {
        get => attack_value;
        private set => attack_value = value;
    }

    public int Defense_value
    {
        get => defense_value;
        private set => defense_value = value;
    }

    public float Speed_value
    {
        get => speed_value;
        private set => speed_value = value;
    }

    public float Player_hp
    {
        get => player_hp;
        private set => player_hp = value;
    }

    public float Player_maxHp
    {
        get => player_maxHp;
        private set => player_maxHp = value;
    }

    public float Player_mp
    {
        get => player_mp;
        private set => player_mp = value;
    }

    public float Player_maxMp
    {
        get => player_maxMp;
        private set => player_maxMp = value;
    }

    public float Player_hungry
    {
        get => player_hungry;
        private set => player_hungry = value;
    }
    public float Player_maxHungry
    {
        get => player_maxHungry;
        private set => player_maxHungry = value;
    }

    public float Player_stamina
    {
        get => player_stamina;
        private set => player_stamina = value;
    }
    public float Player_maxStamina
    {
        get => player_maxStamina;
        private set => player_maxStamina = value;
    }

    public int Player_exp
    {
        get => player_exp;
        private set => player_exp = value;
    }
    public int Player_maxExp
    {
        get => player_maxExp;
        private set => player_maxExp = value;
    }
}
