using System.Collections;
using UnityEngine;

public class MonsterZenManager : MonoBehaviour
{
    [SerializeField]
    private MonsterType monsterType;

    [SerializeField]
    public float spawnTime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine()
    {
        MonsterZen.instance.SpawnMonster(monsterType, this.transform);

        yield return new WaitForSeconds(spawnTime);
    }
}
