using UnityEngine;
using UnityEngine.AI;
using UnityEngine.iOS;

public class WanderingTraderNav : MonoBehaviour
{
    [SerializeField]
    private NavigationNode currentNode; // 현재 위치해 있는 노드

    NavigationNode nextNode; // 다음으로 갈 노드

    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        this.transform.position = currentNode.transform.position; // 위치 설정
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        MoveNextNode();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsArrived())
        {
            currentNode = nextNode;
            MoveNextNode();
        }
    }

    private void MoveNextNode()
    {
        nextNode = DecideNextNode();
        agent.SetDestination(nextNode.transform.position);
    }

    private NavigationNode DecideNextNode()
    {
        if (currentNode.Edges.Count == 0)
        {
            return null;
        }

        //선택확률조정
        int total_weight = 0;

        foreach (var edge in currentNode.Edges)
        {
            total_weight += edge.weight;
        }

        int randomNum = Random.Range(0, total_weight);

        foreach (var edge in currentNode.Edges)
        {
            randomNum -= edge.weight;

            if(randomNum <= 0)
            {
                return edge.node;
            }
        }

        return currentNode.Edges[currentNode.Edges.Count - 1].node;
    }

    private bool IsArrived()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                return true;
            }
        }

        return false;
    }

}
