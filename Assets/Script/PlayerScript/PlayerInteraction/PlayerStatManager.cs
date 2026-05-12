using UnityEngine;

public class PlayerStatManager : MonoBehaviour, IT_PlayerDamaged
{
    int playerHp = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamaged(int _damage)
    {
        playerHp -= _damage;
        Debug.Log("플레이어가 데미지를 입었다" + _damage);

        if(playerHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        return;
    }
}
