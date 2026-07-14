using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class NotifyUIPoolManager : MonoBehaviour
{
    public static NotifyUIPoolManager Instance;

    private Queue<GameObject> notifyTextObjectPool = new Queue<GameObject>();

    [SerializeField]
    private int pool_size = 20;

    [SerializeField]
    private GameObject notifyPanel; // 알림판을 띄울 패널

    [SerializeField]
    private GameObject notifyObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        CreatePool();
        GameEventChannel.OnNotify += ShowNotifyUI;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePool()
    {
        for(int i = 0; i < pool_size; i++)
        {
            GameObject prefab = Instantiate(notifyObject, notifyPanel.transform);
            prefab.SetActive(false);

            notifyTextObjectPool.Enqueue(prefab);
        }
    }

    private void ShowNotifyUI(string _text)
    {
        GameObject notifyUIObj = notifyTextObjectPool.Dequeue();

        notifyUIObj.SetActive(true);

        NotifyUI notifyUI = notifyUIObj.GetComponent<NotifyUI>();
        notifyUI.notifyText.text = _text;

    }

    public void ReturnPool(GameObject _gameObject)
    {
        notifyTextObjectPool.Enqueue(_gameObject);
        _gameObject.SetActive(false);
    }
}
