using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameBtn()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
