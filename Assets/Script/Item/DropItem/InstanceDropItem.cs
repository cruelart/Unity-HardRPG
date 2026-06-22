using System.Collections;
using UnityEngine;

public class InstanceDropItem : MonoBehaviour
{
    [Header("하위 Visual Root에 대한 필요정보")]
    [SerializeField]
    private Transform visualRootTransform;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [Header("UI관련 정보")]
    [SerializeField]
    private DropItemUI dropItemUI;

    [Header("이펙트 정보")]
    [SerializeField]
    private Transform effectTransform;

    [Header("드롭아이템 유지 시간")]
    [SerializeField]
    private float returnTime = 60.0f;

    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ReturnDropItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingDropItem(Mesh _itemMesh, Material _itemMaterial, Vector3 _itemRotation, Vector3 _itemScale, Vector3 _itemPosition, int _itemID)
    {
        meshFilter.sharedMesh = _itemMesh;
        meshRenderer.sharedMaterial = _itemMaterial;

        visualRootTransform.localEulerAngles = _itemRotation + new Vector3(-90, 0,0);
        visualRootTransform.localScale = _itemScale;
        this.transform.position = _itemPosition + new Vector3(0, 1.5f, 0);
        visualRootTransform.position = _itemPosition + new Vector3(0,1.5f,0);
        effectTransform.position = visualRootTransform.position;
        dropItemUI.Init(_itemPosition, _itemID);
    }

    public void RealDrop()
    {
        Vector3 random_force = new Vector3(Random.Range(-3f, 3f), Random.Range(4f, 6f), Random.Range(-3f, 3f));

        rigid.AddForce(random_force, ForceMode.Impulse);
    }

    //돌아가야지, 일정 시간지나면 다시 풀로
    IEnumerator ReturnDropItem()
    {
        yield return new WaitForSeconds(returnTime);

        DropItemPoolManager.Instance.ReturnPool(this);
    }
}
