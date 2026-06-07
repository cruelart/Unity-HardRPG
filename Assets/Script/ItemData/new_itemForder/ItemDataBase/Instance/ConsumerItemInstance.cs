using UnityEngine;

public class ConsumerItemInstance
{
    //베이스 아이템 고유데이터
    public ConsumerItemDB setting;

    //인스턴스 아이템 데이터
    public long instanceID { get; private set; }

    public ConsumerItemInstance(ConsumerItemDB _setting, int _id)
    {
        this.setting = _setting;
        //this.data = _data;
        instanceID = ItemInstanceID.GetInstanceID();
        instanceID = _id;
    }

    //아이템 사용했을때 효과
    public void UseConsumerItem()
    {

    }
}
