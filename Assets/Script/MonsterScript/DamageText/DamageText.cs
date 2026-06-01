using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI damageText;

    private float textSpeed; // 데미지 텍스트가 올라가는 속도
    private float lifeTime; // 유지시간
    private float currentTime;

    private Color textColor; // 일반 데미지 -> 흰색 , 크리티컬 -> 노란색

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * textSpeed * Time.deltaTime;

        currentTime += Time.deltaTime;

        textColor.a = currentTime / lifeTime; // 점점 희미해져가게
        damageText.color = textColor; // 실시간으로 색변경

        if(currentTime > lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void Setup(int _damage)
    {
        damageText.text = _damage.ToString(); // 데미지를 문자열로 변환

        textSpeed = 1.0f;
        lifeTime = 1.0f;
        currentTime = 0.0f;

        textColor = damageText.color;
        textColor.a = 1.0f;
        damageText.color = textColor; // 원래 색으로 초기화

        gameObject.SetActive(true);
    }
}
