using UnityEngine;

public class ConsumerItemInstance
{
    public ConsumerItemDB base_setting;
    public ItemRawData data;

    public ConsumerItemInstance(ConsumerItemDB _base_setting, ItemRawData _data)
    {
        this.base_setting = _base_setting;
        this.data = _data;
    }

    public void UseConsumerItem()
    {

    }
}
