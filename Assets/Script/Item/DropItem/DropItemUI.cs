using TMPro;
using UnityEngine;

public class DropItemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dropItemName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Init(Vector3 _itemPosition, int _itemID)
    {
        this.transform.position = _itemPosition + new Vector3(0.0f, 3.0f, 0.0f);
        dropItemName.text = ItemDataManager.Instance.GetBaseitemDB(_itemID).itemName;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward; // UI가 카메라 정면 바라보게함
    }
}
