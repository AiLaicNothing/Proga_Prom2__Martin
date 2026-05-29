using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private TeamSide teamSide;

    public TeamSide TeamSide => teamSide;

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit == null) return;

        if (unit.TeamSide != teamSide && unit.UnitType == UnitType.Tank || unit.UnitType == UnitType.SuperTank)
        {
            GameManager.Instance.EndGame(unit.TeamSide);
        }
    }
}
