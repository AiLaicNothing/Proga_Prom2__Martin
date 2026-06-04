using UnityEngine;

public class BaseShop : MonoBehaviour
{
    [SerializeField] private TeamSide teamSide;
    [SerializeField] private Transform spawnPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject chopperPrefab;

    [Header("Patroll")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform enemyBase;

    public void BuyTank()
    {
        SpawnTank(tankPrefab, 50);
    }

    public void BuyChopper()
    {
        SpawnChopper(chopperPrefab, 40);
    }

    private void SpawnTank(GameObject prefab, int cost)
    {
        if (!EconomyManager.Instance.Spend(teamSide, cost)) return;

        GameObject tankPrefab = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Tank tank = tankPrefab.GetComponent<Tank>();
        tank.SetTargetPos(enemyBase);
    }

    private void SpawnChopper(GameObject prefab, int cost)
    {
        if (!EconomyManager.Instance.Spend(teamSide, cost)) return;

        GameObject chopperPrefab = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Chopper chopper = chopperPrefab.GetComponent<Chopper>();
        chopper.SetPatrolPoint(patrolPoints);
    }
}
