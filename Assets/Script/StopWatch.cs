using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch
{
    public bool stop_watch(float _calTime, float _wantTime)
    {
        if(Time.time - _calTime > _wantTime)
        {
            return true;
        }
        return false;
    }
}
