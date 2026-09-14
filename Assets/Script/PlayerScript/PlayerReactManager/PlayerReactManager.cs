using UnityEngine;

public class PlayerReactManager : MonoBehaviour, IF_OnDamaged
{
    private PlayerStatManager playerStatManager;
    private Color damageColor = new Color(1,0,0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(PlayerStatManager _playerStatManager)
    {
        playerStatManager = _playerStatManager;
    }

    public void OnDamaged(int _damage, GameObject _attacker)
    {
        if (playerStatManager != null)
        {
            playerStatManager.OnDamaged(_damage);
            DamageTextManager.Instance.ShowDamageText(this.transform, _damage, damageColor);
        }
    }

}
