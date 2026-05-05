using UnityEngine;

public class Re_Camera : MonoBehaviour
{

    [Header("카메라 세팅")]
    public Transform target;
    public float PlCaDistance = 10f; // 플레이어와 카메라의 거리
    public float InitDistance; // 플레이어와 카메라의 초기 거리
    //public Vector3 offset = new Vector3(0, 2f, -5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("회전값")]
    public float sensitivity = 5f;

    //상하는 회전제한 둘 예정 -> 한바퀴 뒤집히면 뭔가 이상할거같아서
    public float minYAngle = -20f; // 회전 최소
    public float maxYAngle = 80f; // 회전 최대

    private float currentX = 0f;
    private float currentY = 0f;

    private void Awake()
    {
        //초기 변수 설정
        InitDistance = PlCaDistance;
    }

    void Start()
    {
        if (target == null)
        {
            Debug.Log("플레이어가 감지안됨. 플레이어를 넣어라 까먹지말고");
        }
        Debug.Log("정상적으로 인식됐습니다");

        Cursor.lockState = CursorLockMode.Locked; // 커서 Lock
        Cursor.visible = false; // 커서 숨김
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.Log("플레이어가 감지안됨. 플레이어를 넣고 해라 난 return 한다 ㅂ2");
            return;
        }

        //일단 마우스 움직임이 감지계산
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle); // 위아래 제한걸기

        //회전값
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 dir = new Vector3(0, 0, -PlCaDistance);

        //메인 카메라의 위치 target중심으로 변경
        this.transform.position = target.position + (rotation * dir);
        Debug.Log("플레이어 현재 위치: " + target.position);

        transform.LookAt(target.position); // 플레이어 중심 바라보기;

        ZoomView(rotation);

    }

    void ZoomView(Quaternion _rotation) // 카메라가 벽에 가려 플레이어가 보이지 않을 때
    {
        RaycastHit rayhit;
        Vector3 cam_dir = _rotation * Vector3.back;

        if (Physics.Raycast(target.position, cam_dir, out rayhit, InitDistance)) // 플레이어에서 카메라로 레이캐스트 쏘고 (InitDistance쓰는 이유는 
        {
            if (rayhit.transform.tag == "Wall") // 플레이어가 벽에 막혀있다면, 벽이 발견됐다면
            {
                PlCaDistance = Mathf.Lerp(PlCaDistance, rayhit.distance - 0.5f, 30 * Time.deltaTime); // 벽 앞에다가 카메라 세워두기
            }

        }
        else
        {
            if (PlCaDistance < InitDistance)
            {
                PlCaDistance = Mathf.Lerp(PlCaDistance, InitDistance, 30 * Time.deltaTime);
            }
        }
    }
}
