using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Tank : Unit
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform enemyBase;

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Start()
    {
        base.Start();
        agent.speed = stats.moveSpeed;
    }

    private void Update()
    {
        if (enemyBase != null)
        {
            agent.SetDestination(enemyBase.position);
        }
    }

    public void SetTargetPos(Transform target)
    {
        enemyBase = target;
    }
}

