using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject CameraViewPoint;

    private float PlCaDistance = 24.0f; // 플레이어와 카메라의 거리
    private float PlCaAngle = 0.0f;
    public float RotationSpeed = 600.0f;

    float Ro_x, Ro_y;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0) // 마우스의 움직임이 있을 때 줌 함수 호출
        {
            if (!Input.GetKey(KeyCode.Q))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                ZoomView();

                //위로 마우스를 올리면 유니티에서는 x의 좌표가 바뀜
                float Move_mouseX = -Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSpeed; // 마우스 수평 이동
                float Move_mouseY = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed; // 마우스 수직 이동

                Ro_x = Mathf.Abs(this.transform.eulerAngles.x + Move_mouseX);
                Ro_y = Mathf.Abs(Ro_y + Move_mouseY);

                //Ro_x = Mathf.Clamp(Ro_x, -90, 90); // 카메라가 반바퀴를 돌아 뒤를 보지않도록함 -> 이렇게 하면 캐릭터가 뒤를 돌지않기에 주석처리함
                //Debug.Log(this.transform.eulerAngles.y);
                this.transform.eulerAngles = new Vector3(Ro_x, Ro_y, 0); // 카메라가 바라보고 있는 방향과도 같음

                //transform.rotation.y -> x방향으로 이동한 각도를 나타냄 
                PlCaAngle = this.transform.eulerAngles.y;
            }
            else
            {
                // 마우스 숨기기
                Cursor.visible = false;

                // 마우스 고정
                Cursor.lockState = CursorLockMode.Locked;
            }

            //Vector3.forward와 카메라의 회전방향의 각을 계산하고 PlCa거리로 삼각함수 좌표계산
        }
        this.transform.position = new Vector3(player.transform.position.x - PlCaDistance * Mathf.Sin(PlCaAngle), player.transform.position.y + 15, player.transform.position.z - PlCaDistance * Mathf.Cos(PlCaAngle));
        this.transform.LookAt(CameraViewPoint.transform);

        void ZoomView() // 카메라가 벽에 가려 플레이어가 보이지 않을 때
        {
            RaycastHit rayhit;
            if (Physics.Raycast(transform.position, transform.forward, out rayhit, PlCaDistance))
            {
                if (rayhit.transform.tag == "Wall") // 플레이어가 벽에 막혀있다면
                {
                    PlCaDistance = PlCaDistance - 30 * Time.deltaTime;
                }

            }
            else
            {
                if (PlCaDistance < 24)
                {
                    PlCaDistance = PlCaDistance + 30 * Time.deltaTime;
                }
            }
        }

    }
}
