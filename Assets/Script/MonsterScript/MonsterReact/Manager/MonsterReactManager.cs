using UnityEngine;

public class MonsterReactManager : MonoBehaviour, IF_OnDamaged
{
    MonsterStatManager monsterStatManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(MonsterStatManager _monsterStatmanager)
    {
        monsterStatManager = _monsterStatmanager;
    }

    public void OnDamaged(int _damage)
    {
        if(monsterStatManager != null)
        {
            monsterStatManager.OnDamaged(_damage);
        }
    }
}
