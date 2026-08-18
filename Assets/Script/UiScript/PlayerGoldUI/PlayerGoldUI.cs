using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goldText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerGoldManager.Instance.OnChangeGold += ChangeGoldText;
        goldText.text = PlayerGoldManager.Instance.GetCurrentGoldValue().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeGoldText(long _goldValue)
    {
        goldText.text = _goldValue.ToString(); 
    }
}
