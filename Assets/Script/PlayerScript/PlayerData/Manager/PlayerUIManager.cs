using UnityEngine.UI;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public Image hpBar;

    private void OnEnable()
    {
        PlayerStatManager.OnHpChanged += UpdateHpBar;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHpBar(int _currentHp, int _maxHp)
    {
        //Debug.Log("PlayerUIManager에 있는 UpdateHpBar호출");
        //Debug.Log("PlayerUIManager" + "currentHp" + _currentHp + "maxHp:" + _maxHp);
        //Debug.Log("PlayerUIManager" + (float)_currentHp / _maxHp);
        hpBar.fillAmount = (float)_currentHp / _maxHp;

    }
}
