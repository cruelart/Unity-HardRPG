using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathMonster : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    Vector3 PlMo = new Vector3(0, 0, 0);
    //PlayerMove playerMove;
    public float vertorLength;
    // Start is called before the first frame update
    void Start()
    {
        //playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        PlMo = Player.transform.position - transform.position;
        vertorLength = PlMo.magnitude;

    }
}
