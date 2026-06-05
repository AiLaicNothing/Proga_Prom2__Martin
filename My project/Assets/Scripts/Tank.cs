using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Tank : Unit
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform enemyBase;

    [SerializeField] private LayerMask unitLayer;
    private Unit closestTarget;
    private bool hasClosestTarget;

    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float timer;

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        agent.speed = stats.moveSpeed;

    }

    private void Update()
    {
        if (enemyBase != null)
        {
            agent.SetDestination(enemyBase.position);
        }

        checkEnemy();
        Shoot();
    }

    public void SetTargetPos(Transform target)
    {
        enemyBase = target;
    }

    private void checkEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stats.attackRange, unitLayer);

        closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Unit")) continue;

            Unit unit = hit.GetComponent<Unit>();

            if (unit == null) continue;

            if (unit.TeamSide == teamSide) continue;

            float sqrDist = (unit.transform.position - transform.position).sqrMagnitude;

            if (sqrDist < closestDistance)
            {
                closestDistance = sqrDist;
                closestTarget = unit;
            }
        }

        hasClosestTarget = closestTarget != null;
    }

    private void Shoot()
    {
        if (!hasClosestTarget) return;

        float targetDis = Vector3.Distance(transform.position, closestTarget.transform.position);

        if (targetDis > stats.attackRange)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer < stats.attackCooldown) return;

        timer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 dir = closestTarget.transform.position - firePoint.position;
        bullet.transform.forward = dir;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        bullet.GetComponent<Bullet>().SetTeam(teamSide);
        bullet.GetComponent<Bullet>().SetDamage(stats.damage);

        if (rb != null)
        {
            rb.linearVelocity = dir * 10;

        }
    }
}

