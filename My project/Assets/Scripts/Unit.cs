using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] protected UnitStats stats;
    [SerializeField] protected TeamSide teamSide;

    public TeamSide TeamSide => teamSide;
    public UnitType UnitType => stats.unitType;

    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = stats.maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void SetTeam(TeamSide teamSide)
    {
        this.teamSide = teamSide;
    }
}
