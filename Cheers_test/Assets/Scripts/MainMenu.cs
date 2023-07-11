using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("BattleMap");
    }
    public void Training()
    {
        SceneManager.LoadScene("TrainingMap");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
