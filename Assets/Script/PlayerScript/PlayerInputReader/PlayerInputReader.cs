using System;
using UnityEngine;

//이 스크립트는 입력만을 처리합니다.
public class PlayerInputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public event Action OnInteractPressed; // 상호작용 -> TalkUI띄우는 용도
    public event Action OnInventoryPressed; // 인벤토리 -> 인벤토리 UI 띄우는 용도
    public event Action OnEquipSpacePressed; // 장비창 -> 장비창 UI 띄우는 용도
    public event Action OnQuestPressed; // 퀘스트 -> 퀘스트 UI 띄우는 용도
    public event Action OnPlayerStatusPressed; // 플레이어 정보 -> 플레이어 정보 UI 띄우는 용도
    public event Action OnUIExitPressed; // Exit

    //public event Action On
    // Start is called once before the first execution of Updatep after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
        }

        //----------------------------------------------------UI On - Off
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInventoryPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnEquipSpacePressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnInteractPressed?.Invoke();
        }

        //target과의 상호작용
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnInteractPressed?.Invoke();
            //target?.Interact(transform.parent.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnQuestPressed?.Invoke();
            //UIManager.Instance.ShowQuestUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnUIExitPressed?.Invoke();
        }
    }
}
