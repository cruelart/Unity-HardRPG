using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MonsterEvent.OnMonsterDead += GetExpReward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectMonsterDie()
    {

    }

    public void GetReward(MonsterDeadInfo _monsterDeadInfo)
    {

    }

    //몬스터가 지급하는 경험치
    public void GetExpReward(MonsterDeadInfo _monsterDeadInfo)
    {
        PlayerStatManager targetPlayerStatManager = _monsterDeadInfo.killerPlayer;
        MonsterDB monsterDB = MonsterDBManager.Instance.GetMonsterDB(_monsterDeadInfo.monsterID);

        if (targetPlayerStatManager == null)
        {
            Debug.Log("targetPlayerStatManager null인데?");
        }

        targetPlayerStatManager.GetExp(monsterDB.exp_value);
    }
}
