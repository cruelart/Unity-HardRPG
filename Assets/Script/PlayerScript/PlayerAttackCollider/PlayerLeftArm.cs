using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //슬라임와 부딪혔을 경우
        if(col.tag == "Slime")
        {

        }
    }
}
