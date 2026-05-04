using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorrScript : MonoBehaviour
{
    [SerializeField]
    private GameObject BlueDoor;

    [SerializeField]
    private GameObject YellowDoor;

    [SerializeField]
    private GameObject GreenDoor;

    [SerializeField]
    private GameObject GreenDoor2;

    bool isTouchBlueStone = false;
    bool isTouchYellowStone = false;
    bool isTouchGreenStone = false;
    bool isTouchGargoyle = false;

    float bluecloseState_y;
    float yellowcloseState_y;
    float greencloseState_y;
    // Start is called before the first frame update
    void Start()
    {
        closeState();
    }

    // Update is called once per frame
    void Update()
    {
        isOpenDoor();
        isCloseDoor();
        Debug.Log(greencloseState_y);
        //Debug.Log(isTouchBlueStone);
        //Debug.Log("isTouchBlueStone:" + BlueDoor.transform.eulerAngles.y);
        //Debug.Log("isTouchYellowStone:" + YellowDoor.transform.eulerAngles.y);
        //Debug.Log("isTouchGreenStone:" + GreenDoor.transform.eulerAngles.y);
        //Debug.Log("isTouchGreenStone:"+isTouchGreenStone);
        //Debug.Log("isTouchBlueStone:"+isTouchBlueStone);
        //Debug.Log("isTouchYellowStone:"+isTouchYellowStone);
        ////Debug.Log(isTouchGreenStone);

        //Debug.Log(this.transform.eulerAngles.y);

    }
    public void closeState()
    {
        bluecloseState_y = BlueDoor.transform.eulerAngles.y;
        yellowcloseState_y = YellowDoor.transform.eulerAngles.y;
        greencloseState_y = GreenDoor.transform.eulerAngles.y;
        greencloseState_y = GreenDoor2.transform.eulerAngles.y;
        
    }
    public void isOpenDoor()
    {
        if(isTouchBlueStone && BlueDoor.transform.eulerAngles.y > bluecloseState_y - 95 )
        {
            BlueDoor.transform.eulerAngles = new Vector3(0, BlueDoor.transform.eulerAngles.y - 6 * Time.deltaTime, 0);
        }

        if (isTouchYellowStone && YellowDoor.transform.eulerAngles.y > yellowcloseState_y - 95)
        {
            YellowDoor.transform.eulerAngles = new Vector3(0, YellowDoor.transform.eulerAngles.y - 6 * Time.deltaTime, 0);
        }

        if (isTouchGreenStone && GreenDoor.transform.eulerAngles.y > greencloseState_y - 90)
        {
            GreenDoor.transform.eulerAngles = new Vector3(0, GreenDoor.transform.eulerAngles.y - 6 * Time.deltaTime, 0);
        }

        if (isTouchGreenStone && GreenDoor2.transform.eulerAngles.y > greencloseState_y - 90)
        {
            GreenDoor2.transform.eulerAngles = new Vector3(0, GreenDoor2.transform.eulerAngles.y - 6 * Time.deltaTime, 0);
        }
        //if (_isTouchLegendStone && _touchcolor.transform.eulerAngles.y > 265)
        //{
        //    Debug.Log("실행되고있니");
        //    _touchcolor.transform.eulerAngles = new Vector3(0, _touchcolor.transform.eulerAngles.y - 6 * Time.deltaTime, 0); // 문이 열림
        //}
    }
    public void isCloseDoor()
    {
        if(!isTouchBlueStone && BlueDoor.transform.eulerAngles.y <= bluecloseState_y)
        {
            BlueDoor.transform.eulerAngles = new Vector3(0, BlueDoor.transform.eulerAngles.y + 6 * Time.deltaTime, 0);
        }

        if (!isTouchYellowStone && YellowDoor.transform.eulerAngles.y <= yellowcloseState_y)
        {
            YellowDoor.transform.eulerAngles = new Vector3(0, YellowDoor.transform.eulerAngles.y + 6 * Time.deltaTime, 0);
        }

        if (!isTouchGreenStone && GreenDoor.transform.eulerAngles.y <= greencloseState_y)
        {
            GreenDoor.transform.eulerAngles = new Vector3(0, GreenDoor.transform.eulerAngles.y + 6 * Time.deltaTime, 0);
        }
        if (!isTouchGreenStone && GreenDoor2.transform.eulerAngles.y <= greencloseState_y)
        {
            GreenDoor2.transform.eulerAngles = new Vector3(0, GreenDoor2.transform.eulerAngles.y + 6 * Time.deltaTime, 0);
        }
    }
    public void isBlueStoneClick()
    {
        isTouchBlueStone = true;
        isTouchYellowStone = false;
        isTouchGreenStone = false;
    }
    public void isYellowStoneClick()
    {
        isTouchBlueStone = false;
        isTouchYellowStone = true;
        isTouchGreenStone = false;
    }
    public void isGreenStoneClick()
    {
        isTouchBlueStone = false;
        isTouchYellowStone = false;
        isTouchGreenStone = true;
    }
    public void GargoyleButtonClick()
    {
        isTouchGargoyle = true;
    }
}
