using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Optional")]
    [SerializeField] private CharacterInput3rdPerson playerInput;

    private bool victoryTriggered;

    private void Awake()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (victoryTriggered) return;

        victoryTriggered = true;
        ShowVictoryMenu();
    }

    private void ShowVictoryMenu()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f;

        if (playerInput != null)
            playerInput.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        Time.timeScale = 1f;

        if (playerInput != null)
            playerInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        victoryTriggered = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}