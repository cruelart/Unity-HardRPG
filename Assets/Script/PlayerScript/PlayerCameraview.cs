using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraview : MonoBehaviour
{
    PlayerMove playerMove;
    float MouseX = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerMove.transform.position.x, 9.55f, playerMove.transform.position.z);
        cmRotation();
    }

    void cmRotation()
    {
        MouseX += Input.GetAxis("Mouse X") * 10;
        transform.eulerAngles = new Vector3(0, MouseX, 0);
    }
}
