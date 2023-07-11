using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int killscounter = 0;
    public static bool GameIsOver;

    public Player player;
    public GameObject gameOverUI;

    void Start()
    {
        killscounter = 0;
        GameIsOver = false;
    }
    void Update()
    {
        if (GameIsOver)
            return;

        if (player.health <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
