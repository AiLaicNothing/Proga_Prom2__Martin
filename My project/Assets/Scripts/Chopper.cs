using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Chopper : Unit
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float reachDistance = 1.5f;

    private int index;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)return;

        Transform target = patrolPoints[index];

        transform.position = Vector3.MoveTowards(transform.position,  target.position,stats.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= reachDistance) index = (index + 1) % patrolPoints.Length;
    }
}
