using UnityEngine;

public class MonsterReactManager : MonoBehaviour, IF_OnDamaged
{
    MonsterStatManager monsterStatManager;
    private Color damageColor = new Color(1,1,0);
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

    public void OnDamaged(int _damage, GameObject _attacker)
    {
        if(monsterStatManager != null)
        {
            monsterStatManager.OnDamaged(_damage, _attacker);
            DamageTextManager.Instance.ShowDamageText(this.transform, _damage, damageColor);
        }
    }
}
