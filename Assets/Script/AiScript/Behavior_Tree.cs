using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum tasks { Success, Failure, Running }

    abstract public class Node
    {
        abstract public tasks Evaluate();
    }

    public class SelectorNode : Node
    {
        List<Node> childlist = new List<Node>();

        public void ListAdd(Node node) // 자식노드 추가
        {
            childlist.Add(node);
        }
        public override tasks Evaluate()
        {
            foreach (Node node in childlist) // 자식노드를 탐색
            {
                switch (node.Evaluate()) // 자식노드가 반환한 값이
                {
                    case tasks.Success: // 성공일 경우
                        return tasks.Success; // 자식중 성공이 나오면 바로 return
                    case tasks.Running:
                        return tasks.Running; // 자식중 실행중이면 바로 return
                }
            }
            return tasks.Failure; // 성공도 아니고 실행도 아닌 상태라면 실패 return
        }
    }

    public class SequenceNode : Node
    {
        List<Node> childlist = new List<Node>();

        public void ListAdd(Node node)
        {
            childlist.Add(node);
        }
        public override tasks Evaluate()
        {
            foreach (Node node in childlist)
            {
                switch (node.Evaluate())
                {
                    case tasks.Success:
                        continue;
                    case tasks.Failure: // 자식노드중 1개라도 실패시 Failure반환 
                        return tasks.Failure;
                    case tasks.Running:
                        return tasks.Running;
                }
            }
            return tasks.Success; // 모든 자식노드를 순회하여 자식노드가 존재하지 않는다면 Success반환 
        }
    }

    public class ActionNode : Node
    {

        public delegate tasks Action(); // states를 반환하는 delegate 선언
        public Action action = null;

        public override tasks Evaluate()
        { 
            return action();
        }

        
    }
}
