using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpbar : MonoBehaviour
{
    private Camera hpBarCamera;
    private Canvas canvas;
    private RectTransform rectCanvas;
    private RectTransform rectHp;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        hpBarCamera = canvas.worldCamera;
        rectCanvas = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

        if(screenPos.z < 0.0f) // 카메라를 거꾸로 돌리면 화면에 UI가 출력되지 않게하기위함
        {
            screenPos *= -1.0f;
        }

        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, screenPos, hpBarCamera, out localPos);

        rectHp.localPosition = localPos;
    }
}
