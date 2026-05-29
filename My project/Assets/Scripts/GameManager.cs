using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void EndGame(TeamSide winner)
    {
        Debug.Log("Winner: " + winner);
        Time.timeScale = 0f;
    }
}
