using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;

    public ItemManager itemManager;

    public ZombieSpawner zombieSpawner;

    private int score;

    public bool IsGameOver { get; private set; }

    public void Start()
    {
        var findPlayer = GameObject.FindWithTag("Player");
        if (findPlayer != null)
        {
            var playerHealth = findPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.OnDeath += EndGame;
            }
        }
        score = 0;
        IsGameOver = false;
        uiManager.SetUpdateScore(score);
        uiManager.SetActiveGameOverUi(false);
    }

    public void AddScore(int points)
    {
        if (IsGameOver) return;
        score += points;
        uiManager.SetUpdateScore(score);
    }

    public void EndGame()
    {
        IsGameOver = true;
        uiManager.SetActiveGameOverUi(true);
        zombieSpawner.enabled = false;
        itemManager.enabled = false;
    }
}
