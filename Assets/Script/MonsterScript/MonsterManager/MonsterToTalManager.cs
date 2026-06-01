using UnityEngine;

public class MonsterToTalManager : MonoBehaviour
{
    [Header("몬스터 고유 아이디 입력란")]
    [SerializeField]
    private int monsterID = -1;

    private MonsterDB monsterDB;

    private MonsterStatManager monsterStatManager;
    private MonsterHitBox monsterHitbox;
    private MonsterAIController monsterAIController;
    private MonsterHpUIManager monsterHpUIManager;
    private MonsterReactManager monsterReactManager;
    private MonsterBase monsterBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        //이 스크립트의 주인 오브젝트의 하위 컴포넌트들 받아오기
        monsterStatManager = GetComponent<MonsterStatManager>();
        monsterHitbox = GetComponentInChildren<MonsterHitBox>(true);
        monsterAIController = GetComponent<MonsterAIController>();
        monsterHpUIManager = GetComponentInChildren<MonsterHpUIManager>();
        monsterReactManager = GetComponent<MonsterReactManager>();
        monsterBase = GetComponent<MonsterBase>();
    }
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadData()
    {
        monsterDB = MonsterDBManager.instance.monsterDBMap[monsterID]; // 해당 몬스터의 데이터를 받아옴

        //데이터전달
        monsterStatManager.Init(monsterDB);
        monsterHitbox.Init(monsterStatManager);
        monsterAIController.Init(monsterStatManager);
        monsterHpUIManager.Init(monsterStatManager);
        monsterReactManager.Init(monsterStatManager);
    }
}
