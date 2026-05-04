using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonsterHpBar : MonoBehaviour
{
    Canvas UICanvas;
    Camera UICanvas_camera; // UICanvas를 찍고있는 카메라

    RectTransform UICanvas_rectTransform;
    RectTransform UI_rectTransform; // 표현할 ui의 RectTransform

    [HideInInspector]
    public Transform monsterTransform; // 몬스터의 위치정보

    public Vector3 offset = Vector3.zero; // 몬스터기준 offset설정변수

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GetComponentInParent<Canvas>(); // 프리펩의 부모노드에 있는 컴포넌트중 Canvas를 반환
        UICanvas_camera = UICanvas.worldCamera;

        UICanvas_rectTransform = UICanvas.GetComponent<RectTransform>();
        UI_rectTransform = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(monsterTransform.position + offset); // 해당 UI의 위치를 screen좌표로 변환

        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f; // screen좌표가 음수가 된다면 양수로 바꾸게 설정 Because 플레이가 카메라를 돌리면 ui가 보이지 않게 하기위하여
        }

        Vector2 get_localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UICanvas_rectTransform, screenPos, UICanvas_camera, out get_localPos);
        UI_rectTransform.localPosition = get_localPos; //화면에 표시될 최종 UI의 좌표
    }
}
