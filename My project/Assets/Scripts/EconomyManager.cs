using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [Header("Money")]
    [SerializeField] private int redMoney = 100;
    [SerializeField] private int blueMoney = 100;

    [Header("Passive Income")]
    [SerializeField] private int incomeAmount = 10;
    [SerializeField] private float incomeRate = 2.5f;

    private float timer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= incomeRate)
        {
            timer = 0f;

            AddMoney(TeamSide.Red, incomeAmount);
            AddMoney(TeamSide.Blue, incomeAmount);
        }
    }

    public bool CanBuy(TeamSide side, int amount)
    {
        return GetMoney(side) >= amount;
    }

    public bool Spend(TeamSide side, int amount)
    {
        if (!CanBuy(side, amount)) return false;

        if (side == TeamSide.Red)
        {
            redMoney -= amount;
        }
        else
        {
            blueMoney -= amount;
        }

        return true;
    }

    public void AddMoney(TeamSide side, int amount)
    {
        if (side == TeamSide.Red)
        {
            redMoney += amount;
        }
        else
        {
            blueMoney += amount;
        }
    }

    public int GetMoney(TeamSide side)
    {
        return side == TeamSide.Red ? redMoney : blueMoney;
    }
} 

