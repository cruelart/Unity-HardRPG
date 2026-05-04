using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterHpUI : MonoBehaviour
{
    [SerializeField]
    private GameObject newuiCanvas;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject hitParticlePrefab;

    //HpBar 설정---------------------
    public GameObject ui_hpBarPrefab;
    GameObject ui_hpBarObject;
    public Vector3 ui_hpBaroffset = new Vector3(0, 10.0f, 0);
    Camera cam1 = null;

    private Canvas newCanvas;
    private Image ui_image;
    //--------------------------------
    //데미지 UI 설정-----------------
    NewMonsterHpBar monsterUiManager;
    public GameObject ui_damagePrefab;
    GameObject ui_damageObject;
    public Vector3 ui_damageoffset = new Vector3(0.0f, 15.0f, 0);

    private TextMeshProUGUI ui_damageText;
    private bool isReact;

    /////////////////////////////
    SlimeData slimeData;

    List<GameObject> damageUI_list;
    List<GameObject> damageEffectUI_list;

    // Start is called before the first frame update
    void Start()
    {
        damageUI_list = new List<GameObject>();
        damageEffectUI_list = new List<GameObject>();

        cam1 = Camera.main;
        NewSetHpBar();
        NewSetUIText();

        slimeData = GetComponent<MonsterSlimeScript>().slimeData;
    }

    // Update is called once per frame
    void Update()
    {
        SetUIScale();
        monsterUiManager.offset = ui_damageoffset;

        if (isReact)
        {
            ui_image.fillAmount = slimeData.monster_hp / slimeData.monster_maxHp;

            ui_damageoffset.y = 11.0f;
            ui_damageText.text = "-" + (PlayerData.playerData.Attack_value - slimeData.defense_value).ToString();
            isReact = false;
        }

        if (slimeData.monster_hp <= 0)
        {
            Destroy(ui_hpBarObject);
            Destroy(ui_damageObject);

            while(damageEffectUI_list.Count != 0)
            {
                GameObject wantDestory = damageEffectUI_list[damageEffectUI_list.Count - 1];
                damageEffectUI_list.RemoveAt(damageEffectUI_list.Count - 1);
                Destroy(wantDestory);
            }
        }
    }

    void SetUIScale()
    {
        float Distance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (Distance < 50.0f) // 너무 가까울 때 크기 조절
        {
            //Debug.Log("너무가깝습니다");
            Distance = 50.0f;
        }
        else if (Distance > 120.0f) // 너무 멀면 안보이게 설정
        {
            ui_hpBarObject.transform.localScale = new Vector3(0, 0, 0);
            ui_damageObject.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            //Debug.Log(Distance);
            ui_hpBarObject.transform.localScale = new Vector3(50 / Distance, 50 / Distance, 50 / Distance);
            ui_damageObject.transform.localScale = new Vector3(40 / Distance, 40 / Distance, 40 / Distance);
        }
    }


    void NewSetHpBar()
    {
        newCanvas = newuiCanvas.GetComponent<Canvas>();
        ui_hpBarObject = Instantiate<GameObject>(ui_hpBarPrefab, newCanvas.transform);
        ui_image = ui_hpBarObject.GetComponentsInChildren<Image>()[1];
        ui_image.fillAmount = 1.0f; // 첫 설정시 꽉차게 설정

        NewMonsterHpBar monsterUiManager = ui_hpBarObject.GetComponent<NewMonsterHpBar>();
        monsterUiManager.offset = ui_hpBaroffset;
        monsterUiManager.monsterTransform = this.transform;

    }

    void NewSetUIText()
    {
        newCanvas = newuiCanvas.GetComponent<Canvas>();
        ui_damageObject = Instantiate<GameObject>(ui_damagePrefab, newCanvas.transform);
        ui_damageText = ui_damageObject.GetComponent<TextMeshProUGUI>();

        monsterUiManager = ui_damageObject.GetComponent<NewMonsterHpBar>();

        monsterUiManager.monsterTransform = this.transform;

        //ui_damageObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("NormalAttackArea")) // 슬라임과 무기류가 충돌한다면
        {
            isReact = true;

            GameObject hitObject = Instantiate<GameObject>(hitParticlePrefab, col.transform.position, Quaternion.identity);
            damageEffectUI_list.Add(hitObject);
            //Debug.Log("슬라임공격에 성공하였습니다");
            //Debug.Log("슬라임의체력:" + slimeData.monster_hp); // 슬라임의 데미지가 플레이어 공격력에 따라 달라진다.
            //Debug.Log(player.playerAttackValue);

            //ui_damageObject.SetActive(true);

            Debug.Log("fillAmount의 값은" + ui_image.fillAmount);
            Debug.Log("몬스터의 현재 체력은:" + slimeData.monster_hp);
            Debug.Log("몬스터의 최대 체력은:"+slimeData.monster_maxHp);

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DamageTextMove());
            }

        }
    }

    IEnumerator DamageTextMove()
    {
        while (ui_damageoffset.y < 15.0f)
        {
            ui_damageoffset.y += Time.deltaTime;
            //Debug.Log(ui_damageoffset.y);
            yield return null;
        }
        ui_damageoffset.y = 11.0f;
        ui_damageText.text = null;
    }


}
