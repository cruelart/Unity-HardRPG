using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform rectTransform;

    private Vector2 move_offset;

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
        //부모기준 좌표계산
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localMouse);

        //이동 기준점을 잡고
        move_offset = rectTransform.anchoredPosition - localMouse; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localMouse);

        rectTransform.anchoredPosition = localMouse + move_offset;
        ControllUIPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    private void ControllUIPosition()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners); // 위치 받아오고

        Vector3 move_pos = Vector3.zero;

        if (corners[2].x > Screen.width)
        {
            move_pos.x -= corners[2].x - Screen.width;
        }

        if (corners[2].y > Screen.height)
        {
            move_pos.y -= corners[2].y - Screen.height;
        }

        if (corners[0].x < 0)
        {
            move_pos.x -= corners[0].x;
        }

        if (corners[0].y < 0)
        {
            move_pos.y -= corners[0].y;
        }

        rectTransform.position += move_pos;
    }
}
