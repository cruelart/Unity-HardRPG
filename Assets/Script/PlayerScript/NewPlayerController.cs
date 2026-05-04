using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    PlayerState player_state;
    PlayerState pastplayer_state;
    Command command;
    CommandManager command_manager = new CommandManager(); // 커맨드 관리 변수
    KeyboardCommand keyboardCommand;

    Rigidbody playerRigid;
    Animator player_anime;

    [SerializeField]
    GameObject MainCam; // 게임오브젝트-> 메인 카메라


    // Start is called before the first frame update
    void Start()
    {
        HpPotion hpPotion = new HpPotion();
        PlayerData.playerData.consume_inventory.Add(hpPotion);

        EpicSword epicSword = new EpicSword();
        PlayerData.playerData.weapon_inventory.Add(epicSword);

        PlayerData.playerData.isCallInventory = true;

        playerRigid = GetComponent<Rigidbody>();
        player_anime = GetComponent<Animator>();
        keyboardCommand = new KeyboardCommand();
        player_state = new Idle(this.transform, playerRigid, this.player_anime, MainCam); // 처음 플레이어의 상태는 대기 상태인 IDLE로 표현
        player_state.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dir);
        //Debug.Log(player_state);
        //Debug.Log("무적모드" + PlayerData.playerData.isSuperMode);

        //state 패턴을 사용
        PlayerState newState = player_state.InputHandler();

        //Debug.Log(newState);

        if (newState != null)
        {
            player_state.Exit();
            player_state = newState;
            player_state.Enter();
        }

        player_state.DoAction();

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("AttackSecion")) // 일반 공격을 받은 상태라면
        {
            if (!PlayerData.playerData.isSuperMode && PlayerData.playerData.player_hp>=0)
            {
                Debug.Log("AttackSECION 접근");
                player_state.Exit();
                player_state = new PlayerReact(this.transform, playerRigid, this.player_anime, MainCam); // 즉시 피격 상태로 전환
                player_state.Enter();

                return;
            }
        }

        if (col.CompareTag("BigAttackSecion")) // 넉백 공격을 받은 상태라면
        {
            if (!PlayerData.playerData.isSuperMode && PlayerData.playerData.player_hp >= 0)
            {
                Debug.Log("BigAttackSECION 접근");
                this.transform.LookAt(col.transform.parent.transform.position);
                player_state.Exit();
                player_state = new PlayerFlyingBack(this.transform, playerRigid, this.player_anime, MainCam); // 즉시 넉백 상태로 전환
                player_state.Enter();

                return;
            }
        }

        if (PlayerData.playerData.player_hp <= 0 && PlayerData.playerData.player_hp > -100)
        {
            PlayerData.playerData.player_hp = -1000;
            player_state.Exit();
            player_state = new PlayerDeath(this.transform, playerRigid, this.player_anime, MainCam);
            player_state.Enter();

            return;
        }
    }


}
