using UnityEngine;

public class DropItemCollider : MonoBehaviour
{
    [SerializeField]
    GameObject CanvasObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasObj.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CanvasObj.SetActive(false);
    }
}
