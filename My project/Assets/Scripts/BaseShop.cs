using UnityEngine;

public class BaseShop : MonoBehaviour
{
    [SerializeField] private TeamSide teamSide;
    [SerializeField] private Transform spawnPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject chopperPrefab;

    public void BuyTank()
    {
        SpawnUnit(tankPrefab, 50);
    }

    public void BuyChopper()
    {
        SpawnUnit(chopperPrefab, 40);
    }

    private void SpawnUnit(GameObject prefab, int cost)
    {
        if (!EconomyManager.Instance.Spend(teamSide, cost)) return;

        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
