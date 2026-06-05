using UnityEngine;

public class EnemyBaseAI : MonoBehaviour
{
    private Base currentBase;
    private BaseShop shop;

    [Header("AI")]
    [SerializeField] private float decisionTime = 3f;
    [SerializeField] private int tankCost = 50;
    [SerializeField] private int chopperCost = 40;

    private float timer;
    private TeamSide teamSide;

    private void Awake()
    {
        if (currentBase == null) currentBase = GetComponent<Base>();

        if (shop == null) shop = GetComponent<BaseShop>();

        if (currentBase != null) teamSide = currentBase.TeamSide;
    }

    private void Update()
    {
        if (currentBase == null || shop == null || EconomyManager.Instance == null) return;

        timer += Time.deltaTime;
        if (timer < decisionTime) return;

        timer = 0f;
        MakeDecision();
    }

    private void MakeDecision()
    {
        int money = EconomyManager.Instance.GetMoney(teamSide);

        if (money >= tankCost)
        {
            shop.BuyTank();
        }
        else if (money >= chopperCost)
        {
            shop.BuyChopper();
        }
    }
}
