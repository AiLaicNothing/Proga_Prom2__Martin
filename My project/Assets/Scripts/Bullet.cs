using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 5;
    private TeamSide teamSide;

    public void SetTeam(TeamSide team)
    {
        teamSide = team;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            Unit unit = other.GetComponent<Unit>();

            if (unit != null)
            {
                if (unit.TeamSide == teamSide)
                {
                    return;
                }
                else
                {
                    unit.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                if (player.TeamSide == teamSide)
                {
                    return;
                }
                else
                {
                    player.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
