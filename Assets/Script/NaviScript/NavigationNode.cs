using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Edge
{
    public NavigationNode node;
    public int weight; // 가중치 간선을 위한 값
}

public class NavigationNode : MonoBehaviour
{
    [SerializeField]
    private List<Edge> edges = new();
    public IReadOnlyList<Edge> Edges => edges; // 읽기 전용


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(var edge in edges)
        {
            if(edge.node != null)
            {
                Gizmos.DrawLine(transform.position, edge.node.gameObject.transform.position);
            }
        }
    }
}
