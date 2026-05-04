using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection
{
    public PlayerDirection(Transform _playerTransform, GameObject _mainCam)
    {
        playerTransform = _playerTransform;
        mainCam = _mainCam;
    }

    private Transform playerTransform;
    private GameObject mainCam;

    Vector3 Cam_dir = Vector3.zero;
    private Vector3 dir = Vector3.zero; // 플레이어의 방향 초기 설정
    private float PlayerRotationSpeed = 10.0f;
    private Vector3 futureDir = new Vector3(0, 0, 0);
    private Vector3 LerpBugStop = new Vector3(1, 0, 1);

    void PlayerDirMove(float ad, float sw)
    {
        if (ad < 0) // 왼쪽을 눌렀을때
        {
            if (sw == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (sw > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (sw < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.z + -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
            }
        }
        else if (ad > 0)
        {
            if (sw == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, +Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (sw > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (sw < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.z + -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
            }
        }

        else if (sw < 0)
        {
            if (ad == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z + (-Cam_dir.x), PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, -Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, -Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
        }
        else if (sw > 0)
        {
            if (ad == 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad > 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x + Cam_dir.z, PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z + (-Cam_dir.x), PlayerRotationSpeed * Time.deltaTime);
            }
            else if (ad < 0)
            {
                futureDir.x = Mathf.Lerp(futureDir.x, Cam_dir.x + (-Cam_dir.z), PlayerRotationSpeed * Time.deltaTime);
                futureDir.z = Mathf.Lerp(futureDir.z, Cam_dir.z + Cam_dir.x, PlayerRotationSpeed * Time.deltaTime);
            }
        }

        dir = futureDir;

    }
    //최종 플레이어의 방향
    public Transform PlayerDir()
    {

        Cam_dir.x = Mathf.Sin(mainCam.transform.eulerAngles.y * Mathf.Deg2Rad);
        Cam_dir.z = Mathf.Cos(mainCam.transform.eulerAngles.y * Mathf.Deg2Rad);
        float AD = Input.GetAxis("Horizontal");
        float SW = Input.GetAxis("Vertical");

        PlayerDirMove(AD, SW);




        dir.Normalize();

        if (dir != Vector3.zero)
        {
            if (playerTransform.forward.normalized == -dir) // 정반대 방향 선형보간법 버그를 위한 코드
            {
                playerTransform.forward += LerpBugStop;
            }


            playerTransform.forward = Vector3.Lerp(playerTransform.forward, dir, 50 * Time.deltaTime);
        }

        return playerTransform;
    }
}

