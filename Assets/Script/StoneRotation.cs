using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotation : MonoBehaviour
{
    float stoneRo_Y; // 돌이 Y축으로 회전할 값
    int rotationSpeed = 20; // 돌의 회전 속도
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stoneRo_Y = this.transform.eulerAngles.y + rotationSpeed * Time.deltaTime;

        //돌의 회전 설정
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x ,stoneRo_Y, 0);
    }
}
