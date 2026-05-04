using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class TestSeq : MonoBehaviour
{
    SequenceNode MonsterRunOrWalk;

    // Start is called before the first frame update
    void Start()
    {
        MonsterRunOrWalk = new SequenceNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
