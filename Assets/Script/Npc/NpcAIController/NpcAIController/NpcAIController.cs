using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public abstract class NpcAIController : MonoBehaviour
{
    protected Transform targetTransform;

    protected NpcState npcState = NpcState.Move; // NPC 상태

    protected Node root; // 루트노드

    protected NavMeshAgent agent;

    //private WanderingTraderNav wanderingTraderNav;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
}
