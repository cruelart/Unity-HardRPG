using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField]
    private DamageText damageTextPrefab;

    [SerializeField]
    private Canvas damageCanvas;

    [SerializeField]
    private float poolSize = 30;

    private Queue<DamageText> damageTextPool = new Queue<DamageText>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        CreatePool();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            DamageText damageText = Instantiate(damageTextPrefab, damageCanvas.transform);
            damageText.gameObject.SetActive(false);

            damageTextPool.Enqueue(damageText);
        }
    }

    public void ShowDamagerText(Transform _monsterTransform, int _damage)
    {
        DamageText damageText = damageTextPool.Dequeue();

        damageText.transform.position = _monsterTransform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        damageText.gameObject.SetActive(true);

        damageText.Setup(_damage);
    }

    public void ReturnDamageText(DamageText _damageText)
    {
        _damageText.gameObject.SetActive(false);
        damageTextPool.Enqueue(_damageText);
    }
}
