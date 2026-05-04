using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTextScript : MonoBehaviour
{
    [SerializeField] GameObject TextPanel; // 원하는 텍스트 패널을 받아옴
    [SerializeField] GameObject IncludeRawImagePanel; // 원하는 로우이미지가 담긴 패널을 받아옴

    [SerializeField]
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetMouseButton(0))
            {
                Player.SetActive(false);
                TextPanel.SetActive(true);
                IncludeRawImagePanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Player.SetActive(true);
            TextPanel.SetActive(false);
            IncludeRawImagePanel.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Player.SetActive(true);
            TextPanel.SetActive(false);
            IncludeRawImagePanel.SetActive(false);
        }
    }
}
