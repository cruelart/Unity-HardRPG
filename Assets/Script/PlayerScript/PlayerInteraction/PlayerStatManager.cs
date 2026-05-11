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
