using System.Collections;
using TMPro;
using UnityEngine;

public class NotifyUI : MonoBehaviour
{
    public TextMeshProUGUI notifyText;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;
    private Vector2 origin_pos;

    [Header("UI 타이밍조절")]
    [SerializeField]
    private float moveSpeed = 50.0f;

    [SerializeField]
    private float showTime = 3.0f;

    private void Awake()
    {
        notifyText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        origin_pos = rectTransform.anchoredPosition;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        rectTransform.anchoredPosition = origin_pos;
        StopAllCoroutines();

        StartCoroutine(ShowPanel());
    }

    IEnumerator ShowPanel()
    {
        float timer = 0.0f;

        while (timer< showTime)
        {
            rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            canvasGroup.alpha = 1.0f - (timer / showTime);

            yield return null;
        }

        NotifyUIPoolManager.Instance.ReturnPool(this.gameObject);
    }
}
