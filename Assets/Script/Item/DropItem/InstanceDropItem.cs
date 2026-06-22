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

    [Header("이펙트 정보")]
    [SerializeField]
    private Transform EffectTransform;

    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingDropItem(Mesh _itemMesh, Material _itemMaterial, Vector3 _itemRotation, Vector3 _itemScale, Vector3 _itemPosition)
    {
        meshFilter.sharedMesh = _itemMesh;
        meshRenderer.sharedMaterial = _itemMaterial;

        visualRootTransform.localEulerAngles = _itemRotation;
        visualRootTransform.localScale = _itemScale;
        visualRootTransform.position = _itemPosition + new Vector3(0,1.0f,0);
        EffectTransform.position = visualRootTransform.position;
    }

    public void RealDrop()
    {
        Vector3 random_force = new Vector3(Random.Range(-3f, 3f), Random.Range(4f, 6f), Random.Range(-3f, 3f));

        rigid.AddForce(random_force, ForceMode.Impulse);
    }
}
