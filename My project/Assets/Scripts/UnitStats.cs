using UnityEngine;

[CreateAssetMenu(menuName = "Unit Stats")]

public class UnitStats : ScriptableObject
{
    public UnitType unitType;
    public float maxHealth = 100;
    public float damage = 10;
    public float moveSpeed = 5;
    public float attackRange = 5;
    public float attackCooldown = 1;
    public int cost = 50;
}
