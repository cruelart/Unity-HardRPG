using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFSM : MonoBehaviour
{
    private enum GolemState { Idle, Walk, Attack }

    GolemState golemState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwapState(GolemState currentState) // 골렘의 상태를 변환시키는 함수
    {
       // StopCoroutine(golemState.ToString());
        //playerstate = currentState;
        //StartCoroutine(golemState.ToString());
    }
}
