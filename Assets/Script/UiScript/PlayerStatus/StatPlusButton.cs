using UnityEngine;

public class StatPlusButton : MonoBehaviour
{
    [SerializeField]
    private StatType statType;

    [SerializeField]
    private PlayerStatusUI statusUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlusButton()
    {
        statusUI.UpgradeStat(statType);
    }
}
